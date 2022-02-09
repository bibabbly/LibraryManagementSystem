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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bizit\OneDrive\Documents\studentManagementTable.mdf;Integrated Security=True;Connect Timeout=30");
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                String sql = "insert into ProductTbl values ( " + prodId.Text + " , '" + prodName.Text + "' ," + prodQty.Text + " ," + prodPrice.Text + ",'" + prodCategory.Text + "')"; 
                 SqlCommand cmd = new SqlCommand(sql, conn);
                MessageBox.Show("Product is saved Successfully");
                cmd.ExecuteNonQuery();

                conn.Close();
                populate();
                clearField();

            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            
            }

        }

        private void clearField() {

            prodId.Text = "";
            prodName.Text = "";
            prodQty.Text = "";
            prodPrice.Text = "";
            prodCategory.SelectedIndex = -1;
        
        }

        private void populate() {

            try
            {
                conn.Open();
                String sql = "select * from productTbl";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                var ds = new DataSet();
                adapter.Fill(ds);
                prodDvd.DataSource = ds.Tables[0];

                conn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }
        private void fillCombo() {
           
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select catName from categoryTbl ", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName",typeof(string));
                dt.Load(dr);

                prodCategory.ValueMember = "catName";
                prodCategory.DataSource = dt;               

                conn.Close();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillCombo();
            populate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void Seller_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }

        private void prodDvd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            prodId.Text = prodDvd.SelectedRows[0].Cells[0].Value.ToString();
            prodName.Text = prodDvd.SelectedRows[0].Cells[1].Value.ToString();
             prodQty.Text = prodDvd.SelectedRows[0].Cells[2].Value.ToString();
            prodPrice.Text = prodDvd.SelectedRows[0].Cells[3].Value.ToString();
             prodCategory.Text = prodDvd.SelectedRows[0].Cells[4].Value.ToString();

          //  catId.Text = catDvd.SelectedRows[0].Cells[0].Value.ToString();
           // catName.Text = catDvd.SelectedRows[0].Cells[1].Value.ToString();
           // catDesc.Text = catDvd.SelectedRows[0].Cells[2].Value.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                if (prodId.Text == "")
                {
                    MessageBox.Show("Please select Id to Delete");
                }
                else
                {
                    conn.Open();
                    String sql = "delete from ProductTbl where prodId = " + prodId.Text;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The Product with Id =" + prodId.Text + " Has been deleted successfully");
                    conn.Close();

                    populate();
                    clearField();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                if (prodId.Text == "" || prodName.Text == "" || prodQty.Text == "" || prodPrice.Text == "")
                {
                    MessageBox.Show("Missing information ");
                }
                else
                {

                    conn.Open();
                    String sql = "update productTbl set prodName = '" + prodName.Text + "' , prodQty =" + prodQty.Text + " , prodPrice = "+prodPrice.Text+" , prodCat = '"+prodCategory.Text+"' where ProdId =" + prodId.Text;
                    SqlCommand sqlCommand = new SqlCommand(sql, conn);
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Record successfuly updated");
                    conn.Close();

                    populate();
                    clearField();

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
