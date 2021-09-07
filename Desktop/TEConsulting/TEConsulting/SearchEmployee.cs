using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TEConsulting.Model;
using TEConsulting.Service;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace TEConsulting
{
    public partial class SearchEmployee : Form
    {
        public byte[] selectedRecordVersion;
        public Employee loggedEmp = MainMenu.Employee;
        public bool jobChange = false;
        public string updateWord;
        public SearchEmployee()
        {
            InitializeComponent();
        }

        private void SearchEmployee_Load(object sender, System.EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            pictureBox1.BorderStyle = BorderStyle.None;
        }

        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (rdoID.Checked == true)
                {

                    LookupsService service = new LookupsService();
                    int queriedEmployee = Convert.ToInt32(txtSearch.Text.Trim());
                    SearchEmployeeLookupDTO searchedEmp = service.GetEmployeeSearchById(queriedEmployee);
                    List<SearchEmployeeLookupDTO> formatEmp = new List<SearchEmployeeLookupDTO>();
                    formatEmp.Add(searchedEmp);
                    if (searchedEmp == null)
                    {
                        MessageBox.Show("No search results found.");
                    }
                    else
                    {
                        this.dgvSearch.DefaultCellStyle.ForeColor = Color.Black;
                        dgvSearch.AutoGenerateColumns = false;
                        dgvSearch.DataSource = null;
                        EmployeeID.DataPropertyName = "EmployeeId";
                        FirstName.DataPropertyName = "FirstName";
                        MI.DataPropertyName = "MiddleInitial";
                        LastName.DataPropertyName = "LastName";
                        MailAddress.DataPropertyName = "MailAddress";
                        WorkNum.DataPropertyName = "WorkPhone";
                        CellNum.DataPropertyName = "CellPhone";
                        Email.DataPropertyName = "Email";
                        dgvSearch.DataSource = formatEmp;
                    }
                }
                else if (rdoLName.Checked == true)
                {
                    LookupsService service = new LookupsService();
                    string queriedEmployee = txtSearch.Text.Trim();
                    List<SearchEmployeeLookupDTO> searchedEmp = service.GetEmployeeSearchByLName(queriedEmployee);

                    //Duplicate code, cleanup when time allows
                    this.dgvSearch.DefaultCellStyle.ForeColor = Color.Black;
                    dgvSearch.AutoGenerateColumns = false;
                    dgvSearch.DataSource = null;
                    EmployeeID.DataPropertyName = "EmployeeId";
                    FirstName.DataPropertyName = "FirstName";
                    MI.DataPropertyName = "MiddleInitial";
                    LastName.DataPropertyName = "LastName";
                    MailAddress.DataPropertyName = "MailAddress";
                    WorkNum.DataPropertyName = "WorkPhone";
                    CellNum.DataPropertyName = "CellPhone";
                    Email.DataPropertyName = "Email";
                    dgvSearch.DataSource = searchedEmp;
                }
                else
                {
                    MessageBox.Show("Please select a search option.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void dgvSearch_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {          

            if (dgvSearch.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dgvSearch.SelectedCells[0].RowIndex;
                DataGridViewRow selRow = dgvSearch.Rows[selectedRowIndex];
                int selectedEmpId = Convert.ToInt32(selRow.Cells["EmployeeID"].Value);

                //If Employee is not a supervisor or CEO, they are only allowed to edit their own information
                if (loggedEmp.DepartmentId == 1 || selectedEmpId == loggedEmp.EmployeeId)
                {
                    lblSelectedId.Text = selectedEmpId.ToString();
                    HideSearchComponents();
                }
                else
                {
                    MessageBox.Show("You do not have permission to edit this employee.");
                }
            }
            else
            {
                MessageBox.Show("You must select an employee to modify.");
            }
        }

        private void HideSearchComponents()
        {
            dgvSearch.Visible = false;
            txtSearch.Visible = false;
            rdoID.Visible = false;
            rdoLName.Visible = false;
            BtnModify.Visible = false;
            BtnSearch.Visible = false;
            grpBox1.Text = "Modify Employee";
            rdoPersonal.Visible = true;
            rdoJobInformation.Visible = true;
            rdoRetirement.Visible = true;
            BtnSelect.Visible = true;
            lblSelectedId.Visible = true;
            BtnBack.Visible = true;
        }

        private void ShowSearchComponents()
        {
            dgvSearch.Visible = true;
            txtSearch.Visible = true;
            rdoID.Visible = true;
            rdoLName.Visible = true;
            BtnModify.Visible = true;
            BtnSearch.Visible = true;
            grpBox1.Text = "Search Employee";
            rdoPersonal.Visible = false;
            rdoJobInformation.Visible = false;
            rdoRetirement.Visible = false;
            BtnSelect.Visible = false;
            lblSelectedId.Visible = false;
            BtnBack.Visible = false;
            grpPInfo.Visible = false;
            grpJobInfo.Visible = false;
            grpJobStatus.Visible = false;
        }

        private void PopulatePersonalField()
        {
            EmployeeService service = new EmployeeService();
            Employee emp = service.RetrieveEmployeeById(Convert.ToInt32(lblSelectedId.Text));

            var countryList = new System.ComponentModel.BindingList<KeyValuePair<string, string>>();
            countryList.Add(new KeyValuePair<string, string>("CA", "CA"));
            countryList.Add(new KeyValuePair<string, string>("USA", "USA"));
            cmbCountry.DataSource = countryList;
            cmbCountry.ValueMember = "Key";
            cmbCountry.DisplayMember = "Value";

            selectedRecordVersion = emp.RecordVersion;
            txtF.Text = emp.FirstName;
            txtM.Text = emp.MiddleInitial;
            txtL.Text = emp.LastName;
            txtSA.Text = emp.StreetAddress;
            txtMun.Text = emp.Municipality;
            cmbCountry.SelectedValue = emp.Country;

            if (emp.Country == "CA")
            {
                //Load canadian provinces and select 
                lblPostal.Text = "Postal Code";
                lblProvince.Text = "Province";
                cmbProvince.DataSource = Enum.GetValues(typeof(EmployeeEnums.Province));
                cmbProvince.Text = emp.Province;
            }
            else
            {
                //load usa states and select
                lblPostal.Text = "Zip Code";
                lblProvince.Text = "State";

                cmbProvince.DataSource = Enum.GetValues(typeof(EmployeeEnums.States));
                cmbProvince.Text = emp.Province;
            }
            txtPostal.Text = emp.PostalCode;
            txtWNum.Text = emp.WorkPhone;
            txtCNum.Text = emp.CellPhone;
            txtEmail.Text = emp.Email;
            dtpDOB.Value = emp.DOB;
            txtPass.Text = emp.Password;

        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if(rdoPersonal.Checked == true)
            {
                grpJobInfo.Visible = false;
                grpPInfo.Visible = true;
                grpJobStatus.Visible = false;
                PopulatePersonalField();
            }
            if (rdoJobInformation.Checked == true)
            {
                if(loggedEmp.EmployeeId == Convert.ToInt32(lblSelectedId.Text))
                {
                    MessageBox.Show("You cannot edit your own job information.");
                }
                else
                {
                    grpJobInfo.Visible = true;
                    grpPInfo.Visible = true;
                    grpJobStatus.Visible = false;
                    PopulatePersonalField();
                    txtJobSIN.Enabled = false;
                    EmployeeService service = new EmployeeService();
                    Employee empJob = service.RetrieveEmployeeJobInfo(Convert.ToInt32(lblSelectedId.Text));

                    selectedRecordVersion = empJob.RecordVersion;
                    dtpHidden.Value = empJob.SeniorityDate;
                    txtJobSIN.Text = empJob.SIN;
                    txtJobSIN.Enabled = true;
                    dtpStartDate.Value = empJob.JobStartDate;

                    LoadDepartments();
                    LoadSupervisors();
                    LoadJobs();

                    cmbDepartment.SelectedValue = empJob.DepartmentId;
                    cmbSupervisor.SelectedValue = empJob.SupervisorId;
                    cmbJobAssignment.SelectedValue = empJob.JobId;
                    if (empJob.SupervisorId == 10000000)
                    {
                        cmbSupervisor.SelectedIndex = -1;
                        chkSupervisor.Checked = true;
                    }
                }
                          
            }
            if (rdoRetirement.Checked == true)
            {
                if (loggedEmp.EmployeeId == Convert.ToInt32(lblSelectedId.Text))
                {
                    MessageBox.Show("You cannot edit your own job information.");
                }
                else
                {
                    dtpRetirement.Enabled = true;
                    EmployeeService service = new EmployeeService();
                    Employee empJob = service.RetrieveEmployeeJobInfo(Convert.ToInt32(lblSelectedId.Text));
                    selectedRecordVersion = empJob.RecordVersion;
                    txtStatusSIN.Text = empJob.SIN;
                    grpPInfo.Visible = false;
                    grpJobInfo.Visible = false;
                    grpJobStatus.Visible = true;

                    var statusList = new System.ComponentModel.BindingList<KeyValuePair<int, string>>();

                    statusList.Add(new KeyValuePair<int, string>(1, "Active"));
                    statusList.Add(new KeyValuePair<int, string>(2, "Retired"));
                    statusList.Add(new KeyValuePair<int, string>(3, "Terminated"));

                    cmbStatus.DataSource = statusList;
                    cmbStatus.ValueMember = "Key";
                    cmbStatus.DisplayMember = "Value";

                    Employee empStatus = service.RetrieveStatus(Convert.ToInt32(lblSelectedId.Text));

                    if (empStatus.Active == 2)
                    {
                        cmbStatus.Enabled = false;
                        lblR.Visible = true;
                        dtpRetirement.Visible = true;
                        dtpRetirement.Value = empStatus.RetirementDate;
                        dtpRetirement.Enabled = false;
                    }
                    if (empStatus.Active == 3)
                    {
                        lblR.Visible = true;
                        lblR.Text = "Termination Date";
                        dtpRetirement.Visible = true;
                        dtpRetirement.Value = empStatus.TerminationDate;
                    }

                    cmbStatus.SelectedValue = empStatus.Active;
                }     
            }
            if(rdoPersonal.Checked == false && rdoJobInformation.Checked == false && rdoRetirement.Checked == false)
            {
                MessageBox.Show("Please select an option.");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            ShowSearchComponents();
            dtpRetirement.Visible = false;
            lblR.Visible = false;
            cmbStatus.Enabled = true;
        }

        private void BtnUpdatePInfo_Click(object sender, EventArgs e)
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
                    EmployeeService empService = new EmployeeService();
                    
                    Employee employee = PopulatePersonalEmployeeObject();
                    employee.RecordVersion = selectedRecordVersion;
                    employee.EmployeeId = Convert.ToInt32(lblSelectedId.Text);

                    if (empService.ModifyPersonalEmployeeInfo(employee))
                    {
                        MessageBox.Show($"Employee has been successfully updated.");
                        selectedRecordVersion = employee.RecordVersion;
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");
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

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeeService service = new EmployeeService();
            Employee emp = service.RetrieveEmployeeById(Convert.ToInt32(lblSelectedId.Text));

            if (cmbCountry.SelectedIndex == 0)
            {
                lblPostal.Text = "Postal Code";
                lblProvince.Text = "Province";
                cmbProvince.DataSource = Enum.GetValues(typeof(EmployeeEnums.Province));
                cmbProvince.Text = emp.Province;
            }
            else
            {
                lblPostal.Text = "Zip Code";
                lblProvince.Text = "State";
                cmbProvince.DataSource = Enum.GetValues(typeof(EmployeeEnums.States));
                cmbProvince.Text = emp.Province;
            }
        }

        private Employee PopulatePersonalEmployeeObject()
        {
            return new Employee
            {
                Password = txtPass.Text.Trim(),
                FirstName = txtF.Text.Trim(),
                MiddleInitial = txtM.Text.Trim(),
                LastName = txtL.Text.Trim(),
                DOB = Convert.ToDateTime(dtpDOB.Value),
                StreetAddress = txtSA.Text.Trim(),
                Municipality = txtMun.Text.Trim(),
                Province = cmbProvince.SelectedValue.ToString(),
                Country = cmbCountry.SelectedValue.ToString(),
                PostalCode = txtPostal.Text.Trim(),
                WorkPhone = txtWNum.Text.Trim(),
                CellPhone = txtCNum.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
        }

        private Employee PopulateJobInfo()
        {
            return new Employee
            {
                EmployeeId = Convert.ToInt32(lblSelectedId.Text),
                JobStartDate = Convert.ToDateTime(dtpStartDate.Value),
                DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue),
                SupervisorId = Convert.ToInt32(cmbSupervisor.SelectedValue),
                JobId = Convert.ToInt32(cmbJobAssignment.SelectedValue)
            };
        }

        public string ValidateForm()
        {
            EmployeeService service = new EmployeeService();

            string errMsg = string.Empty;
            if (txtF.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "First name is required.";
            }
            if (txtM.Text != string.Empty && txtM.Text.Length > 1)
            {
                errMsg = errMsg + Environment.NewLine + "Middle initial cannot be greater than 1 character.";
            }
            if (txtL.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Last name is required.";
            }
            if (txtSA.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Street Address is required.";
            }
            if (txtMun.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Municipality is required.";
            }
            //Don't really need this
            if (cmbProvince.SelectedIndex < 0 && cmbCountry.SelectedValue.ToString() == "CA")
            {
                errMsg = errMsg + Environment.NewLine + "You must select a province.";
            }
            if (txtPostal.Text == string.Empty || txtPostal.Text == "A#A#A#" || txtPostal.Text == "#####")
            {
                errMsg = errMsg + Environment.NewLine + "Postal Code is required.";
            }
            if (txtPostal.Text != string.Empty && cmbCountry.SelectedValue.ToString() == "CA" && txtPostal.Text != "A#A#A#")
            {
                var regex = @"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]";
                Match m = Regex.Match(txtPostal.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Postal Code format is invalid.";
                }
            }
            if (txtPostal.Text != string.Empty && cmbCountry.SelectedValue.ToString() == "USA" && txtPostal.Text != "#####")
            {
                var regex = @"[0-9][0-9][0-9][0-9][0-9]";
                Match m = Regex.Match(txtPostal.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Zip Code format is invalid.";
                }
            }
            if (txtWNum.Text == "###-###-####" || txtWNum.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Work Number is required.";
            }
            if (txtWNum.Text != "###-###-####" && txtWNum.Text != string.Empty)
            {
                var regex = @"[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]";
                Match m = Regex.Match(txtWNum.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Work Number format is invalid.";
                }
            }
            if (txtCNum.Text == "###-###-####" || txtCNum.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Cell Number is required.";
            }
            if (txtCNum.Text != string.Empty && txtCNum.Text != "###-###-####")
            {
                var regex = @"[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]";
                Match m = Regex.Match(txtCNum.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Cell Number format is invalid.";
                }
            }
            if (txtEmail.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Email is required.";
            }
            if (txtEmail.Text != string.Empty)
            {
                var regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                Match m = Regex.Match(txtEmail.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Email format is invalid.";
                }
            }
            if (dtpDOB.Value > DateTime.Today.AddYears(-18))
            {
                errMsg = errMsg + Environment.NewLine + "Employee must be at least 18 years of age.";
            }         
            if (txtPass.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Password is required.";
            }           

            return errMsg;
        }

        private void LoadDepartments()
        {
            LookupsService service = new LookupsService();
            List<DepartmentLookupDTO> departments = service.GetDepartments();

            cmbDepartment.DataSource = departments;
            cmbDepartment.DisplayMember = "Name";
            cmbDepartment.ValueMember = "DepartmentId";
            cmbDepartment.SelectedIndex = -1;
        }

        private void LoadJobs()
        {
            LookupsService service = new LookupsService();
            List<JobLookupDTO> jobs = service.GetJobs();

            cmbJobAssignment.DataSource = jobs;
            cmbJobAssignment.DisplayMember = "JobName";
            cmbJobAssignment.ValueMember = "JobId";
            cmbJobAssignment.SelectedIndex = -1;
        }

        private void LoadSupervisors()
        {
            LookupsService service = new LookupsService();
            List<SupervisorLookupDTO> supervisors = service.GetSupervisors();

            cmbSupervisor.DataSource = supervisors;
            cmbSupervisor.DisplayMember = "SupervisorName";
            cmbSupervisor.ValueMember = "SupervisorId";
            cmbSupervisor.SelectedIndex = -1;
        }

        private void LoadSupervisorsByDepartment(int departmentId)
        {
            LookupsService service = new LookupsService();
            List<SupervisorLookupDTO> supervisors = service.GetSupervisorsByDepartment(departmentId);

            cmbSupervisor.DataSource = supervisors;
            cmbSupervisor.DisplayMember = "SupervisorName";
            cmbSupervisor.ValueMember = "SupervisorId";
            cmbSupervisor.SelectedIndex = -1;
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Index 0 of cmbDepartment has a value of 1 but causes it to crash due to SelectedIndexChanged on form load.
            if (cmbDepartment.SelectedIndex == 0)
            {
                LoadSupervisorsByDepartment(Convert.ToInt32(1));
            }
            if (cmbDepartment.SelectedIndex > 0)
            {
                LoadSupervisorsByDepartment(Convert.ToInt32(cmbDepartment.SelectedValue));
            }
        }

        private void cmbSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkSupervisor_CheckedChanged(object sender, EventArgs e)
        {
            cmbSupervisor.SelectedText = "";
            cmbSupervisor.Enabled = !(chkSupervisor.CheckState == CheckState.Checked);
            if (cmbDepartment.SelectedIndex == 0)
            {
                LoadSupervisorsByDepartment(Convert.ToInt32(1));
            }
            if (cmbDepartment.SelectedIndex > 0)
            {
                LoadSupervisorsByDepartment(Convert.ToInt32(cmbDepartment.SelectedValue));
            }
        }

         public string ValidateJobForm(int departmentID)
         {
            EmployeeService service = new EmployeeService();
            int numEmp = service.GetDepartmentEmployeeCount(Convert.ToInt32(cmbDepartment.SelectedValue));
            int numSup = service.GetDepartmentSupervisorCount(Convert.ToInt32(cmbDepartment.SelectedValue));

            string errMsg = string.Empty;

            if (cmbDepartment.SelectedIndex < 0)
            {
                errMsg = errMsg + Environment.NewLine + "You must assign a department.";
            }
            if (dtpStartDate.Value < dtpHidden.Value)
            {
                errMsg = errMsg + Environment.NewLine + "Job start date cannot be sooner than seniority date.";
            }
            if (dtpStartDate.Value < DateTime.Now && jobChange == true)
            {
                errMsg = errMsg + Environment.NewLine + "New Job Start Date cannot be sooner than today.";
            }
            if (cmbSupervisor.SelectedIndex < 0 && chkSupervisor.Checked == false)
            {
                errMsg = errMsg + Environment.NewLine + "You must assign a supervisor.";
            }
            if (txtJobSIN.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "SIN must be 9 digits.";
            }
            if (txtJobSIN.Text != string.Empty)
            {
                var regex = @"[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]";
                Match m = Regex.Match(txtJobSIN.Text.ToString().Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "SIN must be 9 digits.";                 
                }
            }
            //Perform this check only if the employee is switching departments
            if ((numEmp >= (numSup * 10) + numSup * 1) && chkSupervisor.Checked == false && Convert.ToInt32(cmbDepartment.SelectedValue) != departmentID)
            {
                errMsg = errMsg + Environment.NewLine + $"There are {numEmp} employees in {cmbDepartment.Text} and only {numSup} supervisor(s). There must be 1 supervisor per 10 employees.";
            }

            return errMsg;
        }

        
        private void BtnJobInfoUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeService service = new EmployeeService();
                Employee empJob = service.RetrieveEmployeeJobInfo(Convert.ToInt32(lblSelectedId.Text));
                string errMsg = ValidateJobForm(empJob.DepartmentId);

                if (errMsg != string.Empty)
                {
                    MessageBox.Show(errMsg, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    EmployeeService empService = new EmployeeService();

                    Employee employee = PopulateJobInfo();
                    employee.RecordVersion = selectedRecordVersion;
                    if(chkSupervisor.Checked == true)
                    {
                        employee.SupervisorId = 10000000;
                    }
                    if (empService.ModifyJobInfo(employee))
                    {
                        MessageBox.Show($"Job information has been successfully updated.");
                        selectedRecordVersion = employee.RecordVersion;
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");
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

        private void BtnUpdateJStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateRetirement() && Convert.ToInt32(cmbStatus.SelectedValue) != 3 && Convert.ToInt32(cmbStatus.SelectedValue) != 1)
                {
                    MessageBox.Show("You must be at least 55 years of age to retire.");
                }
                else
                {
                    EmployeeService empService = new EmployeeService();

                    Employee employee = new Employee();
                    employee.RecordVersion = selectedRecordVersion;
                    employee.RetirementDate = Convert.ToDateTime("1-1-1800");
                    employee.TerminationDate = Convert.ToDateTime("1-1-1800");
                    if (updateWord == "retired")
                    {
                        employee.RetirementDate = dtpRetirement.Value;
                        dtpRetirement.Enabled = false;
                    }
                    if (updateWord == "terminated")
                    {
                        employee.TerminationDate = dtpRetirement.Value;

                    }
                    if(updateWord != "retired" || updateWord != "terminated")
                    {

                    }
                    
                    employee.EmployeeId = Convert.ToInt32(lblSelectedId.Text);
                    employee.Active = Convert.ToInt32(cmbStatus.SelectedValue);


                    if (empService.ModifyStatus(employee))
                    {
                        selectedRecordVersion = employee.RecordVersion;
                        MessageBox.Show($"Employee's Job Status has been set to {updateWord}");
                        if(updateWord == "active")
                        {
                            lblR.Visible = false;
                            dtpRetirement.Visible = false;
                        }
                        if(updateWord == "retired")
                        {
                            cmbStatus.Enabled = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");
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

        private bool ValidateRetirement()
        {
            EmployeeService service = new EmployeeService();

            Employee employee = service.RetrieveEmployeeById(Convert.ToInt32(lblSelectedId.Text));

            if(employee.DOB > DateTime.Today.AddYears(-55))
            {
                return true;
            }

            return false;
        }


        private void cmbJobAssignment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EmployeeService service = new EmployeeService();
            Employee empJob = service.RetrieveEmployeeJobInfo(Convert.ToInt32(lblSelectedId.Text));
            //If selection goes back to original job, assume job start date should be retained from before
            if (empJob.JobId == Convert.ToInt32(cmbJobAssignment.SelectedValue)){
                dtpStartDate.Value = empJob.JobStartDate;
                jobChange = false;
            }
            else
            {
                dtpStartDate.Text = DateTime.Now.ToShortTimeString();
                jobChange = true;
            }           
        }

        private void cmbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(Convert.ToInt32(cmbStatus.SelectedValue) == 1)
            {
                updateWord = "active";
            }
            if (Convert.ToInt32(cmbStatus.SelectedValue) == 2)
            {
                lblR.Visible = true;
                lblR.Text = "Retirement Date";
                dtpRetirement.Visible = true;
                dtpRetirement.Text = DateTime.Now.ToShortTimeString();
               //employee.RetirementDate = dtpRetirement.Value;
                updateWord = "retired";
            }
            if (Convert.ToInt32(cmbStatus.SelectedValue) == 3)
            {
                lblR.Visible = true;
                lblR.Text = "Termination Date";
                dtpRetirement.Visible = true;
                dtpRetirement.Text = DateTime.Now.ToShortTimeString();
                //employee.TerminationDate = dtpRetirement.Value;
                updateWord = "terminated";

            }
        }
    }
}
