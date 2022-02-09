using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstDesktopApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            uname.Text = "";
            password.Text = "";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (uname.Text == "" || password.Text == "") {
                MessageBox.Show("Username or Password can not be Empty");
            }
            else {
                if(roleCb.SelectedIndex > -1)
                {

                    if (roleCb.SelectedItem.ToString() == "Admin")
                    {

                        if (uname.Text == "Admin" && password.Text == "Admin")
                        {
                            ProductForm prod = new ProductForm();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are admin, Please enter the correct credentials");
                        }

                    }
                    else
                    {
                        MessageBox.Show("You are in  Seller Section ");
                    }


                }
                
                else {
                    MessageBox.Show("Select a Role ");
                }
                
                }
        }
    }
}
