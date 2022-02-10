using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bizit\OneDrive\Documents\studentManagementTable.mdf;Integrated Security=True;Connect Timeout=30");


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
                        try { 
                        conn.Open();
                            String sql = "Select count (*) from sellerTbl where sellerName='" + uname.Text + "' and sellerPassword ='" + password.Text + "'";
                            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                            DataTable dt=new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows[0][0].ToString() == "1")
                            {
                                SellingForm sell = new SellingForm();
                               SellingForm.usernameee= uname.Text;
                                sell.Show();
                                this.Hide();
                                conn.Close();
                            }
                            else {
                                MessageBox.Show("Wrong Username or Password");
                            }
                            conn.Close();

                        }
                        catch(Exception ex) {
                            MessageBox.Show("Login failed due to "+ex.Message);

                        }
                    }


                }
                
                else {
                    MessageBox.Show("Select a Role ");
                }
                
                }
        }
    }
}
