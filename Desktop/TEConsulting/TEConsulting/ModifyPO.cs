using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEConsulting.Model;
using TEConsulting.Service;

namespace TEConsulting
{
    public partial class ModifyPO : Form
    {
        public static Employee emp = MainMenu.Employee;
        //public static LookupsService lookup = new LookupsService();
        private List<Order> _ordersList;
        private Order _selectedOrder = new Order();
        private int flag = 0;
        private bool sup;

        public ModifyPO()
        {
            InitializeComponent();
        }

        private void ModifyPO_Load(object sender, EventArgs e)
        {
            OrderService service = new OrderService();
            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;
            txtSearch.Enabled = false;
            groupBox1.Enabled = false;

            //format orders dgv
            dgvOrders.Columns["Date"].DefaultCellStyle.Format = "dd'/'MM'/'yyyy";
            dgvOrders.AutoGenerateColumns = false;
            this.dgvOrders.DefaultCellStyle.ForeColor = Color.Black;

            //format items dgv
            dgvItems.Columns["Price"].DefaultCellStyle.Format = "c";

            dgvItems.AutoGenerateColumns = false;
            this.dgvItems.DefaultCellStyle.ForeColor = Color.Black;

            lblSubtotal.Text = (0.0m).ToString("C");
            lblTax.Text = (0.0m).ToString("C");
            lblTotal.Text = (0.0m).ToString("C");

            if(emp.SupervisorId == 10000000)
            {
                sup = true;
                dgvOrders.Columns["Employee"].Visible = true;
                lblReason.Visible = true;
                txtReason.Visible = true;
            }

            ReloadOrdersDGV();

        }

        #region CRUD

        //MODIFY ITEMS: if not processed (approved/denied), ALL FIELDS  may be modified and may add new items
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                OrderService service = new OrderService();

                dgvOrders.DataSource = null;
                dgvItems.DataSource = null;

                if (rdoPO.Checked && txtSearch.Text.Trim() != string.Empty)
                {
                    dtpStart.Value = DateTime.Now;
                    dtpEnd.Value = DateTime.Now;

                    string searchString = txtSearch.Text.Trim();

                    if (!int.TryParse(searchString, out _))
                    {
                        MessageBox.Show("Please enter a numeric PO Number");
                    }

                    int PONumberSearch = Convert.ToInt32(searchString.Trim());

                    //if supervisor get orders of supervisor and all employees
                    _ordersList = service.GetAllPendingOrdersForEmp(emp.EmployeeId, emp.SupervisorId, PONumberSearch);
                    if(_ordersList.Count == 0)
                    {
                        MessageBox.Show("No results found");
                    }

                    PONumber.DataPropertyName = "PONumberFormatted";
                    Date.DataPropertyName = "POCreationDate";
                    Employee.DataPropertyName = "EmployeeName";
                    dgvOrders.DataSource = _ordersList;

                    foreach (DataGridViewRow order in dgvOrders.Rows)
                    {
                        Order rowData = order.DataBoundItem as Order;
                        if (rowData != null)
                        {
                            if (rowData.EmployeeName == "")
                            {
                                rowData.EmployeeName = emp.FullName;
                            }
                        }
                    }

                    
                }

