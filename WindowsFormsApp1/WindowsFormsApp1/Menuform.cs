using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Menuform : Form
    {
        public Menuform()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Main_form main = new Main_form();
            main.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerForm customer  = new CustomerForm ();
            customer .Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmployeeForm employee = new EmployeeForm ();
            employee.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PurchaseHistoryForm  purchaseHistory = new PurchaseHistoryForm ();
           purchaseHistory.Show();
            this.Hide();
        }
    }
}
