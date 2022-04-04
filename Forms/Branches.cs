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
    public partial class Branches : Form
    {
        public BranchesContext context;
        public int Branch_ID;

        public Branches()
        {
            InitializeComponent();
            context = new BranchesContext();

            dataGridView1.DataSource = context.Branches.ToList();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();

                Branch_ID = int.Parse(row.Cells[0].Value.ToString());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["Address"].Value.ToString();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var deleteBranch = context.Branches.Where(d => d.ID == Branch_ID).FirstOrDefault();
            context.Branches.Remove(deleteBranch);
            context.SaveChanges();
            dataGridView1.DataSource = context.Branches.ToList();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("You shouldn't Enter empty data");
                return;
            }
            Branch Br = (from s in context.Branches where s.ID == Branch_ID select s).FirstOrDefault();

            Br.Name = textBox1.Text;
            Br.Address = textBox2.Text;

            context.SaveChanges();

            dataGridView1.DataSource = context.Branches.ToList();
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();

                Branch_ID = int.Parse(row.Cells[0].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("You shouldn't Enter empty data");
                return;
            }
            string Name = textBox1.Text;
            string Address = textBox2.Text;
            Branch Branch = new Branch()
            {
                Name = Name,
                Address = Address
            };
            context.Branches.Add(Branch);
            context.SaveChanges();
            var getBranch = context.Branches.Select(p => new
            {
                p.ID,
                p.Name,
                p.Address
            });
            dataGridView1.DataSource = getBranch.ToList();
        }
    }
}