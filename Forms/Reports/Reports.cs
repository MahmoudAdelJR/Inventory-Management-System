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
    public partial class Reports : Form
    {
        public BranchesContext context;

        //ha
        private Form activeForm;

        public Reports()
        {
            InitializeComponent();
            context = new BranchesContext();
        }

        //ha
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
            label3.Text = childForm.Text;
        }

      
        private void button3_Click(object sender, EventArgs e)
        {
            var _product = context.Products.ToList();
            dataGridView1.DataSource = _product.ToList();
        }
     
     
        private void button7_Click(object sender, EventArgs e)
        {
            var _customerBill = (from CBP in context.Customer_B_Products
                          join CB in context.Customers_Bills
                          on CBP.Costomer_Bill_ID equals CB.ID
                          orderby CB.ID

                          select new
                          {
                              CB.ID,
                              CB.Date,
                              CBP.Quantity,
                              CBP.Products.Name,
                          }).ToList();

            dataGridView1.DataSource = _customerBill;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var _supplierBill = (from SBP in context.Suppliers_B_Products
                          join SB in context.Suppliers_Bills
                          on SBP.Supplier_Bill_ID equals SB.ID
                          orderby SB.ID

                          select new
                          {
                              SB.ID,
                              SB.Date,
                              SB.Supplier_ID,
                              SBP.Quantity,
                              SBP.Products.Name,
                          }).ToList();

            dataGridView1.DataSource = _supplierBill;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var _customer = context.Customers.ToList();
            dataGridView1.DataSource = _customer.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var _supplier = context.Suppliers.ToList();
            dataGridView1.DataSource = _supplier.ToList();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var _Branch = context.Branches.ToList();
            dataGridView1.DataSource = _Branch.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var _category = context.Categories.ToList();
            dataGridView1.DataSource = _category.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
         
            OpenChildForm(new product_In_Category(), sender);

            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            OpenChildForm(new Product_In_Branch(), sender);

        }

        private void button10_Click(object sender, EventArgs e)
        {
           
            OpenChildForm(new Products_less_than_Num(), sender);

        }
    }
}