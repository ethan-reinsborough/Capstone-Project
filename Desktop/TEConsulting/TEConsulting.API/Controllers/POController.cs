using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TEConsulting.Model;
using TEConsulting.Service;

namespace TEConsulting.API.Controllers
{
    [RoutePrefix("api/Orders")]
    public class POController : ApiController
    {
        OrderService service = new OrderService();
        LookupsService lookup = new LookupsService();

        [HttpGet]
        [Route("{empId}/{supId}")]
        [ResponseType(typeof(List<Order>))]
        public IHttpActionResult Get(int empId, int supId)
        {
            try
            {
                //get employee from login session
                //create sp get orders for employee
                //get 

                List<Order> orders = service.GetAllPendingOrdersForEmp(empId, supId);
                foreach(Order o in orders)
                {
                    o.Items = service.GetOrderItemsByPONumber(o.PONumber);
                }

                if (orders.Count == 0)
                    return NotFound();

                return Ok(orders);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPost]
        [Route("Details/{empId}")]
        [ResponseType(typeof(List<Order>))]
        public IHttpActionResult ProcessPORs([FromBody] SearchParam search)
        {
            try
            {

                DateTime? startDate = null;
                DateTime? endDate = null;

                if (!string.IsNullOrEmpty(search.start) && !string.IsNullOrEmpty(search.end))
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    startDate = DateTime.ParseExact(search.start, "yyyyMMdd", provider);
                    endDate = DateTime.ParseExact(search.end, "yyyyMMdd", provider);
                }

                List<Order> orders = service.GetAllPendingOrdersForEmp(search.empId, 1, search.poNumber, search.searchEmp, startDate, endDate, search.statusId);
                foreach (Order o in orders)
                {
                    o.Items = service.GetOrderItemsByPONumber(o.PONumber);
                }

                if (orders.Count == 0)
                    return NotFound();

                return Ok(orders);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpGet]
        [Route("Details/Item/{orderItemID}")]
        [ResponseType(typeof(OrderItem))]
        public IHttpActionResult GetOrderItemDetailsByID(int orderItemID)
        {
            try
            {
                OrderItem item = service.GetOrderItemDetailsByID(orderItemID);

                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpGet]
        [Route("SearchDate/{employeeId}/{start}/{end}")]
        [ResponseType(typeof(List<Order>))]
        public IHttpActionResult GetOrdersByDate(int employeeId, string start, string end)
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime startDate = DateTime.ParseExact(start, "yyyyMMdd", provider);
                DateTime endDate = DateTime.ParseExact(end, "yyyyMMdd", provider);
                List<Order> orders = service.GetOrdersByDate(employeeId, 
                    startDate, endDate);

                return Ok(orders);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create([FromBody] Order order)
        {
            try
            {
                service.AddOrder(order);

                if (order.Errors.Count != 0)
                    return Content(HttpStatusCode.BadRequest, order);

                service.UpdateOrderTotals(order);

                return Created($"{Request.RequestUri}/{order.PONumberFormatted}", order);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPut]
        [Route("Update/{PONumber}")]    
        public IHttpActionResult UpdateOrderItem(int PONumber, [FromBody] Order order)
        {
            try
            {
                OrderItem item = order.Items[order.Items.Count - 1];
                if (PONumber != item.PONumber)
                    return BadRequest();

                order.Items.RemoveAt(order.Items.Count - 1);

                //CHECK IF THE MODIFIED ITEM IS A DUPLICATE
                if (service.CheckForDupesV2(order, item))
                {
                    service.UpdateOrderTotals(order);
                    //ITERM MERGED MESSAGE TO FRONTEND
                }
                else
                {
                    int index = order.Items.FindIndex(i => i.OrderItemID == item.OrderItemID);
                    if(index >= 0)
                    {
                        order.Items[index] = item;
                        service.UpdateItems(order);
                        service.UpdateOrderTotals(order);
                    }
                    else
                    {
                        List<ValidationError> err = order.Errors;
                        foreach (OrderItem i in order.Items)
                        {
                            err.Concat(i.Errors);
                        }
                        return Content(HttpStatusCode.BadRequest, err);
                    }
                    
                }

                return Ok(order);

            }
            catch (SqlException ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPost]
        [Route("AddItem/{PONumber}")]
        public IHttpActionResult CreateOrderItem(int PONumber, [FromBody] Order order)
        {
            try
            {
                OrderItem item = order.Items[order.Items.Count - 1];
                if (PONumber != item.PONumber)
                    return BadRequest();

                //take item off order and see if its a dupe
                order.Items.RemoveAt(order.Items.Count - 1);

                if (service.CheckForDupes(order.Items, item))
                {
                    service.UpdateDupedItem(order.Items, item);
                   
                    service.UpdateOrderTotals(order);

                    return Ok();
                }
                else
                {
                    order.Items.Add(item);

                    if (service.AddItem(item))
                    {
                        service.UpdateOrderTotals(order);
                    }
                    else
                    {
                        List<ValidationError> err = order.Errors;
                        foreach (OrderItem i in order.Items)
                        {
                            err.Concat(i.Errors);
                        }
                        return Content(HttpStatusCode.BadRequest, err);
                    }

                    return Ok(order);
                }              
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPut]
        [Route("Details/Process/{PONumber}")]
        public IHttpActionResult ProcessPOR([FromBody] Order order)
        {
            try
            {
                foreach(OrderItem i in order.Items)
                {
                    if (!service.UpdateItem(i))
                    {
                        service.UpdateOrderTotals(order);
                        return Content(HttpStatusCode.BadRequest, "An error occured updating the item status.");
                    }
                }

                if (!service.CloseOrderStatus(order))
                {
                    service.UpdateOrderStatus(order);
                }


                return Ok(order);
                
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPut]
        [Route("Details/Close/{PONumber}")]
        public IHttpActionResult ClosePOR([FromBody] Order order)
        {
            try
            {
                order.StatusID = 5;
                service.UpdateOrderStatus(order);
                service.SendOrderProcessedEmail(order);



                return Ok(order);

            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

        [HttpPut]
        [Route("Need/{PONumber}")]
        public IHttpActionResult NoLongerNeeded([FromBody] Order order)
        {
            try
            {
                OrderItem item = order.Items[order.Items.Count - 1];

                order.Items.RemoveAt(order.Items.Count - 1);

                int originalIndex = order.Items.FindIndex(e => e.OrderItemID == item.OrderItemID);

                order.Items.RemoveAt(originalIndex);

                order.Items.Add(item);

                item.OrderItemPrice = 0.0m;
                item.OrderItemQty = 0;
                item.OrderItemDescription = "no longer needed";
                item.StatusID = 3;

                if (service.UpdateItem(item))
                {
                    service.UpdateOrderTotals(order);
                }
                else
                {
                    return BadRequest("Failed to update item");
                }

                //if all items are approved / denied
                //put under review

                if (!service.CloseOrderStatus(order))
                {
                    service.UpdateOrderStatus(order);
                }


                return Ok(order);

            }
            catch (SqlException ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occurred.  Please contact " +
                    "the system administrator.");
            }
        }

    }
}