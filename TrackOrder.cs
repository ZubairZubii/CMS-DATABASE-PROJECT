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
    public partial class TrackOrder : Form
    {
        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        int user_id;
        string name;
        public TrackOrder(string name,int userid)
        {
            this.name = name;
            this.user_id = userid;  
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            string status = "";
            try
            {
                string trackingId = enter_no.Text;

                string query = @"SELECT u.Username, u.Email, o.TotalSale AS Price, od.OrderDate AS Date, od.Status
                        FROM Users u
                        JOIN OrderTracking ot ON u.UserID = ot.UserID
                        JOIN SalesRecord o ON ot.SalesID = o.SalesID
                        JOIN Orders od ON ot.OrderID = od.OrderID
                        WHERE ot.TrackingID = @TrackingId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingId", trackingId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user_name.Text = reader["Username"].ToString();
                        email.Text = reader["Email"].ToString();
                        price.Text = reader["Price"].ToString();
                        date.Text = reader["Date"].ToString();
                        status = reader["Status"].ToString();

                        // Update the location of the status bar based on the order status
                        switch (status)
                        {
                            case "Pending":
                                bar.Location = new Point(70, 120);
                                break;
                            case "In Progress":
                                bar.Location = new Point(251, 120);
                                break;
                            case "Completed":
                                bar.Location = new Point(406, 120);
                                break;
                            default:
                                // Handle unknown status
                                break;
                        }

                        // Remove the tracking ID if the status is completed
                        if (status == "Completed")
                        {
                            RemoveTrackingID(trackingId);
                            MessageBox.Show("This order has been completed. The tracking ID has been removed from the system.", "Order Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Order not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveTrackingID(string trackingId)
        {
            try
            {
                string query = @"DELETE FROM OrderTracking WHERE TrackingID = @TrackingId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingId", trackingId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void user_name_Click(object sender, EventArgs e)
        {

        }

        private void email_Click(object sender, EventArgs e)
        {

        }

        private void price_Click(object sender, EventArgs e)
        {

        }

        private void quanity_Click(object sender, EventArgs e)
        {

        }

        private void date_Click(object sender, EventArgs e)
        {

        }

        private void enter_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void TrackOrder_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Customer cus = new Customer(name,user_id);
            cus.Show();
            this.Hide();

        }
    }
}
