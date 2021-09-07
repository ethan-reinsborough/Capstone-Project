using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEConsulting.Model;
using TEConsulting.Service;

namespace TEConsulting
{
    public partial class MainMenu : Form
    {
        //Don't really need this
        //private int childFormNumber = 0;

        public static Employee Employee { get; set; }
        public MainMenu()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = null;
            ToolStripMenuItem m = (ToolStripMenuItem)sender;

            switch (m.Tag)
            {
                case "AddDept":
                    childForm = new AddDepartment();
                    break;
                case "AddEmp":
                    childForm = new AddEmployee();
                    break;
                case "AddPO":
                    childForm = new AddPO();
                    break;
                case "ModifyPO":
                    childForm = new ModifyPO();
                    break;
                case "ProcessPO":
                    childForm = new ProcessPO();
                    break;
                case "SearchEmp":
                    childForm = new SearchEmployee();
                    break;
            }

            if (childForm != null)
            {
                foreach (Form f in this.MdiChildren)
                {
                    if (f.GetType() == childForm.GetType())
                    {
                        f.Activate();
                        return;
                    }
                }
            }

            childForm.MdiParent = this;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //ADD SPLASH LATER
            //Splash mySplash = new Splash();
            Login login = new Login();
       
            login.ShowDialog();
            if (login.DialogResult == DialogResult.OK)
            {
                this.Show();

                //Sets object on MDI Container
                Employee = login.Employee;
                toolStripStatusLabel.Text = Employee.FirstName + " " + Employee.LastName + ": " + Employee.EmployeeId;

                //Only HR Employees and Management (CEO) can add employees
                if (Employee.DepartmentId != 1 && Employee.DepartmentId != 3)
                {
                    addEmployeeToolStripMenuItem.Visible = false;
                    searchEmployeeToolStripMenuItem.Visible = false;
                }

                if(Employee.SupervisorId != 10000000)
                {
                    processPOToolStripMenuItem.Visible = false;
                }
                
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
            else
            {
                this.Close();
            }
        }
    }
}
