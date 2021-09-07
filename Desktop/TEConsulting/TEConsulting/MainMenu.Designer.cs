
namespace TEConsulting
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.pOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyPOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processPOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchEmployeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEmployeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDepartmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(48)))), ((int)(((byte)(104)))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 721);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1209, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pOToolStripMenuItem,
            this.hRToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1209, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // pOToolStripMenuItem
            // 
            this.pOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPOToolStripMenuItem,
            this.modifyPOToolStripMenuItem,
            this.processPOToolStripMenuItem});
            this.pOToolStripMenuItem.Name = "pOToolStripMenuItem";
            this.pOToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.pOToolStripMenuItem.Text = "PO";
            // 
            // addPOToolStripMenuItem
            // 
            this.addPOToolStripMenuItem.Name = "addPOToolStripMenuItem";
            this.addPOToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.addPOToolStripMenuItem.Tag = "AddPO";
            this.addPOToolStripMenuItem.Text = "Add PO";
            this.addPOToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // modifyPOToolStripMenuItem
            // 
            this.modifyPOToolStripMenuItem.Name = "modifyPOToolStripMenuItem";
            this.modifyPOToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.modifyPOToolStripMenuItem.Tag = "ModifyPO";
            this.modifyPOToolStripMenuItem.Text = "Modify PO";
            this.modifyPOToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // processPOToolStripMenuItem
            // 
            this.processPOToolStripMenuItem.Name = "processPOToolStripMenuItem";
            this.processPOToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.processPOToolStripMenuItem.Tag = "ProcessPO";
            this.processPOToolStripMenuItem.Text = "Process PO";
            this.processPOToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // hRToolStripMenuItem
            // 
            this.hRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEmployeeToolStripMenuItem,
            this.searchEmployeeToolStripMenuItem,
            this.addDepartmentToolStripMenuItem});
            this.hRToolStripMenuItem.Name = "hRToolStripMenuItem";
            this.hRToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.hRToolStripMenuItem.Text = "HR";
            // 
            // searchEmployeeToolStripMenuItem
            // 
            this.searchEmployeeToolStripMenuItem.Name = "searchEmployeeToolStripMenuItem";
            this.searchEmployeeToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.searchEmployeeToolStripMenuItem.Tag = "SearchEmp";
            this.searchEmployeeToolStripMenuItem.Text = "Manage Employees";
            this.searchEmployeeToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // addEmployeeToolStripMenuItem
            // 
            this.addEmployeeToolStripMenuItem.Name = "addEmployeeToolStripMenuItem";
            this.addEmployeeToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addEmployeeToolStripMenuItem.Tag = "AddEmp";
            this.addEmployeeToolStripMenuItem.Text = "Add Employee";
            this.addEmployeeToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // addDepartmentToolStripMenuItem
            // 
            this.addDepartmentToolStripMenuItem.Name = "addDepartmentToolStripMenuItem";
            this.addDepartmentToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addDepartmentToolStripMenuItem.Tag = "AddDept";
            this.addDepartmentToolStripMenuItem.Text = "Manage Departments";
            this.addDepartmentToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(48)))), ((int)(((byte)(104)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1209, 743);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(186)))), ((int)(((byte)(46)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem pOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyPOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchEmployeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEmployeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDepartmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processPOToolStripMenuItem;
    }
}



