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
        private void populate()
        {

            try
            {
                conn.Open();
                String sql = "select  ProdName, prodQty from productTbl";
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

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void prodDvDD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            proName.Text = prodDvDD.SelectedRows[0].Cells[0].Value.ToString();
            proQty.Text = prodDvDD.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            dateLbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }
    }
}
