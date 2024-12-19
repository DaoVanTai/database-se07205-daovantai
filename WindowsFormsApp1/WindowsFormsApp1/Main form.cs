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
    public partial class Main_form : Form
    {
        public static string connectionString
       = "Server=LAPTOP-9Q3UMQ9Q\\SQLEXPRESS;Database=Asm;Trusted_Connection=True;";
        private object buttonRefresh;

        public Main_form()

        {
            InitializeComponent();

            // Tối đa hóa biểu mẫu và giữ nó ở trên Thanh tác vụ
            this.WindowState = FormWindowState.Maximized;

            // Đặt kiểu đường viền biểu mẫu để đảm bảo nó có giao diện cửa sổ chuẩn
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Tùy chọn, hãy đặt StartPosition thành CenterScreen nếu bạn muốn tải ở giữa
            this.StartPosition = FormStartPosition.CenterScreen;
            dgv_product.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            this.dgv_product.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            

            this.LoadData();
        }

       

        private void LoadData()
        {
            // SQL query to fetch data
            string query = "SELECT * FROM Product";

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
                    dgv_product.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private void SetupDataGridView()
        {
            // Add Edit button column
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.Name = "Edit";
            editButtonColumn.HeaderText = "Edit";
            editButtonColumn.Text = "Edit";
            editButtonColumn.UseColumnTextForButtonValue = true;
            dgv_product.Columns.Add(editButtonColumn);

            // Add Delete button column
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dgv_product.Columns.Add(deleteButtonColumn);

            // Optional: Adjust column widths
            dgv_product.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Handle button clicks
            dgv_product.CellClick += dataGridView1_CellClick;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked row is valid
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgv_product.Rows[e.RowIndex];

                // Retrieve data from each cell in the selected row
                var code = selectedRow.Cells["Code"].Value.ToString();
                var name = selectedRow.Cells["Name"].Value.ToString();
                var price = int.Parse(selectedRow.Cells["Price"].Value.ToString());
                var quantity = int.Parse(selectedRow.Cells["Quantity"].Value.ToString());

                // Display data in textboxes or labels, or use it as needed
                /*  txtID.Text = id.ToString();
                  txtName.Text = name;
                  txtAge.Text = age.ToString();*/

                // MessageBox.Show($"Code  : {code}, Name: {name}, Price: {price},  Quantity: {quantity}");


                UpdateProduct updateProduct = new UpdateProduct(code, name, price, quantity);
                updateProduct.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}





            