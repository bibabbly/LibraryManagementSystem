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
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bizit\OneDrive\Documents\studentManagementTable.mdf;Integrated Security=True;Connect Timeout=30");

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                String sql = "insert into SellerTbl values ( " + sId.Text + " , '" + sName.Text + "' ,'" + sEmail.Text + "' ,'" + sTel.Text + "','" + sPass.Text + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                MessageBox.Show("Seller is saved Successfully");
                cmd.ExecuteNonQuery();

                conn.Close();
                populate();
                clearField();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void populate() {

            try
            {
                conn.Open();
                String sql = "select * from SellerTbl";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                var ds = new DataSet();
                adapter.Fill(ds);
                sellerDVD.DataSource = ds.Tables[0];

                conn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }

        private void clearField() {
            sId.Text = "";
            sName.Text = "";
            sEmail.Text = "";
            sTel.Text = "";
            sPass.Text = "";
        
        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (sId.Text == "")
                {
                    MessageBox.Show("Please select Id to Delete");
                }
                else
                {
                    conn.Open();
                    String sql = "delete from sellerTbl where SellerId = " + sId.Text;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The Seller with Id =" + sId.Text + " Has been deleted successfully");
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

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {

                if (sId.Text == "" || sName.Text == "" || sEmail.Text == "" || sTel.Text == "" || sPass.Text == "")
                {
                    MessageBox.Show("Missing information ");
                }
                else
                {

                    conn.Open();
                    String sql = "update sellerTbl set sellerName = '" + sName.Text + "' , sellerEmail = '" + sEmail.Text + "' , sellerPhone = '" + sTel.Text + "' , sellerPassword = '" + sPass.Text + "' where sellerId =" + sId.Text;
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

        private void sellerDVD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            sId.Text = sellerDVD.SelectedRows[0].Cells[0].Value.ToString();
            sName.Text = sellerDVD.SelectedRows[0].Cells[1].Value.ToString();
            sEmail.Text = sellerDVD.SelectedRows[0].Cells[2].Value.ToString();
            sTel.Text = sellerDVD.SelectedRows[0].Cells[3].Value.ToString();
            sPass.Text = sellerDVD.SelectedRows[0].Cells[4].Value.ToString();

        }
    }
}
