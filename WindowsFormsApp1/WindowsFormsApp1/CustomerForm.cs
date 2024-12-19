using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CustomerForm : Form
    {
        public static string connectionString
 = "Server=LAPTOP-9Q3UMQ9Q\\SQLEXPRESS;Database=Asm;Trusted_Connection=True;";
        public CustomerForm()
        {
            InitializeComponent();
            // Maximize the form and keep it above the Taskbar
            this.WindowState = FormWindowState.Maximized;

            // Set the form border style to ensure it has a standard window
            this.FormBorderStyle = FormBorderStyle.Sizable;

            this.StartPosition = FormStartPosition.CenterScreen;
            dgv_customer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.LoadData();
        }

        private void LoadData()
        {
            // SQL query to fetch data
            string query = "SELECT * FROM Customer";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the database connection
                    connection.Open();

                    // Create a SqlDataAdapter to execute the query and fill the DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with query results
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dgv_customer.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCustomer form = new AddCustomer();
            form.ShowDialog();
            this.Hide();
        }
    }
}
