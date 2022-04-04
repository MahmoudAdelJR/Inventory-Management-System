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
    public partial class Products : Form
    {
        private BranchesContext context;
        public int _ProductID;
        public Suppliers_Bill _SupplierBill;

        public Products()
        {
            InitializeComponent();
            context = new BranchesContext();
            var _categoryName = context.Categories.Select(d => d.Name).ToList();
            comboBox1.DataSource = _categoryName;
            var _BranchName = context.Branches.Select(d => d.Name).ToList();
            comboBox2.DataSource = _BranchName;

            var _SupplierName = context.Suppliers.Select(d => d.Name).ToList();
            comboBox3.DataSource = _SupplierName;
            dataGridView1.DataSource = context.Products.ToList();

            var _productName = context.Products.Select(d => d.Name).ToList();
            comboBox4.DataSource = _productName;
            comboBox4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("You must choose wether it's new or existing product ");
                return;
            }
            
            if (radioButton2.Checked)
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
                {
                    MessageBox.Show("Empty data isn't allowed");
                    return;
                }
                if (textBox3.Text.Length < 1)
                {
                    MessageBox.Show("Enter Quantity");
                    return;
                }
                if (comboBox4.SelectedValue == null)
                {
                    MessageBox.Show("No Branch Selected");
                    return;
                }
                if (comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("No Category Selected");
                    return;
                }
                if (comboBox3.SelectedValue == null)
                {
                    MessageBox.Show("No Supplier Selected");
                    return;
                }

                string ProductName = textBox1.Text;
                int _price = int.Parse(textBox2.Text);
                int _quantity = int.Parse(textBox3.Text);
                string _categoryName = comboBox1.Text;
                string _BranchName = comboBox2.Text;
                var _CategoryID = context.Categories.Where(d => d.Name == _categoryName).Select(s => s.ID).FirstOrDefault();
                var _BranchID = context.Branches.Where(d => d.Name == _BranchName).Select(s => s.ID).FirstOrDefault();

                Product _product = new Product()
                {
                    Name = ProductName,
                    Price = _price,
                    Quantity = _quantity,
                    Category_Id = _CategoryID
                };
                Productes_In_Branch _ProductInBranch = new Productes_In_Branch()
                {
                    Branch_ID = _BranchID,
                    Product_ID = _product.ID,
                    Quantity = _quantity
                };

                string _supplierName = comboBox3.Text;
                var _supplierID = context.Suppliers.Where(d => d.Name == _supplierName).Select(s => s.ID).FirstOrDefault();

                _SupplierBill = new Suppliers_Bill()
                {
                    Date = DateTime.Now,
                    Supplier_ID = _supplierID
                };

                Suppliers_B_Product _supplierBillProduct = new Suppliers_B_Product()
                {
                    Supplier_Bill_ID = _SupplierBill.ID,
                    Product_ID = _product.ID,
                    Quantity = _quantity
                };

                context.Products.Add(_product);
                context.Productes_In_Branches.Add(_ProductInBranch);
                context.Suppliers_Bills.Add(_SupplierBill);
                context.Suppliers_B_Products.Add(_supplierBillProduct);
                context.SaveChanges();
            }
            if (radioButton1.Checked)
            {
                if (comboBox4.SelectedValue == null)
                {
                    MessageBox.Show("No Product Selected");
                    return;
                }
                if (comboBox4.SelectedValue == null)
                {
                    MessageBox.Show("No Branch Selected");
                    return;
                }
                if (comboBox3.SelectedValue == null)
                {
                    MessageBox.Show("No Supplier Selected");
                    return;
                }
                if (textBox3.Text.Length < 1)
                {
                    MessageBox.Show("Enter Quantity");
                    return;
                }
                string _productName = comboBox4.Text;
                string _BranchName = comboBox2.Text;
                var _ProductID = context.Products.Where(d => d.Name == _productName).Select(s => s.ID).FirstOrDefault();
                var _BranchID = context.Branches.Where(d => d.Name == _BranchName).Select(s => s.ID).FirstOrDefault();

                Product _Product = (from s in context.Products where s.ID == _ProductID select s).FirstOrDefault();
                _Product.Quantity += int.Parse(textBox3.Text);

                var _ProductInBranch = context.Productes_In_Branches.Where(p => p.Branch_ID == _BranchID && p.Product_ID == _ProductID).FirstOrDefault();
                if (_ProductInBranch == null)
                {
                    context.Productes_In_Branches.Add(new Productes_In_Branch()
                    {
                        Product_ID = _Product.ID,
                        Branch_ID = _BranchID,
                        Quantity = int.Parse(textBox3.Text)
                    });
                }
                else
                {
                    _ProductInBranch.Quantity += int.Parse(textBox3.Text);
                }

                string _supplierName = comboBox3.Text;
                var _supplierID = context.Suppliers.Where(d => d.Name == _supplierName).Select(s => s.ID).FirstOrDefault();
                _SupplierBill = new Suppliers_Bill()
                {
                    Date = DateTime.Now,
                    Supplier_ID = _supplierID
                };

                Suppliers_B_Product _supplierBillProduct = new Suppliers_B_Product()
                {
                    Supplier_Bill_ID = _SupplierBill.ID,
                    Product_ID = _Product.ID,
                    Quantity = int.Parse(textBox3.Text)
                };
                context.Suppliers_Bills.Add(_SupplierBill);
                context.Suppliers_B_Products.Add(_supplierBillProduct);

                context.SaveChanges();
            }
            
            dataGridView1.DataSource = context.Products.ToList();
            button1.Enabled = false;
            button3.Enabled = true;
            button2.Enabled = true;
            comboBox3.Enabled = false;
            comboBox2.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox4.Visible = false;
            textBox3.Visible = true;

            textBox1.Visible = true;
            textBox2.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = true;

            comboBox4.Visible = true;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product _Product = (from s in context.Products where s.ID == _ProductID select s).FirstOrDefault();

            _Product.Name = textBox1.Text;
            _Product.Price = int.Parse(textBox2.Text);

            context.SaveChanges();
            dataGridView1.DataSource = context.Products.ToList();
        }

        private void Products_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
                {
                    MessageBox.Show("Empty data isn't allowed");
                    return;
                }
                if (textBox3.Text.Length < 1)
                {
                    MessageBox.Show("Enter Quantity");
                    return;
                }
                string Name = textBox1.Text;
                int _price = int.Parse(textBox2.Text);
                int _quantaty = int.Parse(textBox3.Text);
                string _categoryName = comboBox1.Text;
                string _BranchName = comboBox2.Text;
                var _CategoryID = context.Categories.Where(d => d.Name == _categoryName).Select(s => s.ID).FirstOrDefault();
                var _BranchID = context.Branches.Where(d => d.Name == _BranchName).Select(s => s.ID).FirstOrDefault();

                Product _product = new Product()
                {
                    Name = Name,
                    Price = _price,
                    Quantity = _quantaty,
                    Category_Id = _CategoryID
                };
                Productes_In_Branch _productInBranch = new Productes_In_Branch()
                {
                    Branch_ID = _BranchID,
                    Product_ID = _product.ID,
                    Quantity = _quantaty
                };

                try
                {
                    Suppliers_B_Product _supplierBillProduct = new Suppliers_B_Product()
                    {
                        Supplier_Bill_ID = _SupplierBill.ID,
                        Product_ID = _product.ID,
                        Quantity = _quantaty
                    };

                    context.Products.Add(_product);
                    context.Productes_In_Branches.Add(_productInBranch);

                    context.Suppliers_B_Products.Add(_supplierBillProduct);
                    context.SaveChanges();
                }
                catch { }
            }
            if (radioButton1.Checked)
            {
                if (comboBox4.SelectedValue == null)
                {
                    MessageBox.Show("No Product Selected");
                    return;
                }
                if (textBox3.Text.Length < 1)
                {
                    MessageBox.Show("Enter Quantity");
                    return;
                }
                string name = comboBox4.Text;
                string _BranchName = comboBox2.Text;
                var _ProductID = context.Products.Where(d => d.Name == name).Select(s => s.ID).FirstOrDefault();
                var _BranchID = context.Branches.Where(d => d.Name == _BranchName).Select(s => s.ID).FirstOrDefault();

                Product _Product = (from s in context.Products where s.ID == _ProductID select s).FirstOrDefault();
                _Product.Quantity += int.Parse(textBox3.Text);

                var _productINBranch = context.Productes_In_Branches.Where(p => p.Branch_ID == _BranchID && p.Product_ID == _ProductID).FirstOrDefault();
                if (_productINBranch == null)
                {
                    context.Productes_In_Branches.Add(new Productes_In_Branch()
                    {
                        Product_ID = _Product.ID,
                        Branch_ID = _BranchID,
                        Quantity = int.Parse(textBox3.Text)
                    });
                }
                else
                {
                    _productINBranch.Quantity += int.Parse(textBox3.Text);
                }

                string _supplierName = comboBox3.Text;
                var _supplierID = context.Suppliers.Where(d => d.Name == _supplierName).Select(s => s.ID).FirstOrDefault();
                var _supplierBillProduct = context.Suppliers_B_Products.Where(p => p.Product_ID == _Product.ID && p.Supplier_Bill_ID == _SupplierBill.ID).FirstOrDefault();
                if (_supplierBillProduct == null)
                {
                    context.Suppliers_B_Products.Add(new Suppliers_B_Product()
                    {
                        Supplier_Bill_ID = _SupplierBill.ID,
                        Product_ID = _Product.ID,
                        Quantity = int.Parse(textBox3.Text)
                    });
                }
                else 
                {
                    _supplierBillProduct.Quantity += int.Parse(textBox3.Text);
                }

                context.SaveChanges();
            }
            if (!radioButton1.Checked && !radioButton2.Checked)
                MessageBox.Show("You must Check one radioButton ");
            dataGridView1.DataSource = context.Products.ToList();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button3.Enabled = false;
            button2.Enabled = false;
            comboBox3.Enabled = true;
            comboBox2.Enabled = true;
        }
    }
}