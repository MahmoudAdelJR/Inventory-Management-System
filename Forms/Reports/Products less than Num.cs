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
    public partial class Products_less_than_Num : Form
    {

        private Form activeForm;

        private BranchesContext context;
        public Products_less_than_Num()
        {
            InitializeComponent();

            context = new BranchesContext();
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(childForm);
            this.panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("No Quantity Entered !");
                return;
            }
            int num = int.Parse(textBox1.Text);
            var _productsInCategory = context.Products.Where(d => d.Quantity <= num);

            dataGridView1.DataSource = _productsInCategory.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Reports(), sender);

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
        }
    }
}
