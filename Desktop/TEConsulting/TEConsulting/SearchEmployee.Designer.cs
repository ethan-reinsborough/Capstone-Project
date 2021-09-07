
namespace TEConsulting
{
    partial class SearchEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchEmployee));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.dgvSearch = new System.Windows.Forms.DataGridView();
            this.EmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MailAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CellNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpBox1 = new System.Windows.Forms.GroupBox();
            this.grpJobStatus = new System.Windows.Forms.GroupBox();
            this.lblR = new System.Windows.Forms.Label();
            this.dtpRetirement = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpHidden = new System.Windows.Forms.DateTimePicker();
            this.txtStatusSIN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnUpdateJStatus = new System.Windows.Forms.Button();
            this.grpJobInfo = new System.Windows.Forms.GroupBox();
            this.chkSupervisor = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbJobAssignment = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSupervisor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJobSIN = new System.Windows.Forms.TextBox();
            this.BtnJobInfoUpdate = new System.Windows.Forms.Button();
            this.grpPInfo = new System.Windows.Forms.GroupBox();
            this.BtnUpdatePInfo = new System.Windows.Forms.Button();
            this.txtMun = new System.Windows.Forms.TextBox();
            this.lblDOB = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblMI = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblLName = new System.Windows.Forms.Label();
            this.txtF = new System.Windows.Forms.TextBox();
            this.lblCellNum = new System.Windows.Forms.Label();
            this.txtM = new System.Windows.Forms.TextBox();
            this.lblWorkNum = new System.Windows.Forms.Label();
            this.txtL = new System.Windows.Forms.TextBox();
            this.lblPostal = new System.Windows.Forms.Label();
            this.txtSA = new System.Windows.Forms.TextBox();
            this.lblProvince = new System.Windows.Forms.Label();
            this.lblStreet = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblMunicip = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtPostal = new System.Windows.Forms.TextBox();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtWNum = new System.Windows.Forms.TextBox();
            this.cmbProvince = new System.Windows.Forms.ComboBox();
            this.txtCNum = new System.Windows.Forms.TextBox();
            this.BtnBack = new System.Windows.Forms.Button();
            this.lblSelectedId = new System.Windows.Forms.Label();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.rdoRetirement = new System.Windows.Forms.RadioButton();
            this.rdoJobInformation = new System.Windows.Forms.RadioButton();
            this.rdoPersonal = new System.Windows.Forms.RadioButton();
            this.lblChooseModify = new System.Windows.Forms.Label();
            this.BtnModify = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rdoLName = new System.Windows.Forms.RadioButton();
            this.rdoID = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.grpBox1.SuspendLayout();
            this.grpJobStatus.SuspendLayout();
            this.grpJobInfo.SuspendLayout();
            this.grpPInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(6, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(327, 31);
            this.txtSearch.TabIndex = 0;
            // 
            // BtnSearch
            // 
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSearch.Location = new System.Drawing.Point(97, 57);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(115, 40);
            this.BtnSearch.TabIndex = 2;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(341, 61);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(0, 13);
            this.lblOutput.TabIndex = 3;
            // 
            // dgvSearch
            // 
            this.dgvSearch.AllowUserToAddRows = false;
            this.dgvSearch.AllowUserToDeleteRows = false;
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeID,
            this.FirstName,
            this.MI,
            this.LastName,
            this.MailAddress,
            this.WorkNum,
            this.CellNum,
            this.Email});
            this.dgvSearch.Location = new System.Drawing.Point(6, 111);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.ReadOnly = true;
            this.dgvSearch.Size = new System.Drawing.Size(839, 441);
            this.dgvSearch.TabIndex = 4;
            this.dgvSearch.SelectionChanged += new System.EventHandler(this.dgvSearch_SelectionChanged);
            // 
            // EmployeeID
            // 
            this.EmployeeID.HeaderText = "EmployeeID";
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.ReadOnly = true;
            this.EmployeeID.Visible = false;
            // 
            // FirstName
            // 
            this.FirstName.HeaderText = "First Name";
            this.FirstName.Name = "FirstName";
            this.FirstName.ReadOnly = true;
            // 
            // MI
            // 
            this.MI.HeaderText = "Middle Initial";
            this.MI.Name = "MI";
            this.MI.ReadOnly = true;
            // 
            // LastName
            // 
            this.LastName.HeaderText = "Last Name";
            this.LastName.Name = "LastName";
            this.LastName.ReadOnly = true;
            // 
            // MailAddress
            // 
            this.MailAddress.HeaderText = "Mailing Address";
            this.MailAddress.Name = "MailAddress";
            this.MailAddress.ReadOnly = true;
            this.MailAddress.Width = 200;
            // 
            // WorkNum
            // 
            this.WorkNum.HeaderText = "Work Number";
            this.WorkNum.Name = "WorkNum";
            this.WorkNum.ReadOnly = true;
            // 
            // CellNum
            // 
            this.CellNum.HeaderText = "Cell Number";
            this.CellNum.Name = "CellNum";
            this.CellNum.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // grpBox1
            // 
            this.grpBox1.Controls.Add(this.grpJobStatus);
            this.grpBox1.Controls.Add(this.grpJobInfo);
            this.grpBox1.Controls.Add(this.grpPInfo);
            this.grpBox1.Controls.Add(this.BtnBack);
            this.grpBox1.Controls.Add(this.lblSelectedId);
            this.grpBox1.Controls.Add(this.BtnSelect);
            this.grpBox1.Controls.Add(this.rdoRetirement);
            this.grpBox1.Controls.Add(this.rdoJobInformation);
            this.grpBox1.Controls.Add(this.rdoPersonal);
            this.grpBox1.Controls.Add(this.lblChooseModify);
            this.grpBox1.Controls.Add(this.BtnModify);
            this.grpBox1.Controls.Add(this.pictureBox1);
            this.grpBox1.Controls.Add(this.rdoLName);
            this.grpBox1.Controls.Add(this.rdoID);
            this.grpBox1.Controls.Add(this.dgvSearch);
            this.grpBox1.Controls.Add(this.BtnSearch);
            this.grpBox1.Controls.Add(this.txtSearch);
            this.grpBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.grpBox1.Location = new System.Drawing.Point(71, 20);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(851, 560);
            this.grpBox1.TabIndex = 5;
            this.grpBox1.TabStop = false;
            this.grpBox1.Text = "Search by Employee ID or Last Name";
            // 
            // grpJobStatus
            // 
            this.grpJobStatus.Controls.Add(this.lblR);
            this.grpJobStatus.Controls.Add(this.dtpRetirement);
            this.grpJobStatus.Controls.Add(this.label7);
            this.grpJobStatus.Controls.Add(this.cmbStatus);
            this.grpJobStatus.Controls.Add(this.label6);
            this.grpJobStatus.Controls.Add(this.dtpHidden);
            this.grpJobStatus.Controls.Add(this.txtStatusSIN);
            this.grpJobStatus.Controls.Add(this.label5);
            this.grpJobStatus.Controls.Add(this.BtnUpdateJStatus);
            this.grpJobStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.grpJobStatus.Location = new System.Drawing.Point(244, 202);
            this.grpJobStatus.Name = "grpJobStatus";
            this.grpJobStatus.Size = new System.Drawing.Size(322, 316);
            this.grpJobStatus.TabIndex = 17;
            this.grpJobStatus.TabStop = false;
            this.grpJobStatus.Text = "Job Status Information";
            this.grpJobStatus.Visible = false;
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.Location = new System.Drawing.Point(57, 192);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(84, 13);
            this.lblR.TabIndex = 48;
            this.lblR.Text = "Retirement Date";
            this.lblR.Visible = false;
            // 
            // dtpRetirement
            // 
            this.dtpRetirement.Location = new System.Drawing.Point(60, 207);
            this.dtpRetirement.Name = "dtpRetirement";
            this.dtpRetirement.Size = new System.Drawing.Size(200, 20);
            this.dtpRetirement.TabIndex = 47;
            this.dtpRetirement.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 46;
            this.label7.Text = "Employement Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(61, 162);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 21);
            this.cmbStatus.TabIndex = 45;
            this.cmbStatus.SelectionChangeCommitted += new System.EventHandler(this.cmbStatus_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Seniority Date";
            // 
            // dtpHidden
            // 
            this.dtpHidden.Enabled = false;
            this.dtpHidden.Location = new System.Drawing.Point(61, 120);
            this.dtpHidden.Name = "dtpHidden";
            this.dtpHidden.Size = new System.Drawing.Size(200, 20);
            this.dtpHidden.TabIndex = 44;
            // 
            // txtStatusSIN
            // 
            this.txtStatusSIN.Enabled = false;
            this.txtStatusSIN.Location = new System.Drawing.Point(61, 76);
            this.txtStatusSIN.Name = "txtStatusSIN";
            this.txtStatusSIN.Size = new System.Drawing.Size(200, 20);
            this.txtStatusSIN.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "SIN";
            // 
            // BtnUpdateJStatus
            // 
            this.BtnUpdateJStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUpdateJStatus.Location = new System.Drawing.Point(101, 246);
            this.BtnUpdateJStatus.Name = "BtnUpdateJStatus";
            this.BtnUpdateJStatus.Size = new System.Drawing.Size(115, 40);
            this.BtnUpdateJStatus.TabIndex = 28;
            this.BtnUpdateJStatus.Text = "Update";
            this.BtnUpdateJStatus.UseVisualStyleBackColor = true;
            this.BtnUpdateJStatus.Click += new System.EventHandler(this.BtnUpdateJStatus_Click);
            // 
            // grpJobInfo
            // 
            this.grpJobInfo.Controls.Add(this.chkSupervisor);
            this.grpJobInfo.Controls.Add(this.label4);
            this.grpJobInfo.Controls.Add(this.cmbJobAssignment);
            this.grpJobInfo.Controls.Add(this.label2);
            this.grpJobInfo.Controls.Add(this.cmbSupervisor);
            this.grpJobInfo.Controls.Add(this.label3);
            this.grpJobInfo.Controls.Add(this.lbl2);
            this.grpJobInfo.Controls.Add(this.cmbDepartment);
            this.grpJobInfo.Controls.Add(this.dtpStartDate);
            this.grpJobInfo.Controls.Add(this.label1);
            this.grpJobInfo.Controls.Add(this.txtJobSIN);
            this.grpJobInfo.Controls.Add(this.BtnJobInfoUpdate);
            this.grpJobInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.grpJobInfo.Location = new System.Drawing.Point(439, 203);
            this.grpJobInfo.Name = "grpJobInfo";
            this.grpJobInfo.Size = new System.Drawing.Size(389, 316);
            this.grpJobInfo.TabIndex = 16;
            this.grpJobInfo.TabStop = false;
            this.grpJobInfo.Text = "Job Information";
            this.grpJobInfo.Visible = false;
            // 
            // chkSupervisor
            // 
            this.chkSupervisor.AutoSize = true;
            this.chkSupervisor.Location = new System.Drawing.Point(214, 185);
            this.chkSupervisor.Name = "chkSupervisor";
            this.chkSupervisor.Size = new System.Drawing.Size(86, 17);
            this.chkSupervisor.TabIndex = 27;
            this.chkSupervisor.Text = "is Supervisor";
            this.chkSupervisor.UseVisualStyleBackColor = true;
            this.chkSupervisor.CheckedChanged += new System.EventHandler(this.chkSupervisor_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Job Assignment";
            // 
            // cmbJobAssignment
            // 
            this.cmbJobAssignment.FormattingEnabled = true;
            this.cmbJobAssignment.Location = new System.Drawing.Point(91, 226);
            this.cmbJobAssignment.Name = "cmbJobAssignment";
            this.cmbJobAssignment.Size = new System.Drawing.Size(200, 21);
            this.cmbJobAssignment.TabIndex = 25;
            this.cmbJobAssignment.SelectionChangeCommitted += new System.EventHandler(this.cmbJobAssignment_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Supervisor";
            // 
            // cmbSupervisor
            // 
            this.cmbSupervisor.FormattingEnabled = true;
            this.cmbSupervisor.Location = new System.Drawing.Point(91, 183);
            this.cmbSupervisor.Name = "cmbSupervisor";
            this.cmbSupervisor.Size = new System.Drawing.Size(117, 21);
            this.cmbSupervisor.TabIndex = 23;
            this.cmbSupervisor.SelectedIndexChanged += new System.EventHandler(this.cmbSupervisor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Department";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(88, 80);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(55, 13);
            this.lbl2.TabIndex = 21;
            this.lbl2.Text = "Start Date";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(91, 138);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(200, 21);
            this.cmbDepartment.TabIndex = 20;
            this.cmbDepartment.SelectedIndexChanged += new System.EventHandler(this.cmbDepartment_SelectedIndexChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(91, 94);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "SIN";
            // 
            // txtJobSIN
            // 
            this.txtJobSIN.Location = new System.Drawing.Point(92, 48);
            this.txtJobSIN.Name = "txtJobSIN";
            this.txtJobSIN.Size = new System.Drawing.Size(200, 20);
            this.txtJobSIN.TabIndex = 17;
            // 
            // BtnJobInfoUpdate
            // 
            this.BtnJobInfoUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnJobInfoUpdate.Location = new System.Drawing.Point(133, 264);
            this.BtnJobInfoUpdate.Name = "BtnJobInfoUpdate";
            this.BtnJobInfoUpdate.Size = new System.Drawing.Size(115, 40);
            this.BtnJobInfoUpdate.TabIndex = 16;
            this.BtnJobInfoUpdate.Text = "Update";
            this.BtnJobInfoUpdate.UseVisualStyleBackColor = true;
            this.BtnJobInfoUpdate.Click += new System.EventHandler(this.BtnJobInfoUpdate_Click);
            // 
            // grpPInfo
            // 
            this.grpPInfo.Controls.Add(this.BtnUpdatePInfo);
            this.grpPInfo.Controls.Add(this.txtMun);
            this.grpPInfo.Controls.Add(this.lblDOB);
            this.grpPInfo.Controls.Add(this.lblFirstName);
            this.grpPInfo.Controls.Add(this.lblPassword);
            this.grpPInfo.Controls.Add(this.lblMI);
            this.grpPInfo.Controls.Add(this.lblEmail);
            this.grpPInfo.Controls.Add(this.lblLName);
            this.grpPInfo.Controls.Add(this.txtF);
            this.grpPInfo.Controls.Add(this.lblCellNum);
            this.grpPInfo.Controls.Add(this.txtM);
            this.grpPInfo.Controls.Add(this.lblWorkNum);
            this.grpPInfo.Controls.Add(this.txtL);
            this.grpPInfo.Controls.Add(this.lblPostal);
            this.grpPInfo.Controls.Add(this.txtSA);
            this.grpPInfo.Controls.Add(this.lblProvince);
            this.grpPInfo.Controls.Add(this.lblStreet);
            this.grpPInfo.Controls.Add(this.lblCountry);
            this.grpPInfo.Controls.Add(this.lblMunicip);
            this.grpPInfo.Controls.Add(this.txtPass);
            this.grpPInfo.Controls.Add(this.txtPostal);
            this.grpPInfo.Controls.Add(this.dtpDOB);
            this.grpPInfo.Controls.Add(this.cmbCountry);
            this.grpPInfo.Controls.Add(this.txtEmail);
            this.grpPInfo.Controls.Add(this.txtWNum);
            this.grpPInfo.Controls.Add(this.cmbProvince);
            this.grpPInfo.Controls.Add(this.txtCNum);
            this.grpPInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.grpPInfo.Location = new System.Drawing.Point(21, 203);
            this.grpPInfo.Name = "grpPInfo";
            this.grpPInfo.Size = new System.Drawing.Size(389, 316);
            this.grpPInfo.TabIndex = 6;
            this.grpPInfo.TabStop = false;
            this.grpPInfo.Text = "Personal Information";
            this.grpPInfo.Visible = false;
            // 
            // BtnUpdatePInfo
            // 
            this.BtnUpdatePInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUpdatePInfo.Location = new System.Drawing.Point(133, 261);
            this.BtnUpdatePInfo.Name = "BtnUpdatePInfo";
            this.BtnUpdatePInfo.Size = new System.Drawing.Size(115, 40);
            this.BtnUpdatePInfo.TabIndex = 16;
            this.BtnUpdatePInfo.Text = "Update";
            this.BtnUpdatePInfo.UseVisualStyleBackColor = true;
            this.BtnUpdatePInfo.Click += new System.EventHandler(this.BtnUpdatePInfo_Click);
            // 
            // txtMun
            // 
            this.txtMun.Location = new System.Drawing.Point(145, 87);
            this.txtMun.Name = "txtMun";
            this.txtMun.Size = new System.Drawing.Size(100, 20);
            this.txtMun.TabIndex = 24;
            // 
            // lblDOB
            // 
            this.lblDOB.AutoSize = true;
            this.lblDOB.Location = new System.Drawing.Point(22, 203);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(66, 13);
            this.lblDOB.TabIndex = 43;
            this.lblDOB.Text = "Date of Birth";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(22, 22);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(57, 13);
            this.lblFirstName.TabIndex = 16;
            this.lblFirstName.Text = "First Name";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(142, 157);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 42;
            this.lblPassword.Text = "Password";
            // 
            // lblMI
            // 
            this.lblMI.AutoSize = true;
            this.lblMI.Location = new System.Drawing.Point(142, 22);
            this.lblMI.Name = "lblMI";
            this.lblMI.Size = new System.Drawing.Size(113, 13);
            this.lblMI.TabIndex = 17;
            this.lblMI.Text = "Middle Initial (Optional)";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(261, 160);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 41;
            this.lblEmail.Text = "Email";
            // 
            // lblLName
            // 
            this.lblLName.AutoSize = true;
            this.lblLName.Location = new System.Drawing.Point(261, 22);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(58, 13);
            this.lblLName.TabIndex = 18;
            this.lblLName.Text = "Last Name";
            // 
            // txtF
            // 
            this.txtF.Location = new System.Drawing.Point(25, 38);
            this.txtF.Name = "txtF";
            this.txtF.Size = new System.Drawing.Size(100, 20);
            this.txtF.TabIndex = 19;
            // 
            // lblCellNum
            // 
            this.lblCellNum.AutoSize = true;
            this.lblCellNum.Location = new System.Drawing.Point(22, 160);
            this.lblCellNum.Name = "lblCellNum";
            this.lblCellNum.Size = new System.Drawing.Size(34, 13);
            this.lblCellNum.TabIndex = 39;
            this.lblCellNum.Text = "Cell #";
            // 
            // txtM
            // 
            this.txtM.Location = new System.Drawing.Point(145, 38);
            this.txtM.Name = "txtM";
            this.txtM.Size = new System.Drawing.Size(100, 20);
            this.txtM.TabIndex = 20;
            // 
            // lblWorkNum
            // 
            this.lblWorkNum.AutoSize = true;
            this.lblWorkNum.Location = new System.Drawing.Point(261, 117);
            this.lblWorkNum.Name = "lblWorkNum";
            this.lblWorkNum.Size = new System.Drawing.Size(43, 13);
            this.lblWorkNum.TabIndex = 38;
            this.lblWorkNum.Text = "Work #";
            // 
            // txtL
            // 
            this.txtL.Location = new System.Drawing.Point(264, 38);
            this.txtL.Name = "txtL";
            this.txtL.Size = new System.Drawing.Size(100, 20);
            this.txtL.TabIndex = 21;
            // 
            // lblPostal
            // 
            this.lblPostal.AutoSize = true;
            this.lblPostal.Location = new System.Drawing.Point(142, 116);
            this.lblPostal.Name = "lblPostal";
            this.lblPostal.Size = new System.Drawing.Size(64, 13);
            this.lblPostal.TabIndex = 37;
            this.lblPostal.Text = "Postal Code";
            // 
            // txtSA
            // 
            this.txtSA.Location = new System.Drawing.Point(25, 87);
            this.txtSA.Name = "txtSA";
            this.txtSA.Size = new System.Drawing.Size(100, 20);
            this.txtSA.TabIndex = 22;
            // 
            // lblProvince
            // 
            this.lblProvince.AutoSize = true;
            this.lblProvince.Location = new System.Drawing.Point(22, 116);
            this.lblProvince.Name = "lblProvince";
            this.lblProvince.Size = new System.Drawing.Size(49, 13);
            this.lblProvince.TabIndex = 36;
            this.lblProvince.Text = "Province";
            // 
            // lblStreet
            // 
            this.lblStreet.AutoSize = true;
            this.lblStreet.Location = new System.Drawing.Point(22, 71);
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.Size = new System.Drawing.Size(76, 13);
            this.lblStreet.TabIndex = 23;
            this.lblStreet.Text = "Street Address";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(261, 71);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(43, 13);
            this.lblCountry.TabIndex = 35;
            this.lblCountry.Text = "Country";
            // 
            // lblMunicip
            // 
            this.lblMunicip.AutoSize = true;
            this.lblMunicip.Location = new System.Drawing.Point(142, 71);
            this.lblMunicip.Name = "lblMunicip";
            this.lblMunicip.Size = new System.Drawing.Size(62, 13);
            this.lblMunicip.TabIndex = 25;
            this.lblMunicip.Text = "Municipality";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(145, 176);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(100, 20);
            this.txtPass.TabIndex = 34;
            // 
            // txtPostal
            // 
            this.txtPostal.Location = new System.Drawing.Point(145, 132);
            this.txtPostal.Name = "txtPostal";
            this.txtPostal.Size = new System.Drawing.Size(100, 20);
            this.txtPostal.TabIndex = 26;
            // 
            // dtpDOB
            // 
            this.dtpDOB.Location = new System.Drawing.Point(25, 219);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(339, 20);
            this.dtpDOB.TabIndex = 33;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(264, 86);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(100, 21);
            this.cmbCountry.TabIndex = 27;
            this.cmbCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(264, 176);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 32;
            // 
            // txtWNum
            // 
            this.txtWNum.Location = new System.Drawing.Point(264, 133);
            this.txtWNum.Name = "txtWNum";
            this.txtWNum.Size = new System.Drawing.Size(100, 20);
            this.txtWNum.TabIndex = 28;
            // 
            // cmbProvince
            // 
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Location = new System.Drawing.Point(25, 132);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(100, 21);
            this.cmbProvince.TabIndex = 29;
            // 
            // txtCNum
            // 
            this.txtCNum.Location = new System.Drawing.Point(25, 176);
            this.txtCNum.Name = "txtCNum";
            this.txtCNum.Size = new System.Drawing.Size(100, 20);
            this.txtCNum.TabIndex = 30;
            // 
            // BtnBack
            // 
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Location = new System.Drawing.Point(19, 27);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(115, 40);
            this.BtnBack.TabIndex = 15;
            this.BtnBack.Text = "Go Back";
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Visible = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblSelectedId
            // 
            this.lblSelectedId.AutoSize = true;
            this.lblSelectedId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedId.Location = new System.Drawing.Point(359, 47);
            this.lblSelectedId.Name = "lblSelectedId";
            this.lblSelectedId.Size = new System.Drawing.Size(51, 20);
            this.lblSelectedId.TabIndex = 14;
            this.lblSelectedId.Text = "label1";
            this.lblSelectedId.Visible = false;
            // 
            // BtnSelect
            // 
            this.BtnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSelect.Location = new System.Drawing.Point(345, 145);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(115, 40);
            this.BtnSelect.TabIndex = 13;
            this.BtnSelect.Text = "Select";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Visible = false;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // rdoRetirement
            // 
            this.rdoRetirement.AutoSize = true;
            this.rdoRetirement.Location = new System.Drawing.Point(472, 111);
            this.rdoRetirement.Name = "rdoRetirement";
            this.rdoRetirement.Size = new System.Drawing.Size(75, 17);
            this.rdoRetirement.TabIndex = 12;
            this.rdoRetirement.TabStop = true;
            this.rdoRetirement.Text = "Job Status";
            this.rdoRetirement.UseVisualStyleBackColor = true;
            this.rdoRetirement.Visible = false;
            // 
            // rdoJobInformation
            // 
            this.rdoJobInformation.AutoSize = true;
            this.rdoJobInformation.Location = new System.Drawing.Point(353, 111);
            this.rdoJobInformation.Name = "rdoJobInformation";
            this.rdoJobInformation.Size = new System.Drawing.Size(97, 17);
            this.rdoJobInformation.TabIndex = 11;
            this.rdoJobInformation.TabStop = true;
            this.rdoJobInformation.Text = "Job Information";
            this.rdoJobInformation.UseVisualStyleBackColor = true;
            this.rdoJobInformation.Visible = false;
            // 
            // rdoPersonal
            // 
            this.rdoPersonal.AutoSize = true;
            this.rdoPersonal.Location = new System.Drawing.Point(212, 111);
            this.rdoPersonal.Name = "rdoPersonal";
            this.rdoPersonal.Size = new System.Drawing.Size(121, 17);
            this.rdoPersonal.TabIndex = 10;
            this.rdoPersonal.TabStop = true;
            this.rdoPersonal.Text = "Personal Information";
            this.rdoPersonal.UseVisualStyleBackColor = true;
            this.rdoPersonal.Visible = false;
            // 
            // lblChooseModify
            // 
            this.lblChooseModify.AutoSize = true;
            this.lblChooseModify.Location = new System.Drawing.Point(302, 80);
            this.lblChooseModify.Name = "lblChooseModify";
            this.lblChooseModify.Size = new System.Drawing.Size(202, 13);
            this.lblChooseModify.TabIndex = 9;
            this.lblChooseModify.Text = "Which information do you wish to modify?";
            this.lblChooseModify.Visible = false;
            // 
            // BtnModify
            // 
            this.BtnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnModify.Location = new System.Drawing.Point(218, 57);
            this.BtnModify.Name = "BtnModify";
            this.BtnModify.Size = new System.Drawing.Size(115, 40);
            this.BtnModify.TabIndex = 8;
            this.BtnModify.Text = "Modify";
            this.BtnModify.UseVisualStyleBackColor = true;
            this.BtnModify.Click += new System.EventHandler(this.BtnModify_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(718, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 78);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // rdoLName
            // 
            this.rdoLName.AutoSize = true;
            this.rdoLName.Location = new System.Drawing.Point(6, 80);
            this.rdoLName.Name = "rdoLName";
            this.rdoLName.Size = new System.Drawing.Size(90, 17);
            this.rdoLName.TabIndex = 6;
            this.rdoLName.TabStop = true;
            this.rdoLName.Text = "by Last Name";
            this.rdoLName.UseVisualStyleBackColor = true;
            // 
            // rdoID
            // 
            this.rdoID.AutoSize = true;
            this.rdoID.Location = new System.Drawing.Point(6, 57);
            this.rdoID.Name = "rdoID";
            this.rdoID.Size = new System.Drawing.Size(50, 17);
            this.rdoID.TabIndex = 5;
            this.rdoID.TabStop = true;
            this.rdoID.Text = "by ID";
            this.rdoID.UseVisualStyleBackColor = true;
            // 
            // SearchEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(48)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(1020, 592);
            this.Controls.Add(this.grpBox1);
            this.Controls.Add(this.lblOutput);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Employee";
            this.Load += new System.EventHandler(this.SearchEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.grpBox1.ResumeLayout(false);
            this.grpBox1.PerformLayout();
            this.grpJobStatus.ResumeLayout(false);
            this.grpJobStatus.PerformLayout();
            this.grpJobInfo.ResumeLayout(false);
            this.grpJobInfo.PerformLayout();
            this.grpPInfo.ResumeLayout(false);
            this.grpPInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.DataGridView dgvSearch;
        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.RadioButton rdoLName;
        private System.Windows.Forms.RadioButton rdoID;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MI;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MailAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CellNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.Button BtnModify;
        private System.Windows.Forms.Label lblChooseModify;
        private System.Windows.Forms.RadioButton rdoRetirement;
        private System.Windows.Forms.RadioButton rdoJobInformation;
        private System.Windows.Forms.RadioButton rdoPersonal;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.Label lblSelectedId;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.GroupBox grpPInfo;
        private System.Windows.Forms.Button BtnUpdatePInfo;
        private System.Windows.Forms.TextBox txtMun;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblMI;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.TextBox txtF;
        private System.Windows.Forms.Label lblCellNum;
        private System.Windows.Forms.TextBox txtM;
        private System.Windows.Forms.Label lblWorkNum;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.Label lblPostal;
        private System.Windows.Forms.TextBox txtSA;
        private System.Windows.Forms.Label lblProvince;
        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblMunicip;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtPostal;
        private System.Windows.Forms.DateTimePicker dtpDOB;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtWNum;
        private System.Windows.Forms.ComboBox cmbProvince;
        private System.Windows.Forms.TextBox txtCNum;
        private System.Windows.Forms.GroupBox grpJobInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSupervisor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJobSIN;
        private System.Windows.Forms.Button BtnJobInfoUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbJobAssignment;
        private System.Windows.Forms.CheckBox chkSupervisor;
        private System.Windows.Forms.DateTimePicker dtpHidden;
        private System.Windows.Forms.GroupBox grpJobStatus;
        private System.Windows.Forms.Button BtnUpdateJStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStatusSIN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblR;
        private System.Windows.Forms.DateTimePicker dtpRetirement;
    }
}