using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CMS
{
    public partial class ORDERADD : Form
    {
        string sql_connect = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        int cashierid;
        string name;
        public ORDERADD(string name,int cashierid)
        {
            this.name = name;
            this.cashierid = cashierid;   
            InitializeComponent();
            InitializeDataGridView();
            intializeOrderAddGrid();
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


        public void DisplayOrder(int orderID,int userID, int productID, int quantity, string discountType, decimal total)
        {
            addordergrid.Rows.Add(orderID,userID, productID, quantity, discountType, total);
        }

        private void intializeOrderAddGrid()
        {

            dataGridView1.Columns.Add("UserID", "UserID");
            dataGridView1.Columns.Add("Username", "Username");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("Role", "Role");
            dataGridView1.Columns.Add("Status", "Status");
            dataGridView1.Columns.Add("Reg_date", "Reg_date");
            dataGridView1.Columns.Add("OrderCount", "OrderCount");
        }

        private void addordergrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void search_Click(object sender, EventArgs e)
        {
            string userid = user_id.Text;

            try
            {
                // Connect to the database
                using (SqlConnection connect = new SqlConnection(sql_connect))
                {
                    connect.Open();
                    string selectData = "SELECT * FROM Users WHERE UserID = @Userid";

                    // Execute the query
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@Userid", userid);

                        // Read the data
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Display user details in a new DataGridView
                                DisplayUserDetails(reader);
                            }
                            else
                            {
                                MessageBox.Show("User not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



        // Method to display user details in a new DataGridView along with order count (Join query)
        private void DisplayUserDetails(SqlDataReader reader)
        {


         

     
            int userID = Convert.ToInt32(reader["UserID"]);

            try
            {
             
                using (SqlConnection connect = new SqlConnection(sql_connect))
                {
                    connect.Open();

                    string query = $"SELECT u.UserID, u.Username, u.Email, u.Role, u.Status, u.reg_date, COUNT(o.OrderID) AS OrderCount  " +
                                   $"FROM Users u " +
                                   $"LEFT JOIN Orders o ON u.UserID = o.UserID " +
                                   $"INNER JOIN Product p ON o.ProductID = p.ProductID " +
                                   $"WHERE u.UserID = {userID} " +
                                   $"GROUP BY u.UserID, u.Username, u.Email, u.Role, u.Status, u.reg_date";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader result = cmd.ExecuteReader())
                        {
                            if (result.Read())
                            {
                                dataGridView1.Rows.Add(
                                    result["UserID"],
                                    result["Username"],
                                    result["Email"],
                                    result["Role"],
                                    result["Status"],
                                    result["reg_date"],
                                    result["OrderCount"]
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ORDERADD_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cashier cash = new Cashier(name,cashierid);
            cash.Show();
            this.Hide();
        }
    }
}
