using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS
{
    public partial class order_management : Form
    {

        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        string username;
        public order_management(string username)
        {
            this.username = username;
            InitializeComponent();
            //  InitializeDataGridView();
            LoadOrders();
            order_statistics();
        }


        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Clear();

            // Add columns to the DataGridView
            dataGridView1.Columns.Add("OrderID", "Order ID");
            dataGridView1.Columns.Add("UserID", "User ID");
            dataGridView1.Columns.Add("ProductID", "Product ID");
            dataGridView1.Columns.Add("Quantity", "Quantity");
            dataGridView1.Columns.Add("DiscountID", "Discount ID");
            dataGridView1.Columns.Add("Total", "Total");
            dataGridView1.Columns.Add("OrderDate", "Order Date");
            dataGridView1.Columns.Add("Status", "Status");

            // Set column properties if needed
            // For example:
            // dataGridViewOrders.Columns["OrderDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void order_management_Load(object sender, EventArgs e)
        {
            Admin admin  =  new Admin(username);
            admin.Show();
            admin.Hide();
        }


        private void LoadOrders()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT OrderID, UserID, ProductID, Quantity, DiscountID, Total, OrderDate, Status FROM Orders";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }


        private void UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Status", newStatus);
                    command.Parameters.AddWithValue("@OrderID", orderId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Order status updated successfully.");
                        dataGridView1.Columns.Clear();
                        LoadOrders(); // Refresh DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Failed to update order status.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating order status: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
         

            order_management manage_order = new order_management(username);
            bar.Top = manage_order.Top;
            bar.Height = manage_order.Height;
            manage_order.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cash_prod_clear_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int orderId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);

                string newStatus = comboBox1.Text;

                UpdateOrderStatus(orderId, newStatus);
            }
            else
            {
                MessageBox.Show("Please select a row to update its status.");
            }
        }


        public void order_statistics()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                String pendingOrdersQuery = "SELECT COUNT(*) FROM Orders WHERE Status = 'Pending'";
                SqlCommand pendingOrdersCommand = new SqlCommand(pendingOrdersQuery, connection);
                int pendingOrders = (int)pendingOrdersCommand.ExecuteScalar();
                no_pen.Text = "Pending Orders: " + pendingOrders;

                // Calculate number of orders in progress
                string inProgressOrdersQuery = "SELECT COUNT(*) FROM Orders WHERE Status = 'In Progress'";
                SqlCommand inProgressOrdersCommand = new SqlCommand(inProgressOrdersQuery, connection);
                int inProgressOrders = (int)inProgressOrdersCommand.ExecuteScalar();
                no_prog.Text = "Orders In Progress: " + inProgressOrders;

                // Calculate number of completed orders
                string completedOrdersQuery = "SELECT COUNT(*) FROM Orders WHERE Status = 'Completed'";
                SqlCommand completedOrdersCommand = new SqlCommand(completedOrdersQuery, connection);
                int completedOrders = (int)completedOrdersCommand.ExecuteScalar();
                no_com.Text = "Completed Orders: " + completedOrders;

            }
        }
private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pending_order pend = new Pending_order(username);
            bar.Top = pend.Top;
            bar.Height = pend.Height;
            pend.Show();
            this.Hide();
        }

        private void bar_TextChanged(object sender, EventArgs e)
        {

        }

        private void inprogress_order_Click(object sender, EventArgs e)
        {
            In_progress_order prog = new In_progress_order(username);
            bar.Top = prog.Top;
            bar.Height = prog.Height;
            prog.Show(); this.Hide();
        }

        private void deliver_order_Click(object sender, EventArgs e)
        {
            Delivered_order del = new Delivered_order(username);
            bar.Top = del.Top;
            bar.Height = del.Height;
            del.Show();this.Hide();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin(username);  
            admin.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
