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
    public partial class ProcessPO : Form
    {
        public static Employee emp = MainMenu.Employee;
        private List<Order> _ordersList;
        private Order _selectedOrder = new Order();
        private OrderItem _selectedItem = new OrderItem();

        public ProcessPO()
        {
            InitializeComponent();
        }

        private void ProcessPO_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<int, string>> opts = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Pending"),
                new KeyValuePair<int, string>(5, "Closed"),
                new KeyValuePair<int, string>(10, "All")
            };

            cmbStatus.DataSource = opts;
            cmbStatus.ValueMember = "Key";
            cmbStatus.DisplayMember = "Value";

            OrderService service = new OrderService();

            //format orders dgv
            dgvOrders.Columns["Date"].DefaultCellStyle.Format = "dd'/'MM'/'yyyy";
            dgvOrders.AutoGenerateColumns = false;
            this.dgvOrders.DefaultCellStyle.ForeColor = Color.Black;

            //format items dgv
            dgvItems.Columns["Price"].DefaultCellStyle.Format = "c";

            dgvItems.AutoGenerateColumns = false;
            this.dgvItems.DefaultCellStyle.ForeColor = Color.Black;

            ResetItemForm();

            lblSubtotal.Text = (0.0m).ToString("C");
            lblTax.Text = (0.0m).ToString("C");
            lblTotal.Text = (0.0m).ToString("C");

            ReloadOrdersDGV();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                OrderService service = new OrderService();

                int searchPO = 0;
                string searchEmp = "";
                DateTime? start = null;
                DateTime? end = null;
                int status = 0;


                dgvOrders.DataSource = null;
                dgvItems.DataSource = null;

                if (int.TryParse(txtSearchPO.Text.Trim(), out int _))
                {
                    searchPO = Convert.ToInt32(txtSearchPO.Text.Trim());
                }
                
               
                searchEmp = txtSearchEmp.Text.Trim();
                
                
                status = (int)cmbStatus.SelectedValue;

                if(rdoDates.Checked)
                {

                    start = dtpStart.Value
                        .AddHours(
                        dtpStart.Value.Hour * -1)
                        .AddMinutes(
                        dtpStart.Value.Minute * -1)
                        .AddSeconds(
                        dtpStart.Value.Second * -1);


                    end = dtpEnd.Value
                        .AddHours(
                        dtpEnd.Value.Hour + 23)
                        .AddMinutes(
                        dtpEnd.Value.Minute + 59)
                        .AddSeconds(
                        dtpEnd.Value.Second + 59);
                }
                

                _ordersList = service.GetAllPendingOrdersForEmp(emp.EmployeeId, 1, searchPO, searchEmp, start, end, status);

                if (_ordersList.Count == 0)
                {
                    MessageBox.Show("No results found");
                    return;
                }
                else
                {
                    groupBox1.Enabled = true;
                    PONumber.DataPropertyName = "PONumberFormatted";
                    Date.DataPropertyName = "POCreationDate";
                    Employee.DataPropertyName = "EmployeeName";
                    OrderStatus.DataPropertyName = "Status";

                    dgvOrders.DataSource = _ordersList;
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

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
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
                    Status.DataPropertyName = "Status";

                    dgvItems.DataSource = null;
                    dgvItems.DataSource = _selectedOrder.Items;

                    lblSubtotal.Text = _selectedOrder.POSubtotal.ToString("C");
                    lblTax.Text = _selectedOrder.POTax.ToString("C");
                    lblTotal.Text = _selectedOrder.POTotal.ToString("C");

                    //if(_selectedOrder.Status != )

                    btnProcess.Enabled = true;
                    groupBox1.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            btnProcess.Enabled = true;
            btnApprove.Enabled = false;
            btnDeny.Enabled = false ;
            btnPending.Enabled = false;    
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (_selectedOrder.StatusID == 5)
            {
                MessageBox.Show("You cannot process an order that is already closed");
                return;
            }

            nudQty.Value = Convert.ToDecimal(dgvItems.SelectedRows[0].Cells[0].Value);
            txtItemName.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[1].Value);
            txtDesc.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[2].Value);
            txtPrice.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[3].Value);
            txtJust.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[4].Value);
            txtLoc.Text = Convert.ToString(dgvItems.SelectedRows[0].Cells[5].Value);

            nudQty.Enabled = true;
            txtPrice.Enabled = true;
            txtLoc.Enabled = true;
            txtReason.Enabled = true;

            groupBox1.Enabled = true;
            btnApprove.Enabled = true;
            btnDeny.Enabled = true;
            btnPending.Enabled = true;


        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                OrderService service = new OrderService();
                _selectedItem = dgvItems.SelectedRows[0].DataBoundItem as OrderItem;
                _selectedItem.OrderItemQty = Convert.ToInt32(nudQty.Value);
                _selectedItem.OrderItemPrice = Convert.ToDecimal(txtPrice.Text.Trim());
                _selectedItem.OrderItemLocation = txtLoc.Text.Trim();

                _selectedItem.Reason = txtReason.Text.Trim();
                _selectedItem.StatusID = 2;

                if (!service.UpdateItem(_selectedItem))
                {
                    MessageBox.Show("Status update failed");
                    return;
                }

                UpdateTotals();

                CheckOrderStatus();

                dgvOrders.DataSource = typeof(List<Order>);
                dgvOrders.DataSource = _ordersList;
                dgvItems.DataSource = typeof(List<OrderItem>);
                dgvItems.DataSource = _selectedOrder.Items;

                ResetItemForm();

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

        private void btnDeny_Click(object sender, EventArgs e)
        {
            try
            {
                OrderService service = new OrderService();
                _selectedItem = dgvItems.SelectedRows[0].DataBoundItem as OrderItem;
                _selectedItem.OrderItemQty = Convert.ToInt32(nudQty.Value);
                _selectedItem.OrderItemPrice = Convert.ToDecimal(txtPrice.Text.Trim());
                _selectedItem.OrderItemLocation = txtLoc.Text.Trim();

                _selectedItem.Reason = txtReason.Text.Trim();
                _selectedItem.StatusID = 3;

                if (!service.UpdateItem(_selectedItem))
                {
                    MessageBox.Show("Status update failed");
                    return;
                }

                UpdateTotals();

                CheckOrderStatus();

                dgvOrders.DataSource = typeof(List<Order>);
                dgvOrders.DataSource = _ordersList;
                dgvItems.DataSource = typeof(List<OrderItem>);
                dgvItems.DataSource = _selectedOrder.Items;

                ResetItemForm();

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

        private void btnPending_Click(object sender, EventArgs e)
        {
            try
            {
                OrderService service = new OrderService();
                _selectedItem = dgvItems.SelectedRows[0].DataBoundItem as OrderItem;
                _selectedItem.OrderItemQty = Convert.ToInt32(nudQty.Value);
                _selectedItem.OrderItemPrice = Convert.ToDecimal(txtPrice.Text.Trim());
                _selectedItem.OrderItemLocation = txtLoc.Text.Trim();

                _selectedItem.Reason = txtReason.Text.Trim();
                _selectedItem.StatusID = 1;

                if (!service.UpdateItem(_selectedItem))
                {
                    MessageBox.Show("Status update failed");
                    return;
                }

                UpdateTotals();

                CheckOrderStatus();

                dgvOrders.DataSource = typeof(List<Order>);
                dgvOrders.DataSource = _ordersList;
                dgvItems.DataSource = typeof(List<OrderItem>);
                dgvItems.DataSource = _selectedOrder.Items;

                ResetItemForm();
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

        #region Housekeeping and Helpers

        public void ResetItemForm()
        {
            foreach (Control ctr in groupBox1.Controls)
            {
                if (ctr is TextBox)
                {
                    ctr.Text = "";
                    ctr.Enabled = false;
                }

                NumericUpDown nud = ctr as NumericUpDown;

                if (nud != null)
                {
                    nud.Value = nud.Minimum;
                    nud.Enabled = false;
                }
            }

            groupBox1.Enabled = false;
            btnProcess.Enabled = false;
        }

        private void CheckOrderStatus()
        {
            //if all items are approved / denied
            //put under review
            OrderService service = new OrderService();

            if (!service.CloseOrderStatus(_selectedOrder))
            {
                service.UpdateOrderStatus(_selectedOrder);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("All items have been processed. Would" +
                    " you like to close this POR?", "Close POR", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _selectedOrder.StatusID = 5;
                    service.UpdateOrderStatus(_selectedOrder);
                    service.SendOrderProcessedEmail(_selectedOrder);
                    MessageBox.Show("POR has been closed");
                }
            }

        }

        private void rdoDates_CheckedChanged(object sender, EventArgs e)
        {
            dtpStart.Enabled = true;
            dtpEnd.Enabled = true;
        }

        private void ReloadOrdersDGV()
        {
            OrderService service = new OrderService();
            PONumber.DataPropertyName = "PONumberFormatted";
            Date.DataPropertyName = "POCreationDate";
            Employee.DataPropertyName = "EmployeeName";
            OrderStatus.DataPropertyName = "Status";
            dgvOrders.DataSource = service.GetAllPendingOrdersForEmp(emp.EmployeeId, 1, 0, "", null, null, 1);

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

        public void UpdateTotals()
        {
            OrderService service = new OrderService();
            service.UpdateOrderTotals(_selectedOrder);
            lblSubtotal.Text = _selectedOrder.POSubtotal.ToString("C");
            lblTax.Text = _selectedOrder.POTax.ToString("C");
            lblTotal.Text = _selectedOrder.POTotal.ToString("C");
        }

        #endregion

        private void btnRemove_Click(object sender, EventArgs e)
        {
            dgvOrders.DataSource = null;
            dgvItems.DataSource = null;
            txtSearchEmp.Text = "";
            txtSearchPO.Text = "";
            dtpStart.Value = DateTime.Now;
            dtpStart.Enabled = false;
            dtpEnd.Value = DateTime.Now;
            dtpEnd.Enabled = false;
            rdoDates.Checked = false;

            ResetItemForm();
        }

        
    }
}
