using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TEConsulting.Model;
using TEConsulting.Service;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace TEConsulting
{
    public partial class AddEmployee : Form
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            //Load current supervisor info for display possibly
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            pictureBox1.BorderStyle = BorderStyle.None;
            LoadDepartments();
            LoadJobs();
            LoadSupervisors();
            var countryList = new System.ComponentModel.BindingList<KeyValuePair<string, string>>();
            countryList.Add(new KeyValuePair<string, string>("CA", "CA"));
            countryList.Add(new KeyValuePair<string, string>("USA", "USA"));
            cmbCountry.DataSource = countryList;
            cmbCountry.ValueMember = "Key";
            cmbCountry.DisplayMember = "Value";
            cmbProvince.DataSource = Enum.GetValues(typeof(EmployeeEnums.Province));
        }
        private void BtnCreate_Click(object sender, EventArgs e)
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
                    EmployeeService service = new EmployeeService();

                    Employee employee = PopulateEmployeeObject();

                    //If the employee being created is to be a supervisor, their supervisor will be the CEO (Khraig)
                    if(cmbSupervisor.Enabled == false)
                    {
                        employee.SupervisorId = 10000000;
                    }

                    employee.HashedPass = ComputeHash(employee.Password, new SHA256CryptoServiceProvider());
                    MessageBox.Show(employee.HashedPass);
                    //Causing problems in the DB, disabled for now.
                    //employee.Password = Encoding.UTF8.GetString(hashed, 0, hashed.Length);

                    if (service.AddEmployee(employee))
                    {
                        MessageBox.Show($"Employee {employee.FirstName} {employee.LastName} has been successfully created with an ID of {employee.EmployeeId}");
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Creation of Employee failed.");
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
            errorProvider1.Clear();
            EmployeeService service = new EmployeeService();
            int numEmp = service.GetDepartmentEmployeeCount(Convert.ToInt32(cmbDepartment.SelectedValue));
            int numSup = service.GetDepartmentSupervisorCount(Convert.ToInt32(cmbDepartment.SelectedValue));
            string errMsg = string.Empty;
            if(txtFName.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "First name is required.";
                errorProvider1.SetError(txtFName, "First name is required.");
            }
            if (txtMI.Text != string.Empty && txtMI.Text.Length > 1)
            {
                errMsg = errMsg + Environment.NewLine + "Middle initial cannot be greater than 1 character.";
                errorProvider1.SetError(txtMI, "Middle initial cannot be greater than 1 character.");
            }
            if (txtLName.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Last name is required.";
                errorProvider1.SetError(txtLName, "Last name is required.");
            }
            if (txtStreet.Text == string.Empty)
            {
                errorProvider1.SetError(txtStreet, "Street Address is required.");
                errMsg = errMsg + Environment.NewLine + "Street Address is required.";
            }
            if (txtMunicipality.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Municipality is required.";
                errorProvider1.SetError(txtMunicipality, "Municipality is required.");
            }
            //Don't really need this
            if (cmbProvince.SelectedIndex < 0 && cmbCountry.SelectedValue.ToString() == "CA")
            {
                errMsg = errMsg + Environment.NewLine + "You must select a province.";
            }
            if (txtPostal.Text == string.Empty || txtPostal.Text == "A#A#A#" || txtPostal.Text == "#####")
            {
                errMsg = errMsg + Environment.NewLine + "Postal Code is required.";
                errorProvider1.SetError(txtPostal, "Postal Code is required.");
            }
            if(txtPostal.Text != string.Empty && cmbCountry.SelectedValue.ToString() == "CA" && txtPostal.Text != "A#A#A#")
            {
               var regex = @"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]";
                Match m = Regex.Match(txtPostal.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if(!m.Success){
                    errMsg = errMsg + Environment.NewLine + "Postal Code format is invalid.";
                    errorProvider1.SetError(txtPostal, "Postal Code format is invalid.");
                }
            }
            if (txtPostal.Text != string.Empty && cmbCountry.SelectedValue.ToString() == "USA" && txtPostal.Text != "#####")
            {
                var regex = @"[0-9][0-9][0-9][0-9][0-9]";
                Match m = Regex.Match(txtPostal.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Zip Code format is invalid.";
                    errorProvider1.SetError(txtPostal, "Zip Code format is invalid.");
                }
            }
            if (txtWorkNum.Text == "###-###-####" || txtWorkNum.Text == string.Empty) 
            {
                errMsg = errMsg + Environment.NewLine + "Work Number is required.";
                errorProvider1.SetError(txtWorkNum, "Work Number is required.");
            }
            if(txtWorkNum.Text != "###-###-####" && txtWorkNum.Text != string.Empty)
            {
                var regex = @"[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]";
                Match m = Regex.Match(txtWorkNum.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Work Number format is invalid.";
                    errorProvider1.SetError(txtWorkNum, "Work Number format is invalid.");
                }
            }
            if (txtCellNum.Text == "###-###-####" || txtCellNum.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Cell Number is required.";
                errorProvider1.SetError(txtCellNum, "Cell Number is required.");
            }
            if(txtCellNum.Text != string.Empty && txtCellNum.Text != "###-###-####")
            {
                var regex = @"[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]";
                Match m = Regex.Match(txtCellNum.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Phone Number format is invalid.";
                    errorProvider1.SetError(txtCellNum, "Cell Number format is invalid.");
                }
            }
            if (txtSIN.Text == string.Empty || txtSIN.Text == "#########")
            {
                errMsg = errMsg + Environment.NewLine + "SIN is required.";
                errorProvider1.SetError(txtSIN, "SIN is required");
            }
            if (txtSIN.Text != string.Empty && txtSIN.Text != "#########")
            {
                var regex = @"[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]";
                Match m = Regex.Match(txtSIN.Text.ToString().Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "SIN must be 9 digits.";
                    errorProvider1.SetError(txtSIN, "SIN must be 9 digits");
                }
            }
            if (txtEmail.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Email is required.";
                errorProvider1.SetError(txtEmail, "Email is required");
            }
            if (txtEmail.Text != string.Empty)
            {
                var regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                Match m = Regex.Match(txtEmail.Text.Trim(), regex, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    errMsg = errMsg + Environment.NewLine + "Email format is invalid.";
                    errorProvider1.SetError(txtEmail, "Email format is invalid");
                }
            }
            if(dtpDOB.Value > DateTime.Today.AddYears(-18))
            {
                errMsg = errMsg + Environment.NewLine + "Employee must be at least 18 years of age.";
                errorProvider1.SetError(dtpDOB, "Employee must be at least 18 years of age.");
            }
            if(cmbJobAssignment.SelectedIndex < 0)
            {
                errMsg = errMsg + Environment.NewLine + "You must assign a job.";
                errorProvider1.SetError(cmbJobAssignment, "You must assign a job.");
            }
            if(txtOfficeLocation.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Office Location is required.";
                errorProvider1.SetError(txtOfficeLocation, "Office Location is required");
            }
            if(cmbDepartment.SelectedIndex < 0)
            {
                errMsg = errMsg + Environment.NewLine + "You must assign a department.";
                errorProvider1.SetError(cmbDepartment, "You must assign a department.");
            }
            if(dtpJobStart.Value < dtpSeniority.Value)
            {
                errMsg = errMsg + Environment.NewLine + "Job start date cannot be sooner than seniority date.";
                errorProvider1.SetError(dtpJobStart, "Job start date cannot be sooner than seniority date.");
            }
            if (cmbSupervisor.SelectedIndex < 0 && chkIsSuper.Checked == false)
            {
                errMsg = errMsg + Environment.NewLine + "You must assign a supervisor.";
                errorProvider1.SetError(cmbSupervisor, "You must assign a supervisor.");
            }
            if (txtPassword.Text == string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + "Password is required.";
                errorProvider1.SetError(txtPassword, "Password is required.");
            }
            //Only make this check after all forms are filled
            if ((numEmp >= (numSup * 10) + numSup * 1) && chkIsSuper.Checked == false && txtPassword.Text != string.Empty)
            {
                errMsg = errMsg + Environment.NewLine + $"There are {numEmp} employees in {cmbDepartment.Text} and only {numSup} supervisor(s). There must be 1 supervisor per 10 employees.";
                errorProvider1.SetError(btnCreate, $"There are {numEmp} employees in {cmbDepartment.Text} and only {numSup} supervisor(s). There must be 1 supervisor per 10 employees.");
            }

            return errMsg;
        }

        private Employee PopulateEmployeeObject()
        {
            return new Employee
            {
                DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue),
                JobId = Convert.ToInt32(cmbJobAssignment.SelectedValue),
                Password = txtPassword.Text.Trim(),
                FirstName = txtFName.Text.Trim(),
                MiddleInitial = txtMI.Text.Trim(),
                LastName = txtLName.Text.Trim(),
                SIN = txtSIN.Text.Trim(),
                DOB = Convert.ToDateTime(dtpDOB.Value),
                StreetAddress = txtStreet.Text.Trim(),
                Municipality = txtMunicipality.Text.Trim(),
                Province = cmbProvince.SelectedValue.ToString(),
                Country = cmbCountry.SelectedValue.ToString(),
                PostalCode = txtPostal.Text.Trim(),
                SeniorityDate = Convert.ToDateTime(dtpSeniority.Value),
                JobStartDate = Convert.ToDateTime(dtpJobStart.Value),
                WorkPhone = txtWorkNum.Text.Trim(),
                CellPhone = txtCellNum.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                SupervisorId = Convert.ToInt32(cmbSupervisor.SelectedValue),
                Active = 1,
                OfficeLocation = txtOfficeLocation.Text.Trim()
            };
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
            if(cmbDepartment.SelectedIndex == 0)
            {
                LoadSupervisorsByDepartment(Convert.ToInt32(1));
            }
            if (cmbDepartment.SelectedIndex > 0)
            {
                LoadSupervisorsByDepartment(Convert.ToInt32(cmbDepartment.SelectedValue));
            }                
        }

        private void chkIsSuper_CheckedChanged(object sender, EventArgs e)
        {
            cmbSupervisor.SelectedIndex = -1;
            cmbSupervisor.Enabled = !(chkIsSuper.CheckState == CheckState.Checked);
        }

        //Can clean up this code, but works for now.
        private void ClearForm()
        {
            txtFName.Text = string.Empty;
            txtMI.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtMunicipality.Text = string.Empty;
            txtPostal.Text = string.Empty;
            txtWorkNum.Text = string.Empty;
            txtCellNum.Text = string.Empty;
            txtSIN.Text = string.Empty;
            txtEmail.Text = string.Empty;
            dtpDOB.Text = DateTime.Now.ToShortTimeString();
            cmbJobAssignment.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            dtpSeniority.Text = DateTime.Now.ToShortTimeString();
            dtpJobStart.Text = DateTime.Now.ToShortTimeString();
            cmbSupervisor.SelectedIndex = -1;
            chkIsSuper.Checked = false;
            txtPassword.Text = string.Empty;
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCountry.SelectedValue.ToString() == "USA")
            {
                txtPostal.Text = "#####";
                cmbProvince.DataSource = Enum.GetValues(typeof(EmployeeEnums.States));
                lblProvince.Text = "State";
                lblPostal.Text = "Zip Code";
            }
            if(cmbCountry.SelectedValue.ToString() == "CA")
            {
                txtPostal.Text = "A#A#A#";
                cmbProvince.DataSource = Enum.GetValues(typeof(EmployeeEnums.Province));
                lblProvince.Text = "Province";
                lblPostal.Text = "Postal Code";
            }
        }

        //Can update with these with a "Focus" and "Unfocus" case
        private void txtPostal_MouseEnter(object sender, EventArgs e)
        {
            if(txtPostal.Text == "A#A#A#" || txtPostal.Text == "#####")
            {
                txtPostal.Text = string.Empty;
            }
        }

        private void txtWorkNum_MouseEnter(object sender, EventArgs e)
        {
            if (txtWorkNum.Text == "###-###-####")
            {
                txtWorkNum.Text = string.Empty;
            }

        }

        private void txtCellNum_MouseEnter(object sender, EventArgs e)
        {
            if (txtCellNum.Text == "###-###-####")
            {
                txtCellNum.Text = string.Empty;
            }
        }

        private void txtSIN_MouseEnter(object sender, EventArgs e)
        {
            if(txtSIN.Text == "#########")
            {
                txtSIN.Text = string.Empty;
            }
        }

        private void txtPostal_MouseLeave(object sender, EventArgs e)
        {
            if(txtPostal.Text == string.Empty && cmbCountry.SelectedValue.ToString() == "CA")
            {
                txtPostal.Text = "A#A#A#";
            }
            if(txtPostal.Text == string.Empty && cmbCountry.SelectedValue.ToString() == "USA")
            {
                txtPostal.Text = "#####";
            }
        }

        private void txtWorkNum_MouseLeave(object sender, EventArgs e)
        {
            if (txtWorkNum.Text == string.Empty)
            {
                txtWorkNum.Text = "###-###-####";
            }
        }

        private void txtCellNum_MouseLeave(object sender, EventArgs e)
        {
            if (txtCellNum.Text == string.Empty)
            {
                txtCellNum.Text = "###-###-####";
            }
        }

        private void txtSIN_MouseLeave(object sender, EventArgs e)
        {
            if (txtSIN.Text == string.Empty)
            {
                txtSIN.Text = "#########";
            }
        }

        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        private byte[] CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));
            return hashValue;
        }

    }
}