                if (rdoDates.Checked)
                {
                    DateTime startDate = dtpStart.Value;
                    DateTime endDate = dtpEnd.Value;
                    txtSearch.Text = string.Empty;

                    _ordersList = service.GetAllPendingOrdersForEmp(emp.EmployeeId, emp.SupervisorId, 0, null, startDate, endDate);

                    if (_ordersList.Count == 0)
                    {
                        MessageBox.Show("No results found");
                    }

                    PONumber.DataPropertyName = "PONumberFormatted";
                    Date.DataPropertyName = "POCreationDate";
                    Employee.DataPropertyName = "EmployeeName";
                    dgvOrders.DataSource = _ordersList;

                    foreach (DataGridViewRow order in dgvOrders.Rows)
                    {
                        Order rowData = order.DataBoundItem as Order;
                        if (rowData != null)
                        {
                            if (rowData.EmployeeName == "")
                            {
                                rowData.EmployeeName = emp.FullName;
                            }
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ResetItemForm();
                OrderService service = new OrderService();

                if (dgvOrders.SelectedCells.Count > 0)
                {
                    _selectedOrder = (Order)dgvOrders.CurrentRow.DataBoundItem;
                    _selectedOrder.Items = service.GetOrderItemsByPONumber(_selectedOrder.PONumber);

                    dgvItems.AutoGenerateColumns = false;
                    Qty.DataPropertyName = "OrderItemQty";
                    ItemName.DataPropertyName = "OrderItemName";
                    Desc.DataPropertyName = "OrderItemDescription";
                    Price.DataPropertyName = "OrderItemPrice";
                    Just.DataPropertyName = "OrderItemJustification";
                    Loc.DataPropertyName = "OrderItemLocation";

                    if (sup == false && _selectedOrder.StatusID == 4)
                    {
                        foreach (OrderItem i in _selectedOrder.Items)
                        {
                            if (i.StatusID != 1)
                            {
                                i.Unknown = "";
                            }
                            else
                            {
                                i.Unknown = i.Status;
                            }
                        }

                        Status.DataPropertyName = "Unknown";
                    }
                    else
                    {
                        Status.DataPropertyName = "Status";
                    }
                                       
                    
                    dgvItems.DataSource = _selectedOrder.Items;

                    lblSubtotal.Text = _selectedOrder.POSubtotal.ToString("C");
                    lblTax.Text = _selectedOrder.POTax.ToString("C");
                    lblTotal.Text = _selectedOrder.POTotal.ToString("C");

                    //foreach (DataGridViewRow i in dgvItems.Rows)
                    //{
                    //    OrderItem rowData = i.DataBoundItem as OrderItem;
                    //    if (rowData != null)
                    //    {
                    //        if (rowData.StatusID == 2 || rowData.StatusID == 3)
                    //        {
                    //            rowData.Status = "";
                    //        }
                    //    }
                    //}


                    //foreach (DataGridViewRow item in dgvItems.Rows)
                    //{
                    //    OrderItem rowData = item.DataBoundItem as OrderItem;
                    //    if (rowData != null)
                    //    {
                    //        if (rowData.StatusID != 1)
                    //        {
                    //            item.ReadOnly = true;
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.Rows.Count == 0)
                {
                    MessageBox.Show("You must retrieve an order first.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(_selectedOrder.EmployeeID != emp.EmployeeId)
                {
                    MessageBox.Show("You cannot add items to an order that does not belong to you.");
                    return;
                }
                if(_selectedOrder.StatusID == 5 || _selectedOrder.StatusID == 4)
                {
                    MessageBox.Show("You can only add items to a pending order.");
                    return;
                }

                ResetItemForm();
                btnSubmit.Text = "Add Item";
                btnUpdate.Enabled = false;
                groupBox1.Enabled = true;
                flag = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.Rows.Count == 0)
                {
                    MessageBox.Show("You must retrieve an order first.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dgvItems.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select item to update");
                    return;
                }
                if(_selectedOrder.StatusID != 1)
                {
                    MessageBox.Show("You can only update items on a pending order.");
                    return;
                }
                if(sup == true)
                {
                    txtItemName.Enabled = false;
                    txtDesc.Enabled = false;
                    txtJust.Enabled = false;
                    txtReason.Enabled = true;
                }

                OrderService service = new OrderService();

                OrderItem selectedItem = dgvItems.SelectedRows[0].DataBoundItem as OrderItem;
                if (!service.CanEditItem(selectedItem))
                {
                    MessageBox.Show("Only items that are pending can be edited");
                    return;
                }

                nudQty.Value = Convert.ToDecimal(dgvItems.SelectedRows[0].Cells[0].Value);
                txtItemName.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[1].Value);
                txtDesc.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[2].Value);
                txtPrice.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[3].Value);
                txtJust.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[4].Value);
                txtLoc.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[5].Value);

