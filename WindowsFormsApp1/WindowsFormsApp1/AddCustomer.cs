using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleManagementWinform
{
    public partial class AddCustomer : Form
    {
        public static string connectionString
       = "Server=WHALE;Database=SALE_MANGEMENT;Trusted_Connection=True;";
        private string PhoneKH;
        private object txb_code;

        public AddCustomer()
        {
            NewMethod();
            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;


            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            void NewMethod()
            {
               
            }
        }
        private void InsertData(string code, string name, string phonenumber, string address, string email)
        {
            // Connection string to your database

            // SQL query to insert data
            string query = "INSERT INTO Customer (Code, Name, PhoneNumber, Address, Email) VALUES (@Code, @Name, @PhoneNumber, @Address, @Email)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create the SQL command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);

                        command.Parameters.AddWithValue("@Code", code);
                        command.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Email", email);

                        // Execute the command
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!long.TryParse(PhoneKH, out _))
            {
                MessageBox.Show("Phone number must be numeric.");
                return;
            }
            NewMethod();

            void NewMethod()
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Lấy mã sản phẩm từ TextBox
            string productCode = NewMethod();

            if (!string.IsNullOrEmpty(productCode))
            {
                // Gọi phương thức xóa
                DeleteProductFromDatabase(productCode);
            }
            else
            {
                MessageBox.Show("Please enter a product code to delete.");
            }
        }

        private string NewMethod()
        {
            throw new NotImplementedException();
        }

        private string 
            
            // Giả sử bạn sử dụng txb_code để nhập mã sản phẩm
        

        p public AddCustomer(string )
        {
            this.p = p;
        }

        void DeleteProductFromDatabase(string code)
        {
            string deletePurchaseHistoryQuery = "DELETE FROM PurchaseHistory WHERE Code = @Code";
            string deleteProductQuery = "DELETE FROM Product WHERE Code = @Code";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        // Xóa các bản ghi trong PurchaseHistory
                        using (SqlCommand command = new SqlCommand(deletePurchaseHistoryQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Code", code);
                            command.ExecuteNonQuery();
                        }

                        // Xóa sản phẩm
                        using (SqlCommand command = new SqlCommand(deleteProductQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Code", code);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Product deleted successfully.");
                            }
                            else
                            {
                                MessageBox.Show("No product found with the specified code.");
                            }
                        }

                        // Commit transaction
                        transaction.Commit();
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