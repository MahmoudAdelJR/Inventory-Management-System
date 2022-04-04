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
    public partial class product_In_Category : Form
    {
        private BranchesContext context;
        private Form activeForm;

        public product_In_Category()
        {
            InitializeComponent();
            context = new BranchesContext();


            var _category = context.Categories.Select(d => d.Name).ToList();
            comboBox1.DataSource = _category;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _CategoryName = comboBox1.Text;
            var _categoryID = context.Categories.Where(d => d.Name == _CategoryName).Select(s => s.ID).FirstOrDefault();
            var _Products = context.Products.Where(d => d.Category_Id == _categoryID);

            dataGridView1.DataSource = _Products.ToList();
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
        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Reports(), sender);

        }
    }
}