                btnSubmit.Text = "Submit Changes";
                btnAddItem.Enabled = false;
                groupBox1.Enabled = true;
                flag = 2;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                OrderService service = new OrderService();
                //ADD ITEM
                if (flag == 1)
                {
                    //check to ensure order is not closed
                    string errMsg = ValidateAdd();

                    if (errMsg != string.Empty)
                    {
                        MessageBox.Show(errMsg, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        OrderItem newItem = PopulateItemObject();

                        if (service.CheckForDupes(_selectedOrder.Items, newItem))
                        {
                            service.UpdateDupedItem(_selectedOrder.Items, newItem);
                        }
                        else
                        {
                            newItem.PONumber = _selectedOrder.PONumber;
                            _selectedOrder.Items.Add(newItem);
                            service.AddItem(newItem);
                        }

                        ResetItemForm();

                        dgvItems.DataSource = null;
                        dgvItems.DataSource = _selectedOrder.Items;

                        UpdateTotals();

                    }
                }
                //UPDATE ITEM
                else if (flag == 2)
                {
                    //CHECK IF MODIFIED ITEM IS DUPLICATE
                    string errMsg = ValidateAdd();
                    if (errMsg != string.Empty)
                    {
                        MessageBox.Show(errMsg, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        
                        //Get new item
                        OrderItem updatedItem = PopulateItemObject();

                        //Set same ID
                        OrderItem selectedItem = dgvItems.SelectedRows[0].DataBoundItem as OrderItem;
                        updatedItem.OrderItemID = selectedItem.OrderItemID;
                        updatedItem.RecordVersion = selectedItem.RecordVersion;

                        if(sup == true)
                        {
                            if (_selectedOrder.EmployeeID != emp.EmployeeId && txtReason.Text.Trim() != string.Empty)
                            {
                                updatedItem.Reason = txtReason.Text.Trim();
                            }
                            else
                            {
                                MessageBox.Show("You must enter a reason to update your employee's POR");
                                return;
                            }
                        }
                        

                        //check dupes and merge
                        if (service.CheckForDupesV2(_selectedOrder, updatedItem))
                        {
                            MessageBox.Show("Duplicate item merged");

                        }
                        //replace with updated
                        else
                        {
                            //get index
                            int index = _selectedOrder.Items.FindIndex(i => i.OrderItemID == updatedItem.OrderItemID);
                            if (index >= 0)
                            {
                                //replace item
                                _selectedOrder.Items[index] = updatedItem;
                                service.UpdateItems(_selectedOrder);
                            }
                        }

                        UpdateTotals();
                        dgvItems.DataSource = typeof(List<OrderItem>);
                        dgvItems.DataSource = _selectedOrder.Items;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnNotNeed_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedOrder.EmployeeID != emp.EmployeeId)
                {
                    MessageBox.Show("You cannot perform this action on an order item that does not belong to you.");
                    return;
                }

                //get item from dgv where it matches in 
                OrderService service = new OrderService();
                OrderItem selectedItem = dgvItems.SelectedRows[0].DataBoundItem as OrderItem;
                if(selectedItem.StatusID != 1)
                {
                    MessageBox.Show("You can only edit items that are pending.");
                    return;
                }
                selectedItem.OrderItemPrice = 0.0m;
                selectedItem.OrderItemQty = 0;
                selectedItem.OrderItemDescription = "no longer needed";
                selectedItem.StatusID = 3;

                if (service.UpdateItem(selectedItem))
                {
                    UpdateTotals();
                    dgvItems.DataSource = typeof(List<OrderItem>);
                    dgvItems.DataSource = _selectedOrder.Items;
                }
                else
                {
                    MessageBox.Show("Update failed");
                }

                //if all items are approved / denied
                //put under review

                if (!service.CloseOrderStatus(_selectedOrder))
                {
                    service.UpdateOrderStatus(_selectedOrder);
                }
                else
                {

                    _selectedOrder.StatusID = 5;
                    service.UpdateOrderStatus(_selectedOrder);
                    service.SendOrderProcessedEmail(_selectedOrder);
                    MessageBox.Show("POR has been closed");
                    
                }

                ReloadOrdersDGV();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        #endregion

        #region Validation
        public string ValidateAdd()
        {
            string errMsg = string.Empty;

            if (txtItemName.Text == string.Empty || !ValidLength(txtItemName.Text, 50))
            {
                errMsg = errMsg += Environment.NewLine + "Item name cannot be blank or more than 50 characters.";
            }

            if (txtDesc.Text == string.Empty || !ValidLength(txtDesc.Text, 100))
            {
                errMsg = errMsg += Environment.NewLine + "Description cannot be blank or more than 100 characters.";
            }

            if (txtJust.Text == string.Empty || !ValidLength(txtJust.Text, 80))
            {
                errMsg = errMsg += Environment.NewLine + "Justification cannot be blank or more than 80 characters.";
            }

            if (txtLoc.Text == string.Empty || !ValidLength(txtLoc.Text, 50))
            {
                errMsg = errMsg += Environment.NewLine + "Location cannot be blank or more than 50 characters.";
            }

            if (nudQty.Value == 0)
            {
                errMsg = errMsg += Environment.NewLine + "Quantity must be greater than 0.";
            }

            if (!decimal.TryParse(txtPrice.Text, out _) || Convert.ToDecimal(txtPrice.Text) <= 0)
            {
                errMsg = errMsg += Environment.NewLine + "Price must be greater than 0.";
            }

            return errMsg;
        }

        public bool ValidLength(string value, int length)
        {
            if (value.Length > length)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Housekeeping and Helpers

        private OrderItem PopulateItemObject()
        {
            return new OrderItem
            {
                OrderItemName = txtItemName.Text.Trim(),
                OrderItemDescription = txtDesc.Text.Trim(),
                OrderItemJustification = txtJust.Text.Trim(),
                OrderItemPrice = Convert.ToDecimal(txtPrice.Text.Trim()),
                OrderItemQty = Convert.ToInt32(nudQty.Value),
                OrderItemLocation = txtLoc.Text.Trim(),
                StatusID = 1
            };
        }

        public void UpdateTotals()
        {
            OrderService service = new OrderService();
            service.UpdateOrderTotals(_selectedOrder);
            lblSubtotal.Text = _selectedOrder.POSubtotal.ToString("C");
            lblTax.Text = _selectedOrder.POTax.ToString("C");
            lblTotal.Text = _selectedOrder.POTotal.ToString("C");
        }

        public void ResetItemForm()
        {
            foreach (Control ctr in groupBox1.Controls)
            {
                if (ctr is TextBox)
                {
                    ctr.Text = "";
                }

                NumericUpDown nud = ctr as NumericUpDown;

                if (nud != null)
                {
                    nud.Value = nud.Minimum;
                }
            }

            groupBox1.Enabled = false;
            btnAddItem.Enabled = true;
            btnUpdate.Enabled = true;

        }

        private void rdoPO_CheckedChanged(object sender, EventArgs e)
        {
            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;
            txtSearch.Enabled = true;
        }

        private void rdoDates_CheckedChanged(object sender, EventArgs e)
        {
            dtpStart.Enabled = true;
            dtpEnd.Enabled = true;
            txtSearch.Enabled = false;
        }

        private void ReloadOrdersDGV()
        {
            OrderService service = new OrderService();
            PONumber.DataPropertyName = "PONumberFormatted";
            Date.DataPropertyName = "POCreationDate";
            Employee.DataPropertyName = "EmployeeName";
            OrderStatus.DataPropertyName = "Status";
            dgvOrders.DataSource = service.GetAllPendingOrdersForEmp(emp.EmployeeId, emp.SupervisorId, 0, null, null);

            foreach (DataGridViewRow order in dgvOrders.Rows)
            {
                Order rowData = order.DataBoundItem as Order;
                if (rowData != null)
                {
                    if (rowData.EmployeeName == "")
                    {
                        rowData.EmployeeName = emp.FullName;
                    }
                }
            }
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }
        #endregion

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ReloadOrdersDGV();
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            ResetItemForm();
        }
        
    }
}
