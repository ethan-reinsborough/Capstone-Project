using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TEConsulting.Model;
using TEConsulting.Repository;
using TEConsulting.Types;

namespace TEConsulting.Service
{
    public class OrderService
    {

        #region Fields and Constructors
        private OrderRepo repo = new OrderRepo();

        #endregion

        #region Public Methods

        public bool AddOrder(Order o)
        {
            if (ValidateOrder(o))
                return repo.InsertOrder(o);

            return false;
        }

        public bool AddItem(OrderItem item)
        {
            if (ValidateItem(item))
                return repo.InsertItem(item);

            return false;
        }

        public string UpdateItems(Order order)
        {
            string msg = "";
            foreach(OrderItem i in order.Items)
            {
                ValidateItem(i);
                foreach(ValidationError e in i.Errors)
                {
                    msg += e.Description + Environment.NewLine;
                }
            }

            if(msg == string.Empty)
            {
                foreach(OrderItem i in order.Items)
                {
                    if (!repo.ModifyItem(i))
                    {
                        msg += "Update failed on item: " + i.OrderItemName;
                    }
                }
            }

            return msg;
        }

        public bool UpdateItem(OrderItem item)
        {
           return repo.ModifyItem(item);
        }

        public List<Order> GetAllPendingOrdersForEmp(int empId, int supId = 1, 
            int PONumber = 0, string searchEmp = "", DateTime? start = null, DateTime? end = null, int statusId = 0)
        {
            if(statusId == 0)
            {
                return repo.RetrieveAllPendingOrdersForEmp(empId, supId, PONumber, start, end);
            }
            else
            {
                return repo.RetrieveProcessPORs(empId, PONumber, searchEmp, start, end, statusId);
            }
            
        }

        public List<Order> GetOrdersByDate(int employeeId, DateTime start, DateTime end)
        {
            return repo.RetrieveByDate(employeeId, start, end);
        }

        public List<OrderItem> GetOrderItemsByPONumber(int PONumber)
        {
            return repo.RetrieveItemsList(PONumber);
        }

        public OrderItem GetOrderItemDetailsByID(int orderItemID)
        {
            return repo.RetrieveOrderItemDetailsByID(orderItemID)[0];
        }
        
        public bool CloseOrderStatus(Order o)
        {
            bool closecheck = false;
            int count = o.Items.Count();
            int pending = o.Items.Count(s => s.StatusID == 1);
            int approved = o.Items.Count(s => s.StatusID == 2);
            int denied = o.Items.Count(s => s.StatusID == 3);

            if(count == pending)
            {
                //all items pending
                //order back to pending
                o.StatusID = 1;
            }
         
            if(approved > 0 || denied > 0)
            {
                //at least one item pending
                o.StatusID = 4;
            }

           
            if(approved + denied == count)
            {
                //all items check
                //ask for close
                closecheck = true;
            }

            return closecheck;
            
        }

        public bool UpdateOrderStatus(Order o)
        {
            if (repo.UpdateOrderStatus(o))
                return true;

            return false;
        }

        public void SendOrderProcessedEmail(Order o)
        {
            LookupsRepo lookup = new LookupsRepo();
            SearchEmployeeLookupDTO emp = lookup.SearchEmployeeById(o.EmployeeID);

            string body = "<table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 400 + ">" +
              "<tr>" +
               "<td>Qty</td>" +
               "<td>Name</td>" +
               "<td>Status</td>" +
               "<td>Price</td>" +
              "</tr>";

            foreach (OrderItem i in o.Items)
            {
                body += "<tr><td>" + i.OrderItemQty.ToString() + "</td>" +
                        "<td>" + i.OrderItemName + "</td>" +
                        "<td>" + i.Status + "</td>" +
                        "<td>" + i.OrderItemPrice.ToString("C") + "</td></tr>";
            }

            body += "</table>";

            string link = "http://localhost:4200/orders/";
            link += o.PONumber.ToString();



            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(emp.Email);
            //ADD LINK TO WEBSITE ORDER           

            mailMessage.From = new MailAddress("admin@teconsulting.com");
            mailMessage.Subject = "Purchase Order Request: " + o.PONumberFormatted + " - Closed";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "Hello " + emp.FirstName + " " + emp.LastName +
                "<br/>Your purchase order request (PO Number: " + o.PONumberFormatted + ") has been processed." +
                "<br/>Date: " + o.POCreationDate.ToString("dddd, dd MMMM yyyy") +
                "<br/>Subtotal: " + o.POSubtotal.ToString("C") +
                "<br/>Tax: " + o.POTax.ToString("C") +
                "<br/>Total Cost: " + o.POTotal.ToString("C") +
                "<hr><br/>" + body + "</br><a href=\""
                + link + "\">Link To Order</a>";
                

            SmtpClient smtpClient = new SmtpClient("localhost");
            smtpClient.Send(mailMessage);

            return;
        }

