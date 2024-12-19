using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace SaleManagementWinform
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
            // Đặt biểu mẫu bắt đầu ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            // Vô hiệu hóa nút tối đa hóa/khôi phục
            this.MaximizeBox = false;

            // Tùy chọn: Đặt kiểu đường viền cố định để ngăn chặn việc thay đổi kích thước
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerForm customer = new CustomerForm();
            customer.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EmployeeForm employee = new EmployeeForm();
            employee.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PurchaseHistory purchaseHistory = new PurchaseHistory();
            purchaseHistory.Show();
            this.Hide();
        }
        public void ShowProductButton()
        {
            btn_Product.Visible = true; // Hiển thị nút sản phẩm
        }
        public void ShowEmployee()
        {
            btn_Employee.Visible = true;
        }
        public void ShowCustomer()
        {
            btn_Customer.Visible = true;
        }
        public void ShowPurchaseHistory()
        {
            btn_Purchase.Visible = true;
        }
        public void HideProductButton()
        {
            btn_Product.Visible = false; // Ẩn nút sản phẩm nếu cần
        }
        public void HideEmployee()
        {
            btn_Employee.Visible = false;
        }
        public void HideCustomer()
        {
            btn_Customer.Visible = false;
        }
        public void HidePurchaseHistory()
        {
            btn_Purchase.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }
        public void ShowLogout()
        {
            btn_Logout.Visible = true;
        }
        public void HideLogout()
        {
            btn_Logout.Visible = false;
        }

        private void btn_CustomerOders_Click(object sender, EventArgs e)
        {
            customerorders customerorders = new customerorders();
            customerorders.ShowDialog();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Oder oder = new Oder();
            oder.ShowDialog();
            this.Hide();
        }
        public void ShowOrder()
        {
            btn_Orders.Visible = true;
        }
        public void HideOrder()
        {
            btn_Orders.Visible = false;
        }
        public void ShowCustomerOrders()
        {
            btn_CustomerOders.Visible = true;
        }
        public void HideCustomerOrders()
        {
            btn_CustomerOders.Visible = false;
        }
    }
}