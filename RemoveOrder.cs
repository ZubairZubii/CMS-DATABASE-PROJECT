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

namespace CMS
{
    public partial class RemoveOrder : Form
    {

        string sql_connect = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";
      
        int cashierid;
        string name;
        public RemoveOrder(string name,int cashierid)
        {
            this.name = name;
            this.cashierid = cashierid;  
            InitializeComponent();
            InitializeDataGridView();
            InitializeDataGridView1();
        }

        private void InitializeDataGridView()
        {

            addordergrid.Columns.Clear();

            addordergrid.Columns.Add("OrderID", "OrderID");
            addordergrid.Columns.Add("UserID", "UserID");
            addordergrid.Columns.Add("ProductID", "ProductID");
            addordergrid.Columns.Add("Quantity", "Quantity");
            addordergrid.Columns.Add("DiscountID", "DiscountID");
            addordergrid.Columns.Add("Total", "Total");
        }

        private void InitializeDataGridView1()
        {
            // Add columns to the DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("UserID", "UserID");
            dataGridView1.Columns.Add("Username", "Username");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("Role", "Role");
            dataGridView1.Columns.Add("Status", "Status");
            dataGridView1.Columns.Add("Reg_date", "Reg_date");
            dataGridView1.Columns.Add("OrderCount", "OrderCount");
        }
        public void DisplayOrder(int orderID, int userID, int productID, int quantity, string discountType, decimal total)
        {
            addordergrid.Rows.Add(orderID, userID, productID, quantity, discountType, total);
        }



        private void UpdateAndDisplayUserDetails()
        {
            // Clear existing data in dataGridView1
            dataGridView1.Rows.Clear();

            try
            {
                using (SqlConnection connect = new SqlConnection(sql_connect))
                {
                    connect.Open();

                    string query = $"SELECT u.UserID, u.Username, u.Email, u.Role, u.Status, u.reg_date, COUNT(o.OrderID) AS OrderCount " +
                                   $"FROM Users u " +
                                   $"LEFT JOIN Orders o ON u.UserID = o.UserID " +
                                   $"INNER JOIN Product p ON o.ProductID = p.ProductID " +
                                   $"GROUP BY u.UserID, u.Username, u.Email, u.Role, u.Status, u.reg_date";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(
                                    reader["UserID"],
                                    reader["Username"],
                                    reader["Email"],
                                    reader["Role"],
                                    reader["Status"],
                                    reader["reg_date"],
                                    reader["OrderCount"]
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateAndDisplayOrderDetails()
        {
            // Clear existing data in addordergrid
            addordergrid.Rows.Clear();

            try
            {
                using (SqlConnection connect = new SqlConnection(sql_connect))
                {
                    connect.Open();

                    // Query to retrieve order details
                    // Query to retrieve order details with nested query
                    string query = @"SELECT *  FROM Orders ";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add order details to the DataGridView
                                addordergrid.Rows.Add(
                                    reader["OrderID"],
                                    reader["UserID"],
                                    reader["ProductID"],
                                    reader["Quantity"],
                                    reader["DiscountID"],
                                    reader["Total"]
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void RemoveOrders(int orderID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(sql_connect))
                {
                    connect.Open();
                    string deleteQuery = "DELETE FROM Orders WHERE OrderID = @OrderID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Order removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Order not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        UpdateAndDisplayOrderDetails();
                        UpdateAndDisplayUserDetails();
                    }
                }

                // After deleting the order, update and display the user details
           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void addordergrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void remove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(order_id.Text);
            RemoveOrders(id);


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cashier cash = new Cashier(name, cashierid);
            cash.Show();
            this.Hide();

        }
    }
}
