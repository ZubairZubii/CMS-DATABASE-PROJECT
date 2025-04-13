using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS
{
    public partial class Cashier : Form
    {


        int cashierID;
        string cashierName = "";
        string sql_connect = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        public Cashier(string name, int id)
        {
            InitializeComponent(); // Initialize UI components first

            cashierName = name;
            username.Text = name; // Initialize the username control with the passed name

            cashierID = id;

            InitializeDataGridView();

            ProductDisplay();
            CustomerDisplay();
        }


        public void InitializeDataGridView()
        {

        

            cashiergrid1.Columns.Add("ProductID", "Product ID");
            cashiergrid1.Columns.Add("ProductName", "Product Name");
            cashiergrid1.Columns.Add("Price", "Price");
            cashiergrid1.Columns.Add("Stock", "Stock Category");
            cashiergrid1.Columns.Add("Type", "Type");
            cashiergrid1.Columns.Add("Status", "Status");
      
            cashiergrid1.Columns.Add("InsertDate", "Insert Date");
            cashiergrid1.Columns.Add("UpdateDate", "Update Date");


            cashiergrid2.Columns.Add("UserID", "User ID");
            cashiergrid2.Columns.Add("Username", "Username");
            cashiergrid2.Columns.Add("Email", "Email");
            cashiergrid2.Columns.Add("Role", "Role");
            cashiergrid2.Columns.Add("Status", "Status");
            cashiergrid2.Columns.Add("reg_date", "Reg_date ");
         
        }

        private void ProductDisplay()
        {

            ProductManager prod = new ProductManager();
            List<Product> products = prod.LoadProducts();
            cashiergrid1.Rows.Clear();

            foreach (var product in products)
            {
                cashiergrid1.Rows.Add(
                    product.ProductID,
                    product.ProductName,
                    product.Price,
                    product.Stock,
                    product.Type,
                     product.Status,
            

                    product.InsertDate.ToString("yyyy-MM-dd"),
                    product.UpdateDate.ToString("yyyy-MM-dd")


                ); ;
            }
        }


        private void CustomerDisplay()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection("Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false"))
                {
                    connect.Open();
                    string selectData = $"SELECT * FROM Users WHERE Role = 'Customer'";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cashiergrid2.Rows.Add(
                                    reader["UserID"],
                                    reader["Username"],
                                    reader["Email"],
                                    reader["Role"],
                                    reader["Status"],
                                    reader["reg_date"]

                                   

                                ); ;
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


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cashiergrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
      

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void prod_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            prod_id.SelectedIndex = -1;
            prod_id.Items.Clear();

            string selectedValue = prod_type.Text;

            if (selectedValue != null)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection("Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false"))
                    {
                        connect.Open();
                        string selectData = $"SELECT * FROM Product WHERE Type = '{selectedValue}' AND Status = 'Available'";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string value = reader["ProductID"].ToString();
                                    prod_id.Items.Add(value);
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
        }

        private void prod_idcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = prod_id.SelectedItem as string;

            if (selectedValue != null)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection("Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false"))
                    {
                        connect.Open();
                        string selectData = $"SELECT * FROM Product WHERE ProductID = '{selectedValue}' AND Status = 'Available'";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string prodName = reader["ProductName"].ToString();
                                    string prodPrice = reader["Price"].ToString();

                                    name_prod.Text = prodName;
                                    prod_price.Text = prodPrice;
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
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cashiergrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = cashiergrid2.Rows[e.RowIndex];
            cus_id.Text = selectedRow.Cells["UserID"].Value.ToString();

        }

        private void price_Click(object sender, EventArgs e)
        {

        }

        private void amount_TextChanged(object sender, EventArgs e)
        {

        }

        private void change_Click(object sender, EventArgs e)
        {

        }

        private void pay_Click(object sender, EventArgs e)
        {

        }

        private void cash_prod_Add_Click(object sender, EventArgs e)
        {

        }

        public decimal CalculateTotal(int quantity, decimal price, decimal discountAmount)
        {
     
            decimal subtotal = quantity * price;
            decimal discountedTotal = subtotal - (subtotal * discountAmount);
            return discountedTotal;
        }

      


        private void cash_prod_Add_Click_1(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(cus_id.Text);
            int productID = Convert.ToInt32(prod_id.SelectedItem);
            int quantity = Convert.ToInt32(quantity_numeric.Value);
            string discountType = disount.Text.ToString();

            int discountID = 0;
            decimal discountAmount = 0;

            try
            {
                using (SqlConnection connect = new SqlConnection(sql_connect))
                {
                    connect.Open();
                    string selectData = "SELECT DiscountID,DiscountAmount FROM Discounts WHERE DiscountName = @DiscountName";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@DiscountName", discountType);

                        // Execute the query and check if any rows are returned
                        using (SqlDataReader result = cmd.ExecuteReader())
                        {
                            if (result.HasRows)
                            {
                                // Read the first row and get the value of DiscountID
                                result.Read();
                                discountID = Convert.ToInt32(result["DiscountID"]);
                                discountAmount = Convert.ToDecimal(result["DiscountAmount"]);
                            }
                            else
                            {
                                // No rows returned, handle the case appropriately (e.g., set discountID to 0 or show an error message)
                                MessageBox.Show("No discount found for the selected discount type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            decimal total = CalculateTotal(quantity, Convert.ToDecimal(prod_price.Text), discountAmount);

            try
            {
                using (SqlConnection connect = new SqlConnection(sql_connect))
                {
                    connect.Open();
                    string insertData = "INSERT INTO Orders (UserID, ProductID, Quantity, DiscountID, Total) VALUES (@UserID, @ProductID, @Quantity, @DiscountID, @Total)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@ProductID", productID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@DiscountID", discountID); // Use the discount ID instead of name
                        cmd.Parameters.AddWithValue("@Total", total);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Order placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ORDERADD orderAddForm = new ORDERADD(cashierName,cashierID);


                        string selectOrders = "SELECT * FROM Orders";
                        using (SqlCommand selectCmd = new SqlCommand(selectOrders, connect))
                        {
                            using (SqlDataReader reader = selectCmd.ExecuteReader())
                            {


                                while (reader.Read())
                                {
                                    int orderID = Convert.ToInt32(reader["OrderID"]);
                                    int orderUserID = Convert.ToInt32(reader["UserID"]);
                                    int orderProductID = Convert.ToInt32(reader["ProductID"]);
                                    int orderQuantity = Convert.ToInt32(reader["Quantity"]);
                                    string orderDiscountType = reader["DiscountID"].ToString();
                                    decimal orderTotal = Convert.ToDecimal(reader["Total"]);

                                    // Display each order in the ORDERADD form
                                    orderAddForm.DisplayOrder(orderID,orderUserID, orderProductID, orderQuantity, orderDiscountType, orderTotal);
                                }
                                orderAddForm.Show();
                                this.Hide();
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

        private void cash_prod_remove_Click(object sender, EventArgs e)
        {


            RemoveOrder rem = new RemoveOrder(cashierName,cashierID);

            try
            {
                using (SqlConnection con = new SqlConnection(sql_connect))
                {
                    con.Open();

                    string selectOrders = "SELECT * FROM Orders";
                    using (SqlCommand selectCmd = new SqlCommand(selectOrders, con))
                    {
                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {
                                int orderID = Convert.ToInt32(reader["OrderID"]);
                                int orderUserID = Convert.ToInt32(reader["UserID"]);
                                int orderProductID = Convert.ToInt32(reader["ProductID"]);
                                int orderQuantity = Convert.ToInt32(reader["Quantity"]);
                                string orderDiscountType = reader["DiscountID"].ToString();
                                decimal orderTotal = Convert.ToDecimal(reader["Total"]);

                                // Display each order in the ORDERADD form
                                rem.DisplayOrder(orderID, orderUserID, orderProductID, orderQuantity, orderDiscountType, orderTotal);

                            }
                            rem.Show();
                            this.Hide();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void cash_prod_clear_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(sql_connect))
                {
                    connect.Open();

                   
                    string deleteAllQuery = "DELETE FROM Orders";
                    using (SqlCommand cmd = new SqlCommand(deleteAllQuery, connect))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("All orders cleared successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pay_order_Click(object sender, EventArgs e)
        {
            cashierpaycs pay = new cashierpaycs(cashierName,cashierID);
            try
            {
                using (SqlConnection con = new SqlConnection(sql_connect))
                {
                    con.Open();

                    string selectOrders = "SELECT * FROM Orders";
                    using (SqlCommand selectCmd = new SqlCommand(selectOrders, con))
                    {
                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {
                                int orderID = Convert.ToInt32(reader["OrderID"]);
                                int orderUserID = Convert.ToInt32(reader["UserID"]);
                                int orderProductID = Convert.ToInt32(reader["ProductID"]);
                                int orderQuantity = Convert.ToInt32(reader["Quantity"]);
                                string orderDiscountType = reader["DiscountID"].ToString();
                                decimal orderTotal = Convert.ToDecimal(reader["Total"]);

                                // Display each order in the ORDERADD form
                                pay.DisplayOrder(orderID, orderUserID, orderProductID, orderQuantity, orderDiscountType, orderTotal);

                            }
                            pay.Show();
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cashier_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            CashierRecord cr = new CashierRecord(cashierName,cashierID);
            cr.Show();
            this.Hide();
        }

        private void orderadd_Click(object sender, EventArgs e)
        {
            Cashier cashier = new Cashier(cashierName,cashierID);   
            cashier.Show(); 
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void username_Click(object sender, EventArgs e)
        {

        }
    }
}
