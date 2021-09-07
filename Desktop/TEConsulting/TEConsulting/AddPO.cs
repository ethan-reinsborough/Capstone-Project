using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEConsulting.Model;
using TEConsulting.Service;

namespace TEConsulting
{
    public partial class AddPO : Form
    {
        private Order _order;
        public Employee emp = MainMenu.Employee;

        public AddPO()
        {
            InitializeComponent();
        }

        private void AddPO_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            dtpCurrent.Value = today;
            lblEmpName.Text = emp.FullName;
            lblPONumber.Text = "";
            lblSubtotal.Text = (0.0m).ToString("C");
            lblTax.Text = (0.0m).ToString("C");
            lblTotal.Text = (0.0m).ToString("C");
            LoadSupDep();
        }

        #region CRUD

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                string errMsg = ValidateAdd();

                if (errMsg != string.Empty)
                {
                    MessageBox.Show(errMsg, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    OrderService service = new OrderService();
                    
                    if (dgvItems.Rows.Count == 0)
                    {
                        _order = new Order()
                        {
                            EmployeeID = emp.EmployeeId
                        };

                        _order.Items = new List<OrderItem>();

                        OrderItem newItem = PopulateItemObject();

                        _order.Items.Add(newItem);

                       
                        if (service.AddOrder(_order))
                        {
                            lblPONumber.Text = "PO Number: " + _order.PONumberFormatted + " - Status: Pending";
                        }
   
                    }
                    else
                    {
                        OrderItem newItem = PopulateItemObject();

                        if (service.CheckForDupes(_order.Items, newItem))
                        {
                            service.UpdateDupedItem(_order.Items, newItem);
                        }
                        else
                        {
                            //Add new item
                            newItem.PONumber = _order.PONumber;
                            _order.Items.Add(newItem);
                            service.AddItem(newItem);
                        }
                    }

                    ResetItemForm();

                    //update list
                    dgvItems.DefaultCellStyle.ForeColor = Color.Black;
                    dgvItems.Columns["Price"].DefaultCellStyle.Format = "c";
                    dgvItems.AutoGenerateColumns = false;
                    dgvItems.DataSource = null;
                    Qty.DataPropertyName = "OrderItemQty";
                    ItemName.DataPropertyName = "OrderItemName";
                    Desc.DataPropertyName = "OrderItemDescription";
                    Price.DataPropertyName = "OrderItemPrice";
                    Just.DataPropertyName = "OrderItemJustification";
                    Loc.DataPropertyName = "OrderItemLocation";
                    Status.DataPropertyName = "Status";
                    dgvItems.DataSource = _order.Items;

                    UpdateTotals();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public string ValidateSubmit()
        {
            string errMsg = string.Empty;

            if(dgvItems.Rows.Count == 0)
            {
                errMsg = "Please add at least one item to the purchase order";
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
                OrderItemLocation = txtLoc.Text.Trim()
            };
        }

        public void UpdateTotals()
        {
            OrderService service = new OrderService();
            service.UpdateOrderTotals(_order);
            lblSubtotal.Text = _order.POSubtotal.ToString("C");
            lblTax.Text = _order.POTax.ToString("C");
            lblTotal.Text = _order.POTotal.ToString("C");
        }

        public void LoadSupDep()
        {
            LookupsService lookup = new LookupsService();
            lblDepartment.Text = lookup.GetSupDeps(emp.EmployeeId).Department;
            lblSupervisor.Text = lookup.GetSupDeps(emp.EmployeeId).Supervisor;
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
        }

        #endregion
    }
}
