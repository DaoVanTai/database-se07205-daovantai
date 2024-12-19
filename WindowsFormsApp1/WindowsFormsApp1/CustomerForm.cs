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
using WindowsFormsApp1;

namespace SaleManagementWinform
{
    public partial class CustomerForm : Form
    {
        public static string connectionString
    = "Server=WHALE;Database=SALE_MANGEMENT;Trusted_Connection=True;";
        public CustomerForm()

        {
            InitializeComponent();

            // Tối đa hóa biểu mẫu và giữ nó ở trên Thanh tác vụ
            this.WindowState = FormWindowState.Maximized;

            // Đặt kiểu đường viền biểu mẫu để đảm bảo nó có giao diện cửa sổ chuẩn
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Tùy chọn, hãy đặt StartPosition thành CenterScreen nếu bạn muốn tải ở giữa
            this.StartPosition = FormStartPosition.CenterScreen;
            dgv_Customer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Customer.CellClick += dataGridView1_CellClick;
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
                    dgv_Customer.DataSource = dataTable;
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
            AddCustomer form = new AddCustomer();
            form.ShowDialog();
            this.Hide();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadData(); // Gọi lại phương thức LoadData để cập nhật dữ liệu trong DataGridView
        }
        private string selectedCustomerCode;
        private string selectedCustomerName;
        private string selectedCustomerPhoneNumber;
        private string selectedCustomerAddress;
        private string selectedCustomerEmail;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu hàng được nhấp là hợp lệ
            if (e.RowIndex >= 0)
            {
                // Lấy hàng đã chọn
                DataGridViewRow selectedRow = dgv_Customer.Rows[e.RowIndex];

                // Lấy dữ liệu từ các ô trong hàng đã chọn
                var code = selectedRow.Cells["Code"].Value.ToString();
                var name = selectedRow.Cells["Name"].Value.ToString();
                var phoneNumber = selectedRow.Cells["PhoneNumber"].Value.ToString();
                var address = selectedRow.Cells["Address"].Value.ToString();
                var email = selectedRow.Cells["Email"].Value.ToString();

                // Mở form cập nhật với thông tin khách hàng
                UpdateCustomer updateCustomer = new UpdateCustomer(code, name, phoneNumber, address, email);
                updateCustomer.ShowDialog();

                // Tải lại dữ liệu sau khi cập nhật
                LoadData();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv_Customer.SelectedRows.Count > 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dgv_Customer.SelectedRows[0];

                // Lấy thông tin từ hàng đã chọn
                var code = selectedRow.Cells["Code"].Value?.ToString();
                var name = selectedRow.Cells["Name"].Value?.ToString();
                var phoneNumber = selectedRow.Cells["PhoneNumber"].Value?.ToString();
                var address = selectedRow.Cells["Address"].Value?.ToString();
                var email = selectedRow.Cells["Email"].Value?.ToString();

                // Khởi tạo UpdateCustomer với các tham số đã chọn
                UpdateCustomer updateCustomer = new UpdateCustomer(code, name, phoneNumber, address, email);
                updateCustomer.ShowDialog();

                // Tải lại dữ liệu sau khi cập nhật
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để cập nhật.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomer searchForm = new SearchCustomer();
            searchForm.Show();
        }
    }
}