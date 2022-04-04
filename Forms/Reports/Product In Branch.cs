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
    public partial class Product_In_Branch : Form
    {
        private BranchesContext context;
        private Form activeForm;

        public Product_In_Branch()
        {
            InitializeComponent();
            context = new BranchesContext();

            var _Branch = context.Branches.Select(d => d.Name).ToList();
            comboBox1.DataSource = _Branch;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _BranchName = comboBox1.Text;
            var q = context.Branches.Where(d => d.Name == _BranchName).Select(s => s.ID).FirstOrDefault();
            var _ProductInBranch = from br_pro in context.Productes_In_Branches
                          join br in context.Branches on br_pro.Branch_ID equals br.ID
                          join pro in context.Products on br_pro.Product_ID equals pro.ID

                          where (br_pro.Branch_ID == q && br.ID == q)
                          select new
                          {
                              pro.ID,
                              pro.Name,
                              pro.Price,
                              pro.Quantity,
                              pro.Category_Id
                          };

            dataGridView1.DataSource = _ProductInBranch.ToList();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
