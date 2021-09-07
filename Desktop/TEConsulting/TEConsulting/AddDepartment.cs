using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TEConsulting.Model;
using TEConsulting.Service;

namespace TEConsulting
{
    public partial class AddDepartment : Form
    {
        public static Employee emp = MainMenu.Employee;

        public byte[] selectedRecordVersion;

        public int selectedDepId;

        public bool initial;

        public AddDepartment()
        {
            InitializeComponent();
        }

        private void AddDepartment_Load(object sender, System.EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            pictureBox1.BorderStyle = BorderStyle.None;
            initial = true;

            if (emp.DepartmentId != 1)
            {
                if(emp.SupervisorId != 10000000)
                {
                    groupBox1.Visible = false;
                }
                LoadLoggedDepartment();
                chkModify.Checked = true;
                chkDelete.Checked = false;
                chkDelete.Visible = false;
                dtpInvDate.Enabled = false;
                txtDeptName.Enabled = false;
                chkModify.Enabled = false;
                chkModify.Visible = false;
                cmbDepartments.Visible = false;
                lblModDept.Visible = false;
                btnCreate.Text = "Update";
                //if employee is not a supervisor or HR, can't access the form
            }
            else
            {
                LoadDepartments();
            }
        }

        private void BtnCreate_Click(object sender, System.EventArgs e)
        {
            try
            {
                string errMsg = ValidateForm();

                if (errMsg != string.Empty)
                {
                    MessageBox.Show(errMsg, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    errorProvider1.Clear();
                    if (btnCreate.Text == "Create")
                    {
                        DepartmentService service = new DepartmentService();
                        Department department = PopulateDepartmentObject();
                     
                        if (service.AddDepartment(department))
                        {
                            MessageBox.Show($"Department: {department.Name} has been successfully created with an ID of {department.DepartmentId}");
                            ClearControls();
                            LoadDepartments();
                        }
                        else
                        {
                            MessageBox.Show("Creation of Department failed.");
                        }
                    }
                    if(btnCreate.Text == "Delete")
                    {
                        DepartmentService service = new DepartmentService();
                        LookupsService lService = new LookupsService();
                        Department department = PopulateDepartmentObject();
                        if (!lService.CheckEmptyDept(Convert.ToInt32(cmbDepartments.SelectedValue)))
                        {
                            MessageBox.Show("A department must have no employees to be valid for deletion.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (service.DeleteDepartment(Convert.ToInt32(cmbDepartments.SelectedValue)))
                        {
                            initial = false;
                            MessageBox.Show($"Department has been successfully deleted");
                            ClearControls();
                            chkDelete.Checked = false;
                            LoadDepartments();
                            
                        }
                        else
                        {
                            MessageBox.Show("Deletion of Department failed.");
                        }
                    }
                    if(btnCreate.Text == "Update")
                    {
                        DepartmentService service = new DepartmentService();

                        Department department = PopulateDepartmentObject();
                        if(emp.DepartmentId == 1)
                        {
                            department.DepartmentId = Convert.ToInt32(cmbDepartments.SelectedValue);
                        }
                        else
                        {
                            department.DepartmentId = emp.DepartmentId;
                        }

                        department.RecordVersion = selectedRecordVersion;

                        try
                        {
                            if (service.ModifyDepartment(department))
                            {
                                initial = false;
                                MessageBox.Show($"Department: {department.Name} has been successfully updated.");
                                selectedRecordVersion = department.RecordVersion;

                                LoadDepartments();
                                cmbDepartments.SelectedValue = department.DepartmentId;
                                    
                                //chkModify.Checked = false;
                            }
                            else
                            {
                                MessageBox.Show("Update failed.");
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
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        public string ValidateForm()
        {
            string errMsg = string.Empty;

            if (txtDeptName.Text == string.Empty)
            {
               errorProvider1.SetError(txtDeptName, "Department Name is required.");
               errMsg = errMsg + Environment.NewLine + "Department Name is required.";
            }
            if (txtDesc.Text == string.Empty)
            {
                errorProvider1.SetError(txtDesc, "Department Description is required.");
                errMsg = errMsg + Environment.NewLine + "Department Description is required.";
            }
            if(dtpInvDate.Value < dtpHidden.Value && emp.DepartmentId == 1)
            {
                errorProvider1.SetError(dtpInvDate, "Modified invocation date cannot be before the original invocation date.");
                errMsg = errMsg + Environment.NewLine + "Modified invocation date cannot be before the original invocation date.";
            }
            return errMsg;
        }

        private Department PopulateDepartmentObject()
        {
            return new Department
            {
                Name = txtDeptName.Text.Trim(),
                Description = txtDesc.Text.Trim(),
                InvocationDate = Convert.ToDateTime(dtpInvDate.Value)
            };
        }

        private void chkModify_CheckedChanged(object sender, EventArgs e)
        {
            chkDelete.Checked = false;
            errorProvider1.Clear();
            if (emp.DepartmentId == 1)
            {
                cmbDepartments.Visible = (chkModify.CheckState == CheckState.Checked);
                lblModDept.Visible = (chkModify.CheckState == CheckState.Checked);
                if (emp.DepartmentId == 1)
                {
                    cmbDepartments.SelectedIndex = 0;
                }

                if (chkModify.Checked == false)
                {
                    ClearControls();
                }
            }        
        }

        private void LoadDepartments()
        {
            LookupsService service = new LookupsService();
            List<DepartmentLookupDTO> departments = service.GetDepartments();

            cmbDepartments.DataSource = departments;
            cmbDepartments.DisplayMember = "Name";
            cmbDepartments.ValueMember = "DepartmentId";

            /*
            if(chkDelete.Checked == true || chkModify.Checked == true)
            {
                LoadCurrentDepartment();
            }
            */
            //cmbDepartments.SelectedIndex = -1;
            
            
        }

        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkModify.Checked == true && emp.DepartmentId == 1)
            {
                LoadCurrentDepartment();
                btnCreate.Text = "Update";
                dtpHidden.Value = dtpInvDate.Value;            
            }
            if (chkDelete.Checked == true && emp.DepartmentId == 1)
            {
                LoadCurrentDepartment();
                btnCreate.Text = "Delete";
                dtpHidden.Value = dtpInvDate.Value;
            }
        }

        private void ClearControls()
        {
            //cmbDepartments.SelectedIndex = -1;
            txtDeptName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            dtpInvDate.Text = DateTime.Now.ToShortTimeString();
            btnCreate.Text = "Create";
            lblVersion.Text = string.Empty;
        }

        private void LoadCurrentDepartment()
        {
            LookupsService service = new LookupsService();
            Department d = service.GetDepartment(Convert.ToInt32(cmbDepartments.SelectedValue));

            txtDeptName.Text = d.Name;
            txtDesc.Text = d.Description;
            dtpInvDate.Value = d.InvocationDate;

            selectedRecordVersion = d.RecordVersion;
            selectedDepId = d.DepartmentId;
        }

        private void LoadLoggedDepartment()
        {
            LookupsService service = new LookupsService();
            Department d = service.GetDepartment(Convert.ToInt32(emp.DepartmentId));

            cmbDepartments.SelectedText = d.Name;

            txtDeptName.Text = d.Name;
            txtDesc.Text = d.Description;
            dtpInvDate.Value = d.InvocationDate;
            selectedRecordVersion = d.RecordVersion;
            selectedDepId = d.DepartmentId;
        }

        private void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            chkModify.Checked = false;
            errorProvider1.Clear();

            if (emp.DepartmentId == 1)
            {
                txtDeptName.Enabled = false;
                txtDesc.Enabled = false;
                dtpInvDate.Enabled = false;
                cmbDepartments.Visible = (chkDelete.CheckState == CheckState.Checked);
                lblModDept.Visible = (chkDelete.CheckState == CheckState.Checked);
              

                if (chkDelete.Checked == false)
                {
                    ClearControls();
                    txtDeptName.Enabled = true;
                    txtDesc.Enabled = true;
                    dtpInvDate.Enabled = true;
                }
            }
        }
    }
}
