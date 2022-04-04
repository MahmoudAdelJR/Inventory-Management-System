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
    public partial class Suppliers : Form
    {
        public int Supplier_id;
        BranchesContext context;
        public Suppliers()
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

        private void button1_Click(object sender, EventArgs e)
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
                context.Suppliers.Add(new Supplier()
                {
                    Name = textBox1.Text,
                    Address = textBox2.Text,
                    Phone = textBox3.Text
                });
                context.SaveChanges();
                dataGridView1.DataSource = context.Suppliers.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                Supplier _supplier = (from s in context.Suppliers where s.ID == id select s).FirstOrDefault();
                context.Suppliers.Remove(_supplier);
                context.SaveChanges();
                dataGridView1.DataSource = context.Suppliers.ToList();
                MessageBox.Show("Deleted");
        }

        private void button2_Click(object sender, EventArgs e)
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
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                Supplier _supplier = (from s in context.Suppliers where s.ID == id select s).FirstOrDefault();
                _supplier.Name = textBox1.Text;
                _supplier.Address = textBox2.Text;
                _supplier.Phone = textBox3.Text;
                context.SaveChanges();
                dataGridView1.DataSource = context.Suppliers.ToList();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                Supplier_id = int.Parse(row.Cells[0].Value.ToString());

            }
        }

        
    }
}