        public (decimal, decimal, decimal) UpdateOrderTotals(Order o)
        {
            decimal TAX_RATE = 0.15m;
            o.POSubtotal = 0.0m;
            
            foreach(OrderItem i in o.Items)
            {
                o.POSubtotal += i.OrderItemPrice * i.OrderItemQty;
            }
            
            o.POTax = o.POSubtotal * TAX_RATE;
            o.POTotal = o.POSubtotal + o.POTax;

            repo.UpdateOrderTotals(o);

            Decimal.Round(o.POSubtotal, 2);
            Decimal.Round(o.POTax, 2);
            Decimal.Round(o.POTotal, 2);

            return (o.POSubtotal, o.POTax, o.POTotal);
        }

        #endregion

        #region Validation

        private bool ValidateOrder(Order order)
        {
            ValidationContext context = new ValidationContext(order);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(order, context, results, true);

            foreach (ValidationResult e in results)
            {
                order.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return order.Errors.Count == 0;
        }
        private bool ValidateItem(OrderItem item)
        {
            ValidationContext context = new ValidationContext(item);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(item, context, results, true);

            foreach (ValidationResult e in results)
            {
                item.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return item.Errors.Count == 0;
        }

        #endregion

        #region Business Rules

        public bool CheckForDupesV2(Order o, OrderItem newItem)
        {
            List<OrderItem> testList = new List<OrderItem>();
            foreach(OrderItem i in o.Items)
            {
                testList.Add(i);
            }

            testList.RemoveAll(p => p.OrderItemID == newItem.OrderItemID);

            bool dupes = testList.Any(
                i => i.OrderItemName == newItem.OrderItemName &&
                i.OrderItemDescription == newItem.OrderItemDescription &&
                i.OrderItemJustification == newItem.OrderItemJustification &&
                i.OrderItemLocation == newItem.OrderItemLocation &&
                i.OrderItemPrice == newItem.OrderItemPrice
            );

            if (dupes)
            {
                
                int itemIndex = testList.FindIndex(
                    i => i.OrderItemName == newItem.OrderItemName &&
                    i.OrderItemDescription == newItem.OrderItemDescription &&
                    i.OrderItemJustification == newItem.OrderItemJustification &&
                    i.OrderItemLocation == newItem.OrderItemLocation &&
                    i.OrderItemPrice == newItem.OrderItemPrice
                );

                //update item in list
                testList[itemIndex].OrderItemQty += newItem.OrderItemQty;
             
                //Update Quantity in DB
                repo.UpdateDupedQTY(testList[itemIndex].OrderItemID, testList[itemIndex].OrderItemQty);

                OrderItem itemToRemove = o.Items.Find(p => p.OrderItemID == newItem.OrderItemID);
                repo.RemoveMergedItem(itemToRemove);
                o.Items.RemoveAll(p => p.OrderItemID == newItem.OrderItemID);

                return true;
            }
            else
            {
                return false;
            }     
        }

        public bool CanEditItem(OrderItem item)
        {
            if(item.StatusID == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckForDupes(List<OrderItem> items, OrderItem item)
        {
            return items.Any(
                i => i.OrderItemName == item.OrderItemName &&
                i.OrderItemDescription == item.OrderItemDescription &&
                i.OrderItemJustification == item.OrderItemJustification &&
                i.OrderItemLocation == item.OrderItemLocation &&
                i.OrderItemPrice == item.OrderItemPrice
            );
        }

        public void UpdateDupedItem(List<OrderItem> items, OrderItem item)
        {
            int itemIndex = items.FindIndex(
                i => i.OrderItemName == item.OrderItemName &&
                i.OrderItemDescription == item.OrderItemDescription &&
                i.OrderItemJustification == item.OrderItemJustification &&
                i.OrderItemLocation == item.OrderItemLocation &&
                i.OrderItemPrice == item.OrderItemPrice
            );

            //update item in list
            items[itemIndex].OrderItemQty += item.OrderItemQty;

            //Update Quantity in DB
            repo.UpdateDupedQTY(items[itemIndex].OrderItemID, items[itemIndex].OrderItemQty);

        }

        #endregion
    }
}
