using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_vs_
{
    public partial class Main : Form
    {
       
        private Form activeForm;
        public Main()
        {
            InitializeComponent();
            using (BranchesContext ctx = new BranchesContext())
            {
                var x = ctx.Branches.FirstOrDefault();
            }
        }
     
     
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel5.Controls.Add(childForm);
            this.panel5.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            label3.Text = childForm.Text;
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Categories(), sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Customers(), sender);
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Branches(), sender);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Products(), sender);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Welcome(), sender);

        }

        private void Main_Load(object sender, EventArgs e)
        {
   
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Suppliers(), sender);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Bills(), sender);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Reports(), sender);

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.BurlyWood;
            button1.ForeColor = Color.Black;
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            button2.BackColor = Color.BurlyWood;
            button2.ForeColor = Color.Black;
        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            button3.BackColor = Color.BurlyWood;
            button3.ForeColor = Color.Black;
        }

        private void button7_MouseClick(object sender, MouseEventArgs e)
        {
            button7.BackColor = Color.BurlyWood;
            button7.ForeColor = Color.Black;
        }

        private void button8_MouseClick(object sender, MouseEventArgs e)
        {
            button8.BackColor = Color.BurlyWood;
            button8.ForeColor = Color.Black;
        }

        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            button4.BackColor = Color.BurlyWood;
            button4.ForeColor = Color.Black;
        }

        private void button6_MouseClick(object sender, MouseEventArgs e)
        {
            button6.BackColor = Color.BurlyWood;
            button6.ForeColor = Color.Black;
        }
    } 
 }

