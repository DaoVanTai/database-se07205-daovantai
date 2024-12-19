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
using System.Security.Cryptography;
using System.Text;
using WindowsFormsApp1;

namespace SaleManagementWinform
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            // Đặt biểu mẫu bắt đầu ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            // Vô hiệu hóa nút tối đa hóa/khôi phục
            this.MaximizeBox = false;

            // Tùy chọn: Đặt kiểu đường viền cố định để ngăn chặn việc thay đổi kích thước
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.button1.Click += new System.EventHandler(this.button1_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = txbusername.Text;
            string password = txbpassword.Text;

            string hashPassword = Utils.HashPassword(password);

            // Kiểm tra đăng nhập và lấy RoleId
            (bool checkLogin, int roleId) = CheckLogin(username, hashPassword);

            if (checkLogin)
            {
                MenuForm main = new MenuForm();
                LoadUserRole(main, roleId);
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or password is incorrect !");
            }
        }
        private (bool, int) CheckLogin(string username, string hashedPassword)
        {
            string query = "SELECT password, RoleId FROM Employee WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["password"].ToString(); // Lấy mật khẩu đã mã hóa
                            int roleId = Convert.ToInt32(reader["RoleId"]); // Lấy RoleId

                            if (storedHash == hashedPassword)
                            {
                                return (true, roleId); // Trả về true và RoleId nếu đăng nhập thành công
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

            return (false, 0); // Trả về false và RoleId = 0 nếu không tìm thấy người dùng hoặc có lỗi
        }

        public static class Connection
        {
            public static string SQLConnection = "Server=WHALE;Database=SALE_MANGEMENT;Trusted_Connection=True;";
        }
        private void LoadUserRole(MenuForm main, int roleId)
        {
            switch (roleId)
            {
                case 1: // Admin
                    main.ShowProductButton();
                    ShowAdminFeatures(main);
                    break;
                case 2: // Sale
                    main.ShowProductButton();
                    ShowSaleFeatures(main);
                    break;
                case 3: // Warehouse
                    main.ShowProductButton();
                    ShowWarehouseFeatures(main);
                    break;
                case 4: // Customer
                    ShowCustomerFeatures(main);
                    break;
                default:
                    MessageBox.Show("Role not recognized.");
                    break;
            }
        }
        private void ShowAdminFeatures(MenuForm main)
        {
            // Hiển thị các chức năng dành cho Admin
            main.ShowProductButton();
            main.ShowCustomer();
            main.ShowCustomerOrders();
            main.ShowEmployee();
            main.ShowOrder();
            main.ShowPurchaseHistory();
        }

        private void ShowSaleFeatures(MenuForm main)
        {
            // Hiển thị các chức năng dành cho Sale
            main.ShowEmployee();
            main.ShowProductButton();
            main.ShowOrder();
            main.HideCustomer();
            main.HidePurchaseHistory();
            main.ShowCustomerOrders();
        }

        private void ShowWarehouseFeatures(MenuForm main)
        {
            // Hiển thị các chức năng dành cho Warehouse
            main.ShowProductButton();
            main.ShowOrder();
            main.HideCustomer();
            main.HideCustomerOrders();
            main.HideEmployee();
            main.HidePurchaseHistory();
        }

        private void ShowCustomerFeatures(MenuForm main)
        {
            // Hiển thị các chức năng dành cho Customer
            main.HideCustomer();
            main.HideEmployee();
            main.HideProductButton();
            main.HidePurchaseHistory();
            main.HideCustomerOrders();
            main.ShowOrder();

        }
    }
}