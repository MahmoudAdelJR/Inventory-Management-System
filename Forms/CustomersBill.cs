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
    public partial class CustomersBill : Form
    {
        public CustomersBill()
        {
            InitializeComponent();
        }

        private void CustomersBill_Load(object sender, EventArgs e)
        {
            using (BranchesContext context = new BranchesContext())
            {
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";
                comboBox1.DataSource = context.Categories.ToList();
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "ID";
                int category = Int32.Parse(comboBox1.SelectedValue.ToString());
                comboBox2.DataSource = context.Products.Where(p => p.Category_Id == category).ToList();
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "ID";
                comboBox3.DataSource = context.Customers.ToList();
                comboBox4.DisplayMember = "Name";
                comboBox4.ValueMember = "ID";
                comboBox4.DataSource = context.Branches.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (BranchesContext context = new BranchesContext())
            {
                if (comboBox2.SelectedValue == null)
                {
                    MessageBox.Show("No Product selected");
                    return;
                }
                if (textBox1.Text.Length < 1)
                {
                    MessageBox.Show("No Quantity Entered");
                    return;
                }
                var bill_ID = context.Customers_Bills.Max(d => d.ID);

                int BID = int.Parse(comboBox4.SelectedValue.ToString());
                int _Product_ID = int.Parse(comboBox2.SelectedValue.ToString());
                

                int quantaty = int.Parse(textBox1.Text);
                var ProductInBranch = context.Productes_In_Branches.Where(p => p.Branch_ID == BID && p.Product_ID == _Product_ID && p.Quantity >= quantaty).FirstOrDefault();
                if (ProductInBranch == null)
                {
                    MessageBox.Show("Either this product isn't found or no sufficient quantity try changing branch on new bill branch");
                    return;
                }
                else
                {
                    var Cust_Bill_Product = context.Customer_B_Products.Where(p => p.Costomer_Bill_ID == bill_ID && p.Product_ID == _Product_ID).FirstOrDefault();
                    if (Cust_Bill_Product == null)
                    {
                        context.Customer_B_Products.Add(new Customer_B_Product()
                        {
                            Costomer_Bill_ID = bill_ID,
                            Product_ID = int.Parse(comboBox2.SelectedValue.ToString()),
                            Quantity = int.Parse(textBox1.Text)
                        });
                    }
                    else
                    {
                        Cust_Bill_Product.Quantity += int.Parse(textBox1.Text);
                    } 
                
                    ProductInBranch.Quantity -= int.Parse(textBox1.Text);
                }
             
                context.SaveChanges();
                dataGridView1.DataSource = null;
              
              
                var _customerBillProducts = (from billProduct in context.Customer_B_Products
                          where billProduct.Costomer_Bill_ID == bill_ID select
                          new
                          {
                              product_name = billProduct.Products.Name,
                              price = billProduct.Products.Price,
                              quantity = billProduct.Quantity,
                          }).ToList();
                dataGridView1.DataSource = _customerBillProducts;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (BranchesContext context = new BranchesContext())
            {
                if (comboBox4.SelectedValue == null)
                {
                    MessageBox.Show("No Branch selected");
                    return;
                }
                if (comboBox3.SelectedValue == null)
                {
                    MessageBox.Show("No Customer selected");
                    return;
                }
                context.Customers_Bills.Add(new Customers_Bill()
                {
                    Customer_ID = int.Parse(comboBox3.SelectedValue.ToString()),
                    Date = DateTime.Now
                });
                context.SaveChanges();
                Button btn = (Button)sender;
                btn.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                // var x = context.Suppliers_Bills.Max(d => d.ID);
                // MessageBox.Show(s.Date.ToString());
            }
        }
        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (BranchesContext context = new BranchesContext())
            {
                comboBox2.DataSource = null;
                int _categoryID = Int32.Parse(comboBox1.SelectedValue.ToString());
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "ID";
                comboBox2.DataSource = context.Products.Where(p => p.Category_Id == _categoryID).ToList();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            dataGridView1.DataSource = null;
        }
    }
}