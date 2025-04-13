using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS
{
    public partial class Menu_items : Form
    {
        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        private string currentCategory = "All Items";
        string username;
        public Menu_items(string username)
        {
            this.username = username;
            InitializeComponent();
            LoadProductsFromDatabase();
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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
                                  "INNER JOIN Subcategory sc ON c.CategoryID = sc.CategoryID" +
                                  "WHERE c.CategoryName = @Category OR @Category = 'All Items'";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Category", currentCategory);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int productID = Convert.ToInt32(reader["ProductID"]);
                        string name = reader["ProductName"].ToString();
                        decimal price = Convert.ToDecimal(reader["Price"]);
                        string imageURL = reader["Image"].ToString(); 
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
            productPanel.Size = new Size(250, 240);
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
                    pictureBox.Size = new Size(250, 120);
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




            flowLayoutPanel2.Controls.Add(productPanel);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint_1(object sender, PaintEventArgs e)
        {

        }


        private void RefreshProducts()
        {
            flowLayoutPanel2.Controls.Clear();
            LoadProductsFromDatabase();
        }

     
        private void All_items_Click(object sender, EventArgs e)
        {
            currentCategory = "All Items";
            RefreshProducts();
        }

        private void berage_item_Click(object sender, EventArgs e)
        {
            currentCategory = "Beverages";
            RefreshProducts();
        }

        private void desserts_Click(object sender, EventArgs e)
        {
            currentCategory = "Pastries and Desserts";
            RefreshProducts();
        }

        private void sanwiches_Click(object sender, EventArgs e)
        {
            currentCategory = "Sandwiches and Wraps";
            RefreshProducts();
        }

        private void breakfast_Click(object sender, EventArgs e)
        {
            currentCategory = "Breakfast Items";
            RefreshProducts();

        }

        private void salad_Click(object sender, EventArgs e)
        {
            currentCategory = "Salads";
            RefreshProducts();

        }

        private void snak_Click(object sender, EventArgs e)
        {
            currentCategory = "Snacks";
            RefreshProducts();
        }

        private void Menu_items_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin(username);
            admin.Show();
            this.Hide();
            
        }
    }
}
