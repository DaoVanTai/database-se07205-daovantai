using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp1;

namespace SaleManagementWinform
{
    public partial class MainForm : Form
    {
        public static string connectionString
       = "Server=WHALE;Database=SALE_MANGEMENT;Trusted_Connection=True;";
        public MainForm()

        {
            InitializeComponent();

            // Tối đa hóa biểu mẫu và giữ nó ở trên Thanh tác vụ
            this.WindowState = FormWindowState.Maximized;

            // Đặt kiểu đường viền biểu mẫu để đảm bảo nó có giao diện cửa sổ chuẩn
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Tùy chọn, hãy đặt StartPosition thành CenterScreen nếu bạn muốn tải ở giữa
            this.StartPosition = FormStartPosition.CenterScreen;
            dgv_product.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.dgv_product.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);

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

        private void button_1_Click(object sender, EventArgs e)
        {
            AddProductForm form = new AddProductForm();
            form.ShowDialog();
        }
        private string selectedProductCode;
        private string selectedProductName;
        private int selectedProductPrice;
        private int selectedProductQuantity;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra chỉ số hàng
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
                    selectedProductCode = selectedRow.Cells["Code"].Value.ToString();
                    selectedProductName = selectedRow.Cells["Name"].Value.ToString();
                    selectedProductPrice = int.Parse(selectedRow.Cells["Price"].Value.ToString());
                    selectedProductQuantity = int.Parse(selectedRow.Cells["Quantity"].Value.ToString());

                    UpdateProduct updateProduct = new UpdateProduct(code, name, price, quantity);
                    updateProduct.ShowDialog();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedProductCode)) // Kiểm tra xem có sản phẩm nào được chọn không
            {
                UpdateProduct updateProduct = new UpdateProduct(selectedProductCode, selectedProductName, selectedProductPrice, selectedProductQuantity);
                updateProduct.ShowDialog();
                LoadData(); // Làm mới dữ liệu sau khi cập nhật
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để cập nhật.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData(); // Gọi lại phương thức LoadData để cập nhật dữ liệu trong DataGridView
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MenuForm menu = new MenuForm();
            menu.Show();
            this.Hide();
        }



        private void button3_Click_1(object sender, EventArgs e)
        {
            SearchProduct searchForm = new SearchProduct();
            searchForm.Show();
        }
    }
}

