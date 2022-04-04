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
    
    public partial class Customers : Form
    {
        BranchesContext context;
       
        public int Cust_id;
        public Customers()
        {
            InitializeComponent();
            context = new BranchesContext();
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
            if (textBox3.Text.Length == 11)
            {
                if (char.IsControl(e.KeyChar)) { }
                else
                {
                    e.Handled = true;
                }
            }
        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("You shouldn't Enter empty data");
                return;
            }
            if (textBox3.Text.Length < 11)
            {
                MessageBox.Show("Phone number must be 11 digits length ");
                return;
            }
            string _Name = textBox1.Text;
            string _Address = textBox2.Text;
            string _phone = textBox3.Text;
            Customer _customer = new Customer()
            {
                Name = _Name,
                Address = _Address,
                Phone = _phone

            };
            context.Customers.Add(_customer);
            context.SaveChanges();
            dataGridView1.DataSource = context.Customers.ToList();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("You shouldn't Enter empty data");
                return;
            }
            if (textBox3.Text.Length < 11)
            {
                MessageBox.Show("Phone number must be 11 digits length ");
                return;
            }
            Customer _customer = (from s in context.Customers where s.ID == Cust_id select s).FirstOrDefault();

            _customer.Name = textBox1.Text;
            _customer.Address = textBox2.Text;
            _customer.Phone = textBox3.Text;

            context.SaveChanges();

            dataGridView1.DataSource = context.Customers.ToList();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var _customer = context.Customers.Where(d => d.ID == Cust_id).FirstOrDefault();
            context.Customers.Remove(_customer);
            context.SaveChanges();

            dataGridView1.DataSource = context.Customers.ToList();
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                Cust_id = int.Parse(row.Cells[0].Value.ToString());

            }
        }
    }
}
