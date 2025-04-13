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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CMS
{
    public partial class Admin : Form
    {

        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        public Admin(string userName)
        {
            InitializeComponent();
            username.Text = userName;
       
            timer1.Enabled = true;
            GetTotalOrders();
            GetTotalProductsAvailable();
            GetTotalRevenue();
            GetTotalCustomers();
            GetTotalCashiers();
            GetTotalOrdersToday();
            GetTotalSalesToday();
        }

      

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sidebars.Top = userAdded.Top;
            sidebars.Height = userAdded.Height;

            AddUser adduser = new AddUser(username.Text);
            adduser.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
          sidebars.Top =   add_prod.Top;
            sidebars.Height = add_prod.Height;

            product pro = new product(username.Text);
            pro.Show();
            this.Hide();



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
         
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        public void setlogin_user(string Name )
        {
          

        }

        private void username_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dates.Text = (DateTime.Now).ToString();
        }

        private void sidebars_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {


            sidebars.Top = button5.Top;
            sidebars.Height = button5.Height;


            Menu_items menu = new Menu_items(username.Text);
            menu.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sidebars.Top = button6.Top;
            sidebars.Height = button6.Height;

            order_management manage = new order_management(username.Text);
            manage.Show();
            this.Hide();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {

                return;
            }


            Form2 loginForm = new Form2();
            loginForm.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void order_total_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }



        public void GetTotalOrders()
        {
            int totalOrders = 0;

            string query = "SELECT COUNT(*) AS TotalOrdersToday FROM Orders ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    totalOrders = (int)command.ExecuteScalar();
                }
            }

            order_total.Text = totalOrders.ToString();
        }
        public void GetTotalProductsAvailable()
        {
            int totalProducts = 0;

            string query = "SELECT COUNT(*) AS TotalProducts FROM Product WHERE Status = 'Active';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    totalProducts = (int)command.ExecuteScalar();
                }
            }

            ava_prod.Text = totalProducts.ToString();
        }

        public void GetTotalRevenue()
        {
            decimal totalRevenue = 0;

            string query = "SELECT SUM(Total) AS TotalRevenue FROM Orders;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalRevenue = Convert.ToDecimal(result);
                    }
                }
            }
            total_sale.Text = totalRevenue.ToString();

           
        }

   

        public void GetTotalCustomers()
        {
            int totalCustomers = 0;

            string query = "SELECT COUNT(*) AS TotalCustomers FROM Users WHERE Role = 'Customer';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    totalCustomers = (int)command.ExecuteScalar();
                }
            }

            total_customer.Text = totalCustomers.ToString();
        }

        public void GetTotalCashiers()
        {
            int totalCashiers = 0;

            string query = "SELECT COUNT(*) AS TotalCashiers FROM Users WHERE Role = 'Cashier';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    totalCashiers = (int)command.ExecuteScalar();
                }
            }

            total_cashier.Text = totalCashiers.ToString();
        }

        public void GetTotalOrdersToday()
        {
            int totalOrdersToday = 0;

            string query = "SELECT COUNT(*) AS TotalOrdersToday FROM Orders WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    totalOrdersToday = (int)command.ExecuteScalar();
                }
            }

            delivered_order.Text = totalOrdersToday.ToString();
          
        }

        public void GetTotalSalesToday()
        {
            decimal totalSalesToday = 0;

            string query = "SELECT SUM(Total) AS TotalSalesToday FROM Orders WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalSalesToday = Convert.ToDecimal(result);
                    }
                }
            }

            total_income.Text = totalSalesToday.ToString();
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Show();
        }

        private void total_income_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
