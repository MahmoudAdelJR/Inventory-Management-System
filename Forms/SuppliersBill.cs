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
    public partial class SuppliersBill : Form
    {
        BranchesContext context;
        public SuppliersBill()
        {
            InitializeComponent();
            context = new BranchesContext();
        }

        private void SuppliersBill_Load(object sender, EventArgs e)
        {
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";
                comboBox1.DataSource = context.Categories.ToList();
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "ID";
                int _categoryID = Int32.Parse(comboBox1.SelectedValue.ToString());
                comboBox2.DataSource = context.Products.Where(p => p.Category_Id == _categoryID).ToList();
                comboBox3.DisplayMember = "Name";
                comboBox3.ValueMember = "ID";
                comboBox3.DataSource = context.Suppliers.ToList();
                comboBox4.DisplayMember = "Name";
                comboBox4.ValueMember = "ID";
                comboBox4.DataSource = context.Branches.ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                comboBox2.DataSource = null;
                int _categoryID = Int32.Parse(comboBox1.SelectedValue.ToString());
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "ID";
                comboBox2.DataSource = context.Products.Where(p => p.Category_Id == _categoryID).ToList();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (comboBox4.SelectedValue == null)
                {
                    MessageBox.Show("No Branch selected");
                    return;
                }
                if (comboBox3.SelectedValue == null)
                {
                    MessageBox.Show("No Supplier selected");
                    return;
                }
                context.Suppliers_Bills.Add(new Suppliers_Bill()
                {
                    Supplier_ID = int.Parse(comboBox3.SelectedValue.ToString()),
                    Date = DateTime.Now
                });
                context.SaveChanges();
                Button btn = (Button)sender;
                btn.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                comboBox4.Enabled = false;
                comboBox3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
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
                var bill_ID = context.Suppliers_Bills.Max(d => d.ID);

                int _BranchID = int.Parse(comboBox4.SelectedValue.ToString());
                int _Product_ID = int.Parse(comboBox2.SelectedValue.ToString());
                var _supplierBillProduct = context.Suppliers_B_Products.Where(p => p.Supplier_Bill_ID == bill_ID && p.Product_ID == _Product_ID).FirstOrDefault();
                if (_supplierBillProduct == null)
                {
                    context.Suppliers_B_Products.Add(new Suppliers_B_Product()
                    {
                        Supplier_Bill_ID = bill_ID,
                        Product_ID = int.Parse(comboBox2.SelectedValue.ToString()),
                        Quantity = int.Parse(textBox1.Text)
                    });
                }
                else
                {

                    _supplierBillProduct.Quantity += int.Parse(textBox1.Text);
                }

                var _productInBranch = context.Productes_In_Branches.Where(p => p.Branch_ID == _BranchID && p.Product_ID == _Product_ID).FirstOrDefault();
                if (_productInBranch == null)
                {
                    context.Productes_In_Branches.Add(new Productes_In_Branch()
                    {
                        Product_ID = _Product_ID,
                        Branch_ID = _BranchID,
                        Quantity = int.Parse(textBox1.Text)
                    });
                }
                else
                {
                    _productInBranch.Quantity += int.Parse(textBox1.Text);
                }
                var product = context.Products.Where(p => p.ID == _Product_ID).FirstOrDefault();
                product.Quantity+= int.Parse(textBox1.Text);
                context.SaveChanges();
                dataGridView1.DataSource = null;
               

                var _supplierBillProducts = (from billProduct in context.Suppliers_B_Products
                          where billProduct.Supplier_Bill_ID == bill_ID select
                          new
                          {
                              product_name = billProduct.Products.Name,
                              price = billProduct.Products.Price,
                              quantity = billProduct.Quantity,
                          }).ToList();


               
                dataGridView1.DataSource = _supplierBillProducts;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            comboBox4.Enabled = true;
            comboBox3.Enabled = true;
            dataGridView1.DataSource = null;
        }
        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
