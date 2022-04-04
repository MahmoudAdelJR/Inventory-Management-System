using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_vs_
{

    public partial class Bills : Form
    {
        private Form activeForm;

        public Bills()
        {
            InitializeComponent();
        }


        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(childForm);
            this.panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            label3.Text = childForm.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SuppliersBill(), sender);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CustomersBill(), sender);

        }
    }
}
