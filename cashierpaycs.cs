using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS
{
    public partial class cashierpaycs : Form
    {


        string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        int cashierID;
        int userid;
        string name;
        public cashierpaycs(string name ,int cashierID)
        {
            
            this.name = name;
            this.cashierID = cashierID;


            InitializeComponent();
            InitializeDataGridView();
            InitializeOrderDetailsGridView();
            instialize_grid_helper();
        }


        void instialize_grid_helper()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string selectOrders = @"
                                    SELECT * 
                                    FROM Orders 
                                    WHERE UserID = @CashierID 
                                    AND CONVERT(DATE, OrderDate) = CONVERT(DATE, GETDATE())";
                          


                using (SqlCommand selectCmd = new SqlCommand(selectOrders, con))
                {

                    selectCmd.Parameters.AddWithValue("@CashierID", cashierID);
                  
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
                            DisplayOrder(orderID, orderUserID, orderProductID, orderQuantity, orderDiscountType, orderTotal);
                        }

                    }
                }

            }

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


        private void InitializeOrderDetailsGridView()
        {
            orderdetail.Columns.Clear();
            orderdetail.Columns.Add("OrderID", "OrderID");
            orderdetail.Columns.Add("Quantity", "Quantity");
            orderdetail.Columns.Add("Username", "Customer Name");
            orderdetail.Columns.Add("ProductName", "Product Name");
            orderdetail.Columns.Add("TotalPrice", "Total Price");
            orderdetail.Columns.Add("DiscountID", "DiscountID");
            orderdetail.Columns.Add("DiscountName", "DiscountName");

        }

    
        public void DisplayOrder(int orderID, int userID, int productID, int quantity, string discountType, decimal total)
        {
            addordergrid.Rows.Add(orderID, userID, productID, quantity, discountType, total);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void order_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadOrderDetails()
        {
            int userID = int.Parse(cus_id.Text);

            this.userid = userID;
            string query = "SELECT o.OrderID, o.Quantity, c.Username, p.ProductName, o.Total, o.DiscountID ,d.DiscountName " +
                    "FROM Orders o " +
                    "INNER JOIN Users c ON o.UserID = c.UserID " +
                    "INNER JOIN Product p ON o.ProductID = p.ProductID " +
                    "INNER JOIN Discounts d ON o.DiscountID = d.DiscountID " +
                    "WHERE o.UserID = @UserID AND  CONVERT(DATE, o.OrderDate) = CONVERT(DATE, GETDATE())";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);
               
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    decimal totalPayment = 0; // Initialize total payment variable
                    decimal dicount_pay = 0; // Initialize total payment variable
                    while (reader.Read())
                    {
                        orderdetail.Rows.Add(
                            reader["OrderID"],
                            reader["Quantity"],
                            reader["Username"],
                            reader["ProductName"],
                            reader["Total"],
                            reader["DiscountID"],
                             reader["DiscountName"]
                        );

                        totalPayment += Convert.ToDecimal(reader["Total"]);
                        dicount_pay += GetDiscountAmount(Convert.ToInt32(reader["DiscountID"]));
                    }


                    price.Text = totalPayment.ToString();
                    discount.Text = dicount_pay.ToString();
                }
            }
        }

        private decimal GetDiscountAmount(int discountID)
        {
            decimal discountAmount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DiscountAmount FROM Discounts WHERE DiscountID = @DiscountID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@DiscountID", discountID);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            discountAmount = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return discountAmount;
        }


        private decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            bool discountApplied = false;
            string discountName="";
            foreach (DataGridViewRow row in orderdetail.Rows)
            {
                totalPrice += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                int discountID = Convert.ToInt32(row.Cells["DiscountID"].Value);
                if (discountID != 0)
                {

                    discountName = GetDiscountName(discountID);

                    decimal discountAmount = GetDiscountAmount(discountID);
                    totalPrice -= discountAmount; 
                    discountApplied = true; 
                }
            }

            if (discountApplied)
            {
                MessageBox.Show($"A discount {discountName} is applied!", "Discount Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            return totalPrice;
        }


        private void ProcessPayment()
        {
            decimal totalOrderPrice = CalculateTotalPrice();
            decimal amountPaid = decimal.Parse(amount.Text);
            decimal change = amountPaid - totalOrderPrice;

  
            changepay.Text = change.ToString();

            if (change >= 0)
            {
                
                MessageBox.Show($"Payment successful! Change: {change}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

              
                UpdateStock();

            
                RecordSale();

                ShowTrackOrderDialog();
            }
            else
            {
                MessageBox.Show("Insufficient amount paid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowTrackOrderDialog()
        {
            try
            {
                string trackingId = "";

               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                
                    string selectTrackingIDQuery = @"SELECT TOP 1 TrackingID FROM OrderTracking ORDER BY TrackingID DESC";

                    using (SqlCommand cmd = new SqlCommand(selectTrackingIDQuery, connection))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            trackingId = result.ToString();
                        }
                    }
                }

     
                if (!string.IsNullOrEmpty(trackingId))
                {
                    DialogResult result = MessageBox.Show($"Your order has been recorded successfully. Your tracking ID is: {trackingId}. Would you like to track your order?", "Order Recorded", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                 
                    if (result == DialogResult.Yes)
                    {
                        TrackOrder trackOrderPage = new TrackOrder(name,userid);
                        trackOrderPage.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Unable to retrieve tracking ID. Please contact customer support for assistance.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateStock()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in orderdetail.Rows)
                    {
                        if (!row.IsNewRow)
                        {

                            int orderId = int.Parse(row.Cells["OrderID"].Value.ToString());
                            int quantity = int.Parse(row.Cells["Quantity"].Value.ToString());

                            string updateQuery = "UPDATE Product " +
                                                 "SET StockCategory = CAST(StockCategory AS int) - @Quantity " +
                                                 "WHERE ProductID IN " +
                                                 "(SELECT ProductID FROM Orders WHERE OrderID = @OrderId)";


                            using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@Quantity", quantity);
                                cmd.Parameters.AddWithValue("@OrderId", orderId);

                                int rez = cmd.ExecuteNonQuery();
                                if (rez > 0)
                                {

                              //      MessageBox.Show($"Stock successful! Change", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                }
                                else
                                {
                                    MessageBox.Show("Stock not updated!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
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

        private int GetTotalQuantitySold()
        {
            int totalQuantitySold = 0;

            foreach (DataGridViewRow row in orderdetail.Rows)
            {
                if (!row.IsNewRow)
                {
                    totalQuantitySold += Convert.ToInt32(row.Cells["Quantity"].Value);
                }
            }

            return totalQuantitySold;
        }

        private void RecordSale()
        {
            try
            {
                // Assuming you have a control named 'cus_id' where the user ID is entered or displayed
                int userID = int.Parse(cus_id.Text);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string insertSalesRecordQuery = @"INSERT INTO SalesRecord (CashierID, Cashiername, TotalSale, TotalQuantitySold, SalesDate) 
                                      SELECT @CashierID, u.Username, @TotalSale, @TotalQuantitySold, @SalesDate
                                      FROM Users u
                                      WHERE u.UserID = @CashierID";

                    // Execute the insert query
                    using (SqlCommand cmd = new SqlCommand(insertSalesRecordQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@CashierID", cashierID);
                        cmd.Parameters.AddWithValue("@TotalSale", CalculateTotalPrice());
                        cmd.Parameters.AddWithValue("@TotalQuantitySold", GetTotalQuantitySold());
                        cmd.Parameters.AddWithValue("@SalesDate", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }

                    // Get the SalesID of the last inserted record
                    string selectSalesIDQuery = @"SELECT TOP 1 SalesID FROM SalesRecord ORDER BY SalesID DESC";

                    int salesID;

                    // Execute the query to get the SalesID
                    using (SqlCommand cmd = new SqlCommand(selectSalesIDQuery, connection))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            MessageBox.Show("Error: Unable to retrieve SalesID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        salesID = Convert.ToInt32(result);
                    }

                    // Insert into OrderTracking table
                    string insertOrderTrackingQuery = @"INSERT INTO OrderTracking (UserID, SalesID, OrderID) 
                                       SELECT @UserID, @SalesID, o.OrderID
                                       FROM Orders o
                                       WHERE o.UserID = @UserID";

                    // Execute the insert query for OrderTracking
                    using (SqlCommand orderTrackingCmd = new SqlCommand(insertOrderTrackingQuery, connection))
                    {
                        orderTrackingCmd.Parameters.AddWithValue("@UserID", userID);
                        orderTrackingCmd.Parameters.AddWithValue("@SalesID", salesID);

                        orderTrackingCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Sale recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void remove_Click(object sender, EventArgs e)
        {
            orderdetail.Rows.Clear();
            LoadOrderDetails();
        }

        private void orderdetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
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

            ProcessPayment();

        }


        private void UpdateProductStock(int orderID, int remainingStock)
        {
            // Update product stock in the database
            string updateQuery = "UPDATE Product SET StockCategory = @Stock WHERE ProductID = " +
                                 "(SELECT ProductID FROM Orders WHERE OrderID = @OrderID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Stock", remainingStock);
                command.Parameters.AddWithValue("@OrderID", orderID);

                command.ExecuteNonQuery();
            }
        }

       
        private void receipt_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }


        private string GetDiscountName(int discountID)
        {
            string discountName = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DiscountName FROM Discounts WHERE DiscountID = @DiscountID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@DiscountID", discountID);
                        object result = cmd.ExecuteScalar();
                        if (result != null )
                        {
                            discountName = Convert.ToString(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return discountName;
        }


        private decimal ReceiptCalculateTotalPrice()
        {
            decimal totalPrice = 0;
          
            foreach (DataGridViewRow row in orderdetail.Rows)
            {
                totalPrice += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                int discountID = Convert.ToInt32(row.Cells["DiscountID"].Value);
                if (discountID != 0)
                {
              

                    decimal discountAmount = GetDiscountAmount(discountID);
                    totalPrice -= discountAmount;
                  
                }
            }

          

            return totalPrice;
        }


        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (orderdetail != null)
            {
                int printableWidth = e.PageBounds.Width - 100;
                int printableHeight = e.PageBounds.Height - 100;
                Rectangle printableArea = new Rectangle(50, 50, printableWidth, printableHeight);


                Graphics graphics = e.Graphics;
                Font headingFont = new Font("Arial", 14, FontStyle.Bold); 
                Font contentFont = new Font("Arial", 12, FontStyle.Italic);
                Font rezFont = new Font("Arial", 10, FontStyle.Italic);

                Brush brush = Brushes.Black;

              
                int startX = e.MarginBounds.Left;
                int startY = e.MarginBounds.Top;
                int endX = e.MarginBounds.Right;

                StringFormat centerAlignment = new StringFormat
                {
                    Alignment = StringAlignment.Center
                };

              
                string headerText = "Cafe Management System Receipt";
                graphics.DrawString(headerText, headingFont, brush, new Point((startX + endX) / 2, startY), centerAlignment);

            
                startY += 40;
                graphics.DrawLine(new Pen(Brushes.Black), startX, startY, endX, startY);

              
                string orderHeaderText = "Order Details:";
                startY += 10;
                graphics.DrawString(orderHeaderText, headingFont, brush, new Point(startX, startY));

                decimal totalPaymentAfterDiscount = 0;
                startY += 30;
                string totalPrice = "";


                foreach (DataGridViewRow row in orderdetail.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string orderID = row.Cells["OrderID"].Value.ToString();
                        string quantity = row.Cells["Quantity"].Value.ToString();
                        string customerName = row.Cells["Username"].Value.ToString();
                        string productName = row.Cells["ProductName"].Value.ToString();
                         totalPrice = row.Cells["TotalPrice"].Value.ToString();
                        string discountName = ""; // Placeholder for discount name
                        int discountID = Convert.ToInt32(row.Cells["DiscountID"].Value);
                        discountName = GetDiscountName(discountID);

                        totalPaymentAfterDiscount = ReceiptCalculateTotalPrice();
                        // Fetch discount information



                        string orderDetails = $"Order ID:                -------------------------------------------- {orderID}\n" +
                                              $"Quantity:                -------------------------------------------- {quantity}\n" +
                                              $"Customer Name:  -------------------------------------------- {customerName}\n" +
                                              $"Product Name:     -------------------------------------------- {productName}\n" +
                                              $"Total Price:            -------------------------------------------- {totalPrice}\n" +
                                              $"Discount Applied:  -------------------------------------------- {discountName}\n" + 
                                              $"--------------------------------------------------------------------------------"; ;

                        graphics.DrawString(orderDetails, contentFont, brush, new Point(startX, startY));
                        startY += 130;
                    }
                }

               
                startY += 10;
                graphics.DrawLine(new Pen(Brushes.Black), startX, startY, endX, startY);

          
                decimal amountPaid = decimal.Parse(amount.Text);
                decimal change = amountPaid - totalPaymentAfterDiscount;
                startX += 50;
                string paymentDetails = $"Total Payment: {totalPrice}\nTotal Payment after Discount: {totalPaymentAfterDiscount}\nAmount Paid: {amountPaid}\nChange: {change}";
                graphics.DrawString(paymentDetails, rezFont, brush, new Point(startX + 400, startY += 20), centerAlignment);
            }
        }

        private void cashierpaycs_Load(object sender, EventArgs e)
        {

        }

        private void addordergrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Cashier cash =  new Cashier(name,cashierID);
            cash.Show();
            this.Hide();
        }
    }

}
