
namespace TEConsulting
{
    partial class AddDepartment
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDepartment));
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblInvocationDate = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDeptName = new System.Windows.Forms.TextBox();
            this.dtpInvDate = new System.Windows.Forms.DateTimePicker();
            this.txtDesc = new System.Windows.Forms.RichTextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpHidden = new System.Windows.Forms.DateTimePicker();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cmbDepartments = new System.Windows.Forms.ComboBox();
            this.lblModDept = new System.Windows.Forms.Label();
            this.chkModify = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkDelete = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.lblDepartment.Location = new System.Drawing.Point(44, 84);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(93, 13);
            this.lblDepartment.TabIndex = 0;
            this.lblDepartment.Text = "Department Name";
            // 
            // lblInvocationDate
            // 
            this.lblInvocationDate.AutoSize = true;
            this.lblInvocationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.lblInvocationDate.Location = new System.Drawing.Point(44, 352);
            this.lblInvocationDate.Name = "lblInvocationDate";
            this.lblInvocationDate.Size = new System.Drawing.Size(83, 13);
            this.lblInvocationDate.TabIndex = 1;
            this.lblInvocationDate.Text = "Invocation Date";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.lblDescription.Location = new System.Drawing.Point(44, 144);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            // 
            // txtDeptName
            // 
            this.txtDeptName.BackColor = System.Drawing.SystemColors.Window;
            this.txtDeptName.Location = new System.Drawing.Point(47, 100);
            this.txtDeptName.Name = "txtDeptName";
            this.txtDeptName.Size = new System.Drawing.Size(342, 20);
            this.txtDeptName.TabIndex = 3;
            // 
            // dtpInvDate
            // 
            this.dtpInvDate.Location = new System.Drawing.Point(47, 368);
            this.dtpInvDate.Name = "dtpInvDate";
            this.dtpInvDate.Size = new System.Drawing.Size(342, 20);
            this.dtpInvDate.TabIndex = 4;
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.SystemColors.Window;
            this.txtDesc.Location = new System.Drawing.Point(47, 160);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(342, 169);
            this.txtDesc.TabIndex = 6;
            this.txtDesc.Text = "";
            // 
            // btnCreate
            // 
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Location = new System.Drawing.Point(47, 411);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(342, 56);
            this.btnCreate.TabIndex = 7;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(409, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(261, 301);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDelete);
            this.groupBox1.Controls.Add(this.dtpHidden);
            this.groupBox1.Controls.Add(this.lblVersion);
            this.groupBox1.Controls.Add(this.cmbDepartments);
            this.groupBox1.Controls.Add(this.lblModDept);
            this.groupBox1.Controls.Add(this.chkModify);
            this.groupBox1.Controls.Add(this.txtDesc);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.lblDepartment);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.lblInvocationDate);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.dtpInvDate);
            this.groupBox1.Controls.Add(this.txtDeptName);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.groupBox1.Location = new System.Drawing.Point(36, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(722, 512);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Department Information";
            // 
            // dtpHidden
            // 
            this.dtpHidden.Location = new System.Drawing.Point(47, 394);
            this.dtpHidden.Name = "dtpHidden";
            this.dtpHidden.Size = new System.Drawing.Size(342, 20);
            this.dtpHidden.TabIndex = 13;
            this.dtpHidden.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(406, 433);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 13);
            this.lblVersion.TabIndex = 12;
            // 
            // cmbDepartments
            // 
            this.cmbDepartments.FormattingEnabled = true;
            this.cmbDepartments.Location = new System.Drawing.Point(464, 39);
            this.cmbDepartments.Name = "cmbDepartments";
            this.cmbDepartments.Size = new System.Drawing.Size(152, 21);
            this.cmbDepartments.TabIndex = 11;
            this.cmbDepartments.Visible = false;
            this.cmbDepartments.SelectedIndexChanged += new System.EventHandler(this.cmbDepartments_SelectedIndexChanged);
            // 
            // lblModDept
            // 
            this.lblModDept.AutoSize = true;
            this.lblModDept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.lblModDept.Location = new System.Drawing.Point(461, 23);
            this.lblModDept.Name = "lblModDept";
            this.lblModDept.Size = new System.Drawing.Size(67, 13);
            this.lblModDept.TabIndex = 10;
            this.lblModDept.Text = "Departments";
            this.lblModDept.Visible = false;
            // 
            // chkModify
            // 
            this.chkModify.AutoSize = true;
            this.chkModify.Location = new System.Drawing.Point(47, 41);
            this.chkModify.Name = "chkModify";
            this.chkModify.Size = new System.Drawing.Size(154, 17);
            this.chkModify.TabIndex = 9;
            this.chkModify.Text = "Modify Existing Department";
            this.chkModify.UseVisualStyleBackColor = true;
            this.chkModify.CheckedChanged += new System.EventHandler(this.chkModify_CheckedChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(235, 41);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(154, 17);
            this.chkDelete.TabIndex = 14;
            this.chkDelete.Text = "Delete Existing Department";
            this.chkDelete.UseVisualStyleBackColor = true;
            this.chkDelete.CheckedChanged += new System.EventHandler(this.chkDelete_CheckedChanged);
            // 
            // AddDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(48)))), ((int)(((byte)(104)))));
            this.ClientSize = new System.Drawing.Size(806, 575);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddDepartment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Departments";
            this.Load += new System.EventHandler(this.AddDepartment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblInvocationDate;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDeptName;
        private System.Windows.Forms.DateTimePicker dtpInvDate;
        private System.Windows.Forms.RichTextBox txtDesc;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbDepartments;
        private System.Windows.Forms.Label lblModDept;
        private System.Windows.Forms.CheckBox chkModify;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.DateTimePicker dtpHidden;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox chkDelete;
    }
}