using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEConsulting.Model;
using TEConsulting.Types;

namespace TEConsulting.Repository
{
    public class OrderRepo
    {
        #region Fields and Constructors

        DataAccess db = new DataAccess();

        #endregion

        #region Public Methods
        public bool InsertOrder(Order o)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            //ORDER
            parms.Add(new ParmStruct("@RecordVersion", o.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@PONumber", o.PONumber, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@EmployeeID", o.EmployeeID, SqlDbType.Int, 0)); 

            //FIRST ITEM
            parms.Add(new ParmStruct("@OrderItemID", o.Items[0].OrderItemID, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@OrderItemName", o.Items[0].OrderItemName, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemDescription", o.Items[0].OrderItemDescription, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemJustification", o.Items[0].OrderItemJustification, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemPrice", o.Items[0].OrderItemPrice, SqlDbType.Decimal));
            parms.Add(new ParmStruct("@OrderItemQty", o.Items[0].OrderItemQty, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@OrderItemLocation", o.Items[0].OrderItemLocation, SqlDbType.VarChar, 255));


            if (db.SendData("spAddPurchaseOrder", parms) > 0)
            {
                o.PONumber = (int)parms.Where(p => p.Name == "@PONumber").FirstOrDefault().Value;
                o.Items[0].OrderItemID = (int)parms.Where(p => p.Name == "@OrderItemID").FirstOrDefault().Value;
                o.Items[0].PONumber = (int)parms.Where(p => p.Name == "@PONumber").FirstOrDefault().Value;
                o.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool InsertItem(OrderItem i)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@OrderItemID", i.OrderItemID, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@RecordVersion", i.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.Output));

            parms.Add(new ParmStruct("@PONumber", i.PONumber, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@OrderItemName", i.OrderItemName, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemDescription", i.OrderItemDescription, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemJustification", i.OrderItemJustification, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemPrice", i.OrderItemPrice, SqlDbType.Decimal, 19));
            parms.Add(new ParmStruct("@OrderItemQty", i.OrderItemQty, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@OrderItemLocation", i.OrderItemLocation, SqlDbType.VarChar, 255));

            if (db.SendData("spInsertItem", parms) > 0)
            {
                i.OrderItemID = (int)parms.Where(p => p.Name == "@OrderItemID").FirstOrDefault().Value;
                i.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                return true;
            }

            return false;
          
        }

        public bool UpdateOrderTotals(Order o)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@PONumber", o.PONumber, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@subtotal", o.POSubtotal, SqlDbType.Decimal, 19));
            parms.Add(new ParmStruct("@tax", o.POTax, SqlDbType.Decimal, 19));
            parms.Add(new ParmStruct("@total", o.POTotal, SqlDbType.Decimal, 19));

            if (db.SendData("spUpdateTotals", parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateDupedQTY(int itemId, int qty)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@OrderItemID", itemId, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@OrderItemQty", qty, SqlDbType.Int, 0));

            if (db.SendData("spUpdateDupedQty", parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModifyItem(OrderItem i)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            //change status as well
            parms.Add(new ParmStruct("@OrderItemID", i.OrderItemID, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@RecordVersion", i.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@OrderItemName", i.OrderItemName, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemDescription", i.OrderItemDescription, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemJustification", i.OrderItemJustification, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@OrderItemPrice", i.OrderItemPrice, SqlDbType.Decimal, 19));
            parms.Add(new ParmStruct("@OrderItemQty", i.OrderItemQty, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@OrderItemLocation", i.OrderItemLocation, SqlDbType.VarChar, 255));
            parms.Add(new ParmStruct("@StatusID", i.StatusID, SqlDbType.Int, 0));
            parms.Add(new ParmStruct("@Reason", i.Reason, SqlDbType.VarChar, 255));

            if (db.SendData("spUpdateOrderItem", parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveMergedItem(OrderItem item)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@OrderItemID", item.OrderItemID, SqlDbType.Int, 0));

            if(db.SendData("spRemoveMergedItem", parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public List<Order> RetrievePendingByPONumber(int id, int PONumber)
        //{
        //    List<ParmStruct> parms = new List<ParmStruct>();
        //    parms.Add(new ParmStruct("@EmployeeID", id, SqlDbType.Int));
        //    parms.Add(new ParmStruct("@PONumber", PONumber, SqlDbType.Int));

        //    DataTable dt = db.GetData("spGetOrdersByPONumber", parms);

        //    List<Order> ordersList = new List<Order>();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        ordersList.Add(PopulateOrder(row));
        //    }

        //    return ordersList;      
        //}

        public List<Order> RetrieveAllPendingOrdersForEmp(int empId, int supId, int PONumber = 0, DateTime? start = null, DateTime? end = null)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeID", empId, SqlDbType.Int));

            if(PONumber != 0)
            {
                parms.Add(new ParmStruct("@PONumber", PONumber, SqlDbType.Int));
            }

            if (supId == 10000000)
            {
                parms.Add(new ParmStruct("@SupervisorID", supId, SqlDbType.Int));
                parms.Add(new ParmStruct("@Start", start, SqlDbType.DateTime));
                parms.Add(new ParmStruct("@End", end, SqlDbType.DateTime));

                DataTable supDt = db.GetData("spGetAllPendingOrdersForEmp", parms);

                List<Order> supOrdersList = new List<Order>();

                foreach (DataRow row in supDt.Rows)
                {
                    supOrdersList.Add(PopulateSupOrder(row));
                }

                return supOrdersList;
            }
            else
            {
                parms.Add(new ParmStruct("@Start", start, SqlDbType.DateTime));
                parms.Add(new ParmStruct("@End", end, SqlDbType.DateTime));

                DataTable dt = db.GetData("spGetAllPendingOrdersForEmp", parms);

                List<Order> ordersList = new List<Order>();

                foreach (DataRow row in dt.Rows)
                {
                    ordersList.Add(PopulateOrder(row));
                }

                return ordersList;
            }  
        }

        public List<Order> RetrieveProcessPORs(int empId, int PONumber, string searchEmp, DateTime? start, DateTime? end, int statusId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeID", empId, SqlDbType.Int));
            parms.Add(new ParmStruct("@StatusID", statusId, SqlDbType.Int));
            parms.Add(new ParmStruct("@PONumber", PONumber, SqlDbType.Int));
            parms.Add(new ParmStruct("@SearchEmp", searchEmp, SqlDbType.VarChar, 255));

            parms.Add(new ParmStruct("@Start", start, SqlDbType.DateTime));
            parms.Add(new ParmStruct("@End", end, SqlDbType.DateTime));
            

            DataTable dt = db.GetData("spGetProcessPORs", parms);

            List<Order> ordersList = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                ordersList.Add(PopulateSupOrder(row));
            }

            return ordersList;

        }

        public List<Order> RetrieveByDate(int id, DateTime start, DateTime end)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@EmployeeID", id, SqlDbType.Int),
                new ParmStruct("@Start", start, SqlDbType.DateTime),
                new ParmStruct("@End", end, SqlDbType.DateTime)
            };

            DataTable dt = db.Execute("spGetOrdersByDate", parms);

            List<Order> orders = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                orders.Add(PopulateOrder(row));
            }

            return orders;

        }

        public List<OrderItem> RetrieveItemsList(int PONumber)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@PONumber", PONumber, SqlDbType.Int),
            };

            DataTable dt = db.Execute("spGetItemsList", parms);

            List<OrderItem> items = new List<OrderItem>();

            foreach (DataRow row in dt.Rows)
            {
                items.Add(PopulateOrderItem(row));
            }

            return items;
        }

        public List<OrderItem> RetrieveOrderItemDetailsByID(int orderItemID)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@OrderItemID", orderItemID, SqlDbType.Int),
            };

            DataTable dt = db.Execute("spGetOrderItemDetailsByID", parms);

            List<OrderItem> items = new List<OrderItem>();

            foreach (DataRow row in dt.Rows)
            {
                items.Add(PopulateOrderItem(row));
            }

            return items;
        }

        public bool UpdateOrderStatus(Order o)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@PONumber", o.PONumber, SqlDbType.Int),
                new ParmStruct("@StatusID", o.StatusID, SqlDbType.Int)
            };

            if(db.SendData("spUpdateOrderStatus", parms) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Private Methods
        private Order PopulateSupOrder(DataRow row)
        {
            return new Order
            {
                PONumber = Convert.ToInt32(row["PONumber"]),
                StatusID = Convert.ToInt32(row["StatusID"]),
                POCreationDate = Convert.ToDateTime(row["POCreationDate"]),
                POSubtotal = Convert.ToDecimal(row["POSubtotal"]),
                POTax = Convert.ToDecimal(row["POTax"]),
                POTotal = Convert.ToDecimal(row["POTotal"]),
                EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                RecordVersion = (byte[])row["RecordVersion"],
                EmployeeName = row["Employee"].ToString()
            };
        }

        private Order PopulateOrder(DataRow row)
        {
            return new Order
            {
                PONumber = Convert.ToInt32(row["PONumber"]),
                StatusID = Convert.ToInt32(row["StatusID"]),
                POCreationDate = Convert.ToDateTime(row["POCreationDate"]),
                POSubtotal = Convert.ToDecimal(row["POSubtotal"]),
                POTax = Convert.ToDecimal(row["POTax"]),
                POTotal = Convert.ToDecimal(row["POTotal"]),
                EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                RecordVersion = (byte[])row["RecordVersion"]
            };          
        }

        private OrderItem PopulateOrderItem(DataRow row)
        {
            return new OrderItem
            {
                OrderItemID = Convert.ToInt32(row["OrderItemID"]),
                PONumber = Convert.ToInt32(row["PONumber"]),
                OrderItemQty = Convert.ToInt32(row["OrderItemQty"]),
                OrderItemName = row["OrderItemName"].ToString(),
                OrderItemDescription = row["OrderItemDescription"].ToString(),
                OrderItemJustification = row["OrderItemJustification"].ToString(),
                OrderItemPrice = Convert.ToDecimal(row["OrderItemPrice"]),
                OrderItemLocation = row["OrderItemLocation"].ToString(),
                StatusID = Convert.ToInt32(row["StatusID"]),
                RecordVersion = (byte[])row["RecordVersion"]
            };
        }

        #endregion
    }
}
