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
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bizit\OneDrive\Documents\studentManagementTable.mdf;Integrated Security=True;Connect Timeout=30");
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                String query = "insert into CategoryTbl values ( "+catId.Text+" , '"+catName.Text+"' ,'"+catDesc.Text+"')";
                SqlCommand cmd = new SqlCommand(query, conn);   
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully...");
                conn.Close();
                populate();
                clearFields();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void populate()
        {
            try
            {
                conn.Open();
                String sql = "select * from CategoryTbl";
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
                SqlCommandBuilder builder= new SqlCommandBuilder(adapter);

                var ds=new DataSet();
                adapter.Fill(ds);
                catDvd.DataSource = ds.Tables[0];
                conn.Close();

            }
            catch (Exception ex) {
            
                MessageBox.Show(ex.Message);
            
            }
        }

        private void clearFields() {

            catId.Text = "";
            catName.Text = "";
            catDesc.Text = "";

        }
        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void catDvd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           catId.Text = catDvd.SelectedRows[0].Cells[0].Value.ToString();
           catName.Text= catDvd.SelectedRows[0].Cells[1].Value.ToString();
           catDesc.Text = catDvd.SelectedRows[0].Cells[2].Value.ToString();

        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (catId.Text == "") {
                    MessageBox.Show("Please select Id to Delete");
                }
                else
                {
                    conn.Open();
                    String sql = "delete from CategoryTbl where catId = " + catId.Text;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The Category with Id ="+catId.Text+" Has been deleted successfully");
                    conn.Close();

                    populate();
                    clearFields();
                }

            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try {

                if (catId.Text == "" || catName.Text == "" || catDesc.Text == "")
                {
                    MessageBox.Show("Missing information ");
                }
                else { 
                
                    conn.Open();
                    String sql = "update CategoryTbl set catName= '"+catName.Text+"' , catDesc ='"+catDesc.Text+"' where catId ="+catId.Text;
                    SqlCommand sqlCommand = new SqlCommand(sql, conn);
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Record successfuly updated");
                    conn.Close();

                    populate();
                    clearFields();

                }
            
            }
            catch(Exception ex){ 
            
            MessageBox.Show(ex.Message);
            }   
        }

        private void Seller_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SellerForm seller=new SellerForm();
            seller.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellingForm sell = new SellingForm();
            sell.Show();
            this.Hide();
        }
    }
}
