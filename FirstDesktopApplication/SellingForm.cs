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
    public partial class SellingForm : Form
    {


        public static String usernameee;
        public SellingForm()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bizit\OneDrive\Documents\studentManagementTable.mdf;Integrated Security=True;Connect Timeout=30");

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void prodPrice_TextChanged(object sender, EventArgs e)
        {

        }


        private void fillCombo()
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select catName from categoryTbl ", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(dr);

            prodCategory.ValueMember = "catName";
            prodCategory.DataSource = dt;



            conn.Close();
        }
        private void populate()
        {

            try
            {
                conn.Open();
                String sql = "select  ProdName, prodQty, prodPrice from productTbl";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                var ds = new DataSet();
                adapter.Fill(ds);
                prodDvDD.DataSource = ds.Tables[0];

                conn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }

        private void clearField()
        {
            try
            {
                labelCash.Text = "";
                billid.Text = "";
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
        private void populateBills()
        {

            try
            {
                conn.Open();
                String sql = "select  * from BillTbl";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                var ds = new DataSet();
                adapter.Fill(ds);
                billDVD.DataSource = ds.Tables[0];

                conn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populateBills();
            fillCombo();
            sellerNamee.Text = usernameee;
        }
        int flag = 0;
        private void prodDvDD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            proName.Text = prodDvDD.SelectedRows[0].Cells[0].Value.ToString();
            prodPrice.Text = prodDvDD.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            dateLbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        int Grdtotal = 0;
        int n = 0;
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (proName.Text == "" || proQty.Text == "" || prodPrice.Text=="") {
                MessageBox.Show("Missing Information to add in Cart");
            }
            else
            {
               
                int total = Convert.ToInt32(prodPrice.Text) * Convert.ToInt32(proQty.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(orderDVD);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = proName.Text;
                newRow.Cells[2].Value = prodPrice.Text;
                newRow.Cells[3].Value = proQty.Text;
                newRow.Cells[4].Value = Convert.ToInt32(prodPrice.Text) * Convert.ToInt32(proQty.Text);
                orderDVD.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal + total;
                labelCash.Text =Grdtotal.ToString();

            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (billid.Text == "")
                {
                    MessageBox.Show("Missing Bill Id ");
                }
                else {
                    conn.Open();
                    String sql = "insert into BillTbl values ( " + billid.Text + " , '" + sellerNamee.Text + "' ,'" + dateLbl.Text + "' ," + labelCash.Text + ")";
                    //MessageBox.Show(sql);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Bill " + billid.Text + " is saved Successfully");

                    conn.Close();
                    populateBills();
                    clearField();


                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void billDVD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            flag = 1;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("LIBRARY MANAGEMENT SYSTEM", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("ID "+billDVD.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(130,130));
            e.Graphics.DrawString("Seller Name : " + billDVD.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(130, 180));
            e.Graphics.DrawString("Bill Date : " + billDVD.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(130, 230));
            e.Graphics.DrawString("Total  Amount : " + billDVD.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(130, 280));
            e.Graphics.DrawString("Printed By Ishusho Software ", new Font("Century Gothic", 18, FontStyle.Bold), Brushes.DarkRed, new Point(130, 350));
            // e.Graphics.DrawString("LIBRARY MANAGEMENT SYSTEM", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void prodCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            String sql = "Select ProdName, prodQty, prodPrice from productTbl where prodCat ='" + prodCategory.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodDvDD.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login=new Form1();
            login.Show();
        }
    }
}
