using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TEConsulting.Model;
using TEConsulting.Service;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Collections.Generic;

namespace TEConsulting
{
    public partial class Login : Form
    {
        public string Email { get; set; }
        public Employee Employee { get; private set; }


        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, System.EventArgs e)
        {
            this.Text = Application.ProductName + " - Login";
            txtPass.UseSystemPasswordChar = true;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            errorProvider1.Clear();
            string errMsg = "";
            try
            {
                if (txtEmail.Text == string.Empty)
                {
                    errorProvider1.SetError(txtEmail, "Email is required");
                    errMsg += "Email is required.";
                    txtEmail.Focus();
                }
                if (txtPass.Text == string.Empty)
                {
                    errorProvider1.SetError(txtPass, "Password is required");
                    errMsg += "\nPassword is required.";
                    txtEmail.Focus();
                }
                if (txtPass.Text == string.Empty || txtEmail.Text == string.Empty)
                {
                    MessageBox.Show(errMsg, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    errorProvider1.Clear();
                    Email = txtEmail.Text;
                    string pass = txtPass.Text;
                    //Commented for now since most employee records have unhashed passwords
                    //string result = ValidateLogin(Email, ComputeHash(pass, new SHA256CryptoServiceProvider()));
                    string result = ValidateLogin(Email, pass);

                    if (result == "retired")
                    {
                        MessageBox.Show("This employee's account has been retired.");
                    }
                    if (result == "terminated")
                    {
                        MessageBox.Show("This employee's account has been terminated.");
                    }
                    if (result == "ok")
                    {
                       
                        

                        if (Employee.DepartmentId == 1)
                        {
                            //Using a table to maintain a single date is redundant, used file date instead
                            string path = @"C:\Users\pokem\Documents\GitHub\capstone-tdonovannbcc\Desktop\TEConsulting\TEConsulting.Extras\lastlogin.txt";
                            if (!File.Exists(path))
                            {
                                File.Create(path);
                            }

                            //Uncomment this to test functionality
                            //File.SetLastWriteTime(path, DateTime.Now.AddMonths(-5));

                            // Get the creation time of our record object
                            DateTime dt = File.GetLastWriteTime(path);

                            if (Math.Abs(dt.Subtract(DateTime.Now).TotalHours) <= 24)
                            {
                                //No emails are sent
                                //Shows resulting hours MessageBox.Show(Math.Abs(dt.Subtract(DateTime.Now).TotalHours).ToString());
                            }
                            else
                            {
                                //Set date to today so that emails are only sent once a day
                                File.SetLastWriteTime(path, DateTime.Now);

                                //Construct email sender
                                MailMessage mailMessage = new MailMessage();
                                LookupsService lookupsService = new LookupsService();
                                List<SupervisorLookupDTO> sups = lookupsService.GetSupervisors();


                                foreach (SupervisorLookupDTO supervisor in sups)
                                {
                                    //If the supervisor has employees for review, send them an email
                                    if (lookupsService.GetEmployeesForReview(supervisor.SupervisorId).Count > 0)
                                    {
                                        mailMessage.To.Clear();
                                        mailMessage.To.Add(supervisor.Email);

                                        List<Employee> hrEmps = lookupsService.GetEmployeesByDepartment(1);
                                        //If today's date is within the second month of the quarter
                                        //(more than 30 days) CC HR staff to Email.

                                        //this can be condensed but leave as is due to time constraints
                                        if(DateTime.Now.Month <= 3 && DateTime.Now.Month > 1)
                                        {
                                            foreach(Employee hrEmp in hrEmps)
                                            {
                                                mailMessage.CC.Add(hrEmp.Email);
                                            }
                                        }
                                        if (DateTime.Now.Month <= 6 && DateTime.Now.Month > 4)
                                        {
                                            foreach (Employee hrEmp in hrEmps)
                                            {
                                                mailMessage.CC.Add(hrEmp.Email);
                                            }
                                        }
                                        if (DateTime.Now.Month <= 9 && DateTime.Now.Month > 7)
                                        {
                                            foreach (Employee hrEmp in hrEmps)
                                            {
                                                mailMessage.CC.Add(hrEmp.Email);
                                            }
                                        }
                                        if (DateTime.Now.Month <= 12 && DateTime.Now.Month > 10)
                                        {
                                            foreach (Employee hrEmp in hrEmps)
                                            {
                                                mailMessage.CC.Add(hrEmp.Email);
                                            }
                                        }
                                        mailMessage.From = new MailAddress("admin@teconsulting.com");
                                        mailMessage.Subject = "Outstanding Employee Reviews";
                                        mailMessage.IsBodyHtml = true;
                                        mailMessage.Body = $"Hello {supervisor.SupervisorName} <br/>" +
                                            $"<br/>You must complete reviews for the following employees by the end of the current quarter." +
                                            $"<br/>";
                                        List<ReviewEmployeeDTO> emps = lookupsService.GetEmployeesForReview(supervisor.SupervisorId);
                                        foreach (ReviewEmployeeDTO emp in emps)
                                        {
                                            mailMessage.Body += $"<br/>{emp.FullName}";
                                        }
                                        mailMessage.Body += "<br/><br/>Thank you,<br/><br/>TEConsulting";
                                        SmtpClient smtpClient = new SmtpClient("localhost");
                                        smtpClient.Send(mailMessage);
                                    }
                                }
                            }
                        }
                        DialogResult = DialogResult.OK;
                    }
                    txtPass.Clear();
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(f.Message.ToString());
                txtPass.Clear();
            }
        }

        private string ValidateLogin(string email, string pass)
        {
            LookupsService service = new LookupsService();

            Employee = service.GetEmployeeIdLogin(email, pass);
            if (Employee.Active == 2)
            {
                return "retired";
            }
            if (Employee.Active == 3)
            {
                return "terminated";
            }
            return "ok";
        }

        private byte[] CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));
            return hashValue;
        }

        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
