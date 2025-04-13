using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS
{

    public partial class Customer : Form
    {

        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";
        private Order order;
        private int userID;
        private string currentCategory = "All Items";
        string name;
        public Customer(string name , int userID)
        {
            this.name = name;
            this.userID = userID;
            InitializeComponent();
            order = new Order();
            intializegrid();
            LoadProductsFromDatabase();



        }

        public void intializegrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ProductID", "Product ID");
            dataGridView1.Columns.Add("ProductName", "Product Name");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Quantity", "Quantity");

        }
        private void LoadProductsFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT p.ProductID, p.ProductName, p.Price, p.Image, c.CategoryName, p.Type " +
                                   "FROM Product p " +
                                   "INNER JOIN Category c ON p.CategoryID = c.CategoryID " +
                                    "INNER JOIN Orders o ON p.ProductID = o.ProductID " +
                                    "INNER JOIN Discounts d ON o.DiscountID = d.DiscountID " +
                                   "WHERE c.CategoryName = @Category OR @Category = 'All Items'";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Category", currentCategory);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int productID = Convert.ToInt32(reader["ProductID"]);
                        string name = reader["ProductName"].ToString();
                        decimal price = Convert.ToDecimal(reader["Price"]);
                        string imageURL = reader["Image"].ToString(); // Trim whitespace from the image filename
                        string categoryName = reader["CategoryName"].ToString();
                        string type = reader["Type"].ToString();

                        Console.WriteLine("Image URL from the database: " + imageURL);
                        string imageName = Path.GetFileNameWithoutExtension(imageURL);

                        customerProduct product = new customerProduct(productID, name, price, imageName, categoryName, type);


                        AddProductToPanel(product);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void AddProductToPanel(customerProduct product)
        {
            Panel productPanel = new Panel();
            productPanel.BorderStyle = BorderStyle.FixedSingle;
            productPanel.Size = new Size(250, 250);
            productPanel.Location = new Point(109, -9);

            PictureBox pictureBox = new PictureBox();


            string imagePath = "C:\\Users\\Zubair\\Documents\\CMS\\CMS\\Resources\\" + product.ImageURL + ".png";
            try
            {
                if (File.Exists(imagePath))
                {
                    // Attempt to load the image
                    pictureBox.Image = Image.FromFile(imagePath);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Size = new Size(250, 130);
                    pictureBox.Location = new Point(0, -2);
                    productPanel.Padding = new Padding(0);
                    productPanel.Margin = new Padding(0);

                    // Add the picture box to the product panel
                    productPanel.Controls.Add(pictureBox);
                }
                else
                {
                    MessageBox.Show("Image file not found: " + imagePath);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                MessageBox.Show("Error loading image: " + ex.Message);
            }




            Label nameLabel = new Label();
            nameLabel.Text = product.Name;
            nameLabel.Location = new Point(16, 168);
            nameLabel.Font = new Font("Segoe UI Semibold", 10.2f, FontStyle.Bold | FontStyle.Italic);

            productPanel.Controls.Add(nameLabel);

            Label priceLabel = new Label();
            priceLabel.Text = $"{product.Price:C}";
            priceLabel.Location = new Point(181, 168);
            priceLabel.ForeColor = Color.Red;
            priceLabel.Font = new Font("Segoe UI Semibold", 10.2f, FontStyle.Bold | FontStyle.Italic);

            productPanel.Controls.Add(priceLabel);

            NumericUpDown quantityUpDown = new NumericUpDown();
            quantityUpDown.Minimum = 1;
            quantityUpDown.Maximum = 100;
            quantityUpDown.Value = 1;
            quantityUpDown.Location = new Point(122, 220);
            quantityUpDown.Size = new Size(120, 22);
            productPanel.Controls.Add(quantityUpDown);


            Label quantitylabel = new Label();
            quantitylabel.Text = "Quantity";
            quantitylabel.Size = new Size(69, 20);
            quantitylabel.Location = new Point(16, 220);
            quantitylabel.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold | FontStyle.Italic);
            productPanel.Controls.Add(quantitylabel);


            Label freeLabel = new Label();
            freeLabel.Text = "Free";
            freeLabel.Location = new Point(17, 191);
            freeLabel.Size = new Size(34, 17);
            freeLabel.Font = new Font("Segoe UI ", 7.8f, FontStyle.Italic);

            productPanel.Controls.Add(freeLabel);

            PictureBox iconpictureBox = new PictureBox();
            iconpictureBox.Image = CMS.Properties.Resources.f3810304460608b500a25571bc7d0dd9;
            iconpictureBox.Size = new Size(28, 22);
            iconpictureBox.Location = new Point(57, 191);
            iconpictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            productPanel.Controls.Add(iconpictureBox);

            PictureBox startpictureBox = new PictureBox();
            startpictureBox.Image = CMS.Properties.Resources.images__1_;
            startpictureBox.Size = new Size(14, 17);
            startpictureBox.Location = new Point(185, 191);
            startpictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            productPanel.Controls.Add(startpictureBox);


            Label rateLabel = new Label();
            rateLabel.Text = "3.3";
            rateLabel.Location = new Point(205, 191);
            rateLabel.Size = new Size(25, 17);
            rateLabel.Font = new Font("Segoe UI ", 7.8f, FontStyle.Italic);
            productPanel.Controls.Add(rateLabel);


            productPanel.Click += (sender, e) =>
                {
                    AddProductToOrder(product, (int)quantityUpDown.Value);
                };

            flowLayoutPanel1.Controls.Add(productPanel);

        }




        private void AddProductToOrder(customerProduct product, int quantity)
        {
            product.Quantity = quantity;

            order.AddProduct(product);

            decimal totalPrice = product.Price * product.Quantity;

            order.AddTotalPrice(totalPrice);
            order.AddTotalQuantity(product.Quantity);

            UpdatePriceLabel();
            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear(); // Clear the existing rows

            foreach (customerProduct product in order.GetOrderItems())
            {
                dataGridView1.Rows.Add(product.ProductID, product.Name, product.Price, product.Quantity);
            }
        }

        private void UpdatePriceLabel()
        {
            decimal totalPrice = order.GetTotalPrice();
            prod_price.Text = totalPrice.ToString();
            decimal totalquantity = order.GetTotalQuantity();
            quan.Text = totalquantity.ToString();
        }

        private void PlaceOrder()
        {
            try
            {
                // Ask for confirmation from the user
                DialogResult result = MessageBox.Show("Are you sure you want to place the order?", "Confirm Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If the user confirms, proceed with placing the order
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        foreach (customerProduct product in order.GetOrderItems())
                        {
                            string query = "INSERT INTO Orders (UserID, ProductID, Quantity, DiscountID, Total, OrderDate) " +
                                           "VALUES (@UserID, @ProductID, @Quantity, @DiscountID, @Total, @OrderDate)";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@UserID", userID);
                            command.Parameters.AddWithValue("@ProductID", product.ProductID);
                            command.Parameters.AddWithValue("@Quantity", product.Quantity);
                            command.Parameters.AddWithValue("@DiscountID", 2); // Assuming a default discount ID
                            decimal totalPrice = product.Price * product.Quantity;
                            command.Parameters.AddWithValue("@Total", totalPrice);
                            command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Order placed successfully!");
                        cashierpaycs cash = new cashierpaycs(name,userID);
                        cash.Show();
                        this.Hide();
                    }
                }
                else
                {
                    // If the user cancels, do nothing
                    MessageBox.Show("Order placement canceled by user.", "Order Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error placing order: " + ex.Message);
            }
        }





        private void RefreshProducts()
        {
            flowLayoutPanel1.Controls.Clear();
            LoadProductsFromDatabase();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cash_prod_clear_Click(object sender, EventArgs e)
        {
            PlaceOrder();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {


            PerformSearch();

        }



        private void PerformSearch()
        {
            Console.WriteLine("search from the database: " + search.Text);
            if (!string.IsNullOrEmpty(search.Text))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT p.ProductID, p.ProductName, p.Price, p.Image, c.CategoryName, p.Type " +
                                        "FROM Product p " +
                                        "INNER JOIN Category c ON p.CategoryID = c.CategoryID " +
                                        "WHERE p.ProductName LIKE @SearchText";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@SearchText", "%" + search.Text + "%");

                        SqlDataReader reader = command.ExecuteReader();
                        flowLayoutPanel1.Controls.Clear();

                        while (reader.Read())
                        {
                            int productID = Convert.ToInt32(reader["ProductID"]);
                            string name = reader["ProductName"].ToString();
                            decimal price = Convert.ToDecimal(reader["Price"]);
                            string imageURL = reader["Image"].ToString().Trim();
                            string categoryName = reader["CategoryName"].ToString();
                            string type = reader["Type"].ToString();

                            string imageName = Path.GetFileNameWithoutExtension(imageURL);

                            // Create and add product to flowLayoutPanel1
                            customerProduct product = new customerProduct(productID, name, price, imageName, categoryName, type);
                            AddProductToPanel(product);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching products: " + ex.Message);
                }
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void userAdded_Click(object sender, EventArgs e)
        {
            sidebars.Top = userAdded.Top;
            sidebars.Height = userAdded.Height;
            currentCategory = "All Items";
            RefreshProducts();

        }

        private void beverage_item_Click(object sender, EventArgs e)
        {
            sidebars.Top = beverage_item.Top;
            sidebars.Height = beverage_item.Height;

            currentCategory = "Beverages";
            RefreshProducts();

        }

        private void desets_item_Click(object sender, EventArgs e)
        {

            sidebars.Top = desets_item.Top;
            sidebars.Height = desets_item.Height;
            currentCategory = "Pastries and Desserts";
            RefreshProducts();
        }

        private void breakfast_item_Click(object sender, EventArgs e)
        {

            sidebars.Top = breakfast_item.Top;
            sidebars.Height = breakfast_item.Height;

            currentCategory = "Breakfast Items";
            RefreshProducts();
        }

        private void sandwiche_item_Click(object sender, EventArgs e)
        {

            sidebars.Top = sandwiche_item.Top;
            sidebars.Height = sandwiche_item.Height;

            currentCategory = "Sandwiches and Wraps";
            RefreshProducts();

        }

        private void salad_item_Click(object sender, EventArgs e)
        {

            sidebars.Top = salad_item.Top;
            sidebars.Height = salad_item.Height;

            currentCategory = "Salads";
            RefreshProducts();
        }

        private void snaks_item_Click(object sender, EventArgs e)
        {

            sidebars.Top = snaks_item.Top;
            sidebars.Height = snaks_item.Height;

            currentCategory = "Snacks";
            RefreshProducts();
        }




        private void search_btn_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void remove_Click(object sender, EventArgs e)
        {
            int curr_idx = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.RemoveAt(curr_idx);

        }

        private void label13_Click(object sender, EventArgs e)
        {

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

        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void track_Click(object sender, EventArgs e)
        {
            try
            {
                int user_ID = userID;


                if (CheckUserIDInTrackTable(user_ID))
                {

                    DialogResult result = MessageBox.Show("Your track number is: " + GetTrackNumber(user_ID) + ". Do you want to track your order?", "Track Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        TrackOrder track = new TrackOrder(name,userID);
                        track.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("You don't have any orders to track.", "No Orders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckUserIDInTrackTable(int userID)
        {
            try
            {
                string query = @"
                          SELECT COUNT(*) 
                          FROM OrderTracking 
                          WHERE UserID = (
                          SELECT UserID 
                          FROM Users 
                          WHERE UserID = @UserID
                           )";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private string GetTrackNumber(int userID)
        {
            try
            {
                string query = @"
                                SELECT TOP 1 TrackingID 
                                 FROM OrderTracking 
                                 WHERE UserID = (
                                 SELECT UserID 
                                 FROM Users 
                                 WHERE UserID = @UserID )
                                 ORDER BY TrackingID DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);

                    return command.ExecuteScalar()?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }

        public class Order
        {
            private List<customerProduct> orderItems;
            private decimal totalPrice;
            private int totalQuantity;

            public Order()
            {
                orderItems = new List<customerProduct>();
                totalPrice = 0;
                totalQuantity = 0;
            }

            public void AddProduct(customerProduct product)
            {
                orderItems.Add(product);
            }

            public void AddTotalPrice(decimal price)
            {
                totalPrice += price;
            }

            public void AddTotalQuantity(int quantity)
            {
                totalQuantity += quantity;
            }

            public decimal GetTotalPrice()
            {
                return totalPrice;
            }

            public int GetTotalQuantity()
            {
                return totalQuantity;
            }

            public List<customerProduct> GetOrderItems()
            {
                return orderItems;
            }
        }

        public class customerProduct
        {
            public int ProductID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string ImageURL { get; set; }
            public int Quantity { get; set; }
            public string Category { get; set; }
            public string Type { get; set; }

            public customerProduct(int id, string name, decimal price, string imageURL, string category, string type)
            {
                ProductID = id;
                Name = name;
                Price = price;
                ImageURL = imageURL;
                Quantity = 0;
                Category = category;
                Type = type;
            }
        }
}
