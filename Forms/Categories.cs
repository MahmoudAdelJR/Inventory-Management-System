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
    public partial class Categories : Form
    {
        BranchesContext context;
        public int Category_id;
        public Categories()
        {
            InitializeComponent();
            context = new BranchesContext();
            dataGridView1.DataSource = context.Categories.ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var categories = context.Categories;
            categories.Add(new Category() { Name = textBox1.Text });
            context.SaveChanges();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("You shouldn't Enter empty data");
                return;
            }
            string Name = textBox1.Text;
            Category _category = new Category()
            {
                Name = Name
            };
            context.Categories.Add(_category);
            context.SaveChanges();

            dataGridView1.DataSource = context.Categories.ToList();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[1].Value.ToString();

                Category_id = int.Parse(row.Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" )
            {
                MessageBox.Show("You shouldn't Enter empty data");
                return;
            }
            Category _category = (from s in context.Categories where s.ID == Category_id select s).FirstOrDefault();

            _category.Name = textBox1.Text; ;
            context.SaveChanges();
            dataGridView1.DataSource = context.Categories.ToList();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var cus = context.Categories.Where(d => d.ID == Category_id).FirstOrDefault();
            context.Categories.Remove(cus);
            context.SaveChanges();

            dataGridView1.DataSource = context.Categories.ToList();
        }
    }
}
