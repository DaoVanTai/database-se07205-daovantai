using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SaleManagementWinform
{
    public partial class AddEmployee : Form
    {
        private string connectionString;

        public AddEmployee()
        {
            InitializeComponent();
            connectionString
   = "Server=WHALE;Database=SALE_MANGEMENT;Trusted_Connection=True;";
        }

        public object Connection { get; private set; }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hexadecimal
                }
                return builder.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fullname = tbx_name.Text;
            string code = tbx_code.Text;
            string username = tbx_username.Text;
            string password = tbx_password.Text;
            string position = tbx_position.Text;

            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrWhiteSpace(fullname) || string.IsNullOrWhiteSpace(code) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(position))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Lấy roleID từ giao diện 
            int roleID = GetSelectedRoleId();

            // Thêm nhân viên vào cơ sở dữ liệu
            string hashPassword = HashPassword(password);
            InsertData(code, fullname, position, roleID, username, hashPassword);
        }
        private int GetSelectedRoleId()
        {

            if (rbt_admin.Checked) return 1; // Admin
            if (rbt_sale.Checked) return 2; // Sale
            if (rbt_warehouse.Checked) return 3; // Warehouse
            if (rbt_customer.Checked) return 4; // Customer

            // Giá trị mặc định nếu không có vai trò nào được chọn
            return 0;
        }
        private void InsertData(string code, string name, string position, int roleID, string username, string password)
        {
            // Kết nối đến cơ sở dữ liệu
            string query = "INSERT INTO Employee (code, name, position, username, password, roleID, active) VALUES (@code, @name, @position, @username, @password, @roleId, 1)";

            using (SqlConnection connection = new SqlConnection(connectionString)) // Sử dụng chuỗi kết nối đã được khởi tạo
            {
                try
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo lệnh SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm các tham số vào lệnh
                        command.Parameters.AddWithValue("@code", code);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@position", position);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@roleId", roleID); // Đảm bảo tham số này cũng được thêm

                        // Thực thi lệnh
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}