using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace CMS
{
    public partial class product : Form
    {


        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        string username;
        public product( string username)
        {

            this.username = username;
            InitializeComponent();
           
            InitializeDataGridView();
            ProductDisplay();
        }

        private void InitializeDataGridView()
        {
            dataGridView2.Columns.Add("ProductID", "Product ID");
            dataGridView2.Columns.Add("ProductName", "Product Name");
            dataGridView2.Columns.Add("Price", "Price");
            dataGridView2.Columns.Add("Stock", "Stock Category");
            dataGridView2.Columns.Add("Type", "Type");
            dataGridView2.Columns.Add("Status", "Status");
            dataGridView2.Columns.Add("CategoryID", "Category ID"); 
           dataGridView2.Columns.Add("Image", "Image");
            dataGridView2.Columns.Add("InsertDate", "Insert Date");
            dataGridView2.Columns.Add("UpdateDate", "Update Date");
        
        }

        private void ProductDisplay()
        {
            ProductManager prod = new ProductManager();
            List<Product> products = prod.LoadProducts();

            dataGridView2.Rows.Clear();

            foreach (var product in products)
            {
                dataGridView2.Rows.Add(
                    product.ProductID,
                    product.ProductName,
                    product.Price,
                    product.Stock,
                    product.Type,
                    product.Status,
                    product.CategoryID, // Add CategoryID
                    product.Image,
                 
                    product.InsertDate.ToString("yyyy-MM-dd"),
                    product.UpdateDate.ToString("yyyy-MM-dd")
               
                    
                );;
            }
        }

        private string image_path = "";
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void product_Load(object sender, EventArgs e)
        {

        }

        private void name_text_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }



        private void ClearFields()
        {
         
            prod_name.Text = "";
            prod_price.Text = "";
            prod_stock.Text = "";
            prod_type.SelectedIndex = -1;
            prod_status.SelectedIndex = -1;
            image_path = "";
            importimage.Image = null;
            catg_id.Text = "";
        }



        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

            prod_name.Text = selectedRow.Cells["ProductName"].Value.ToString();
            prod_price.Text = selectedRow.Cells["Price"].Value.ToString();
            prod_stock.Text = selectedRow.Cells["Stock"].Value.ToString();
            prod_type.Text = selectedRow.Cells["Type"].Value.ToString();
            prod_status.Text = selectedRow.Cells["Status"].Value.ToString();
            string imageName = selectedRow.Cells["Image"].Value.ToString(); // Assuming image name includes the file extension

            // Construct the full path to the image
            string imagePath = Path.Combine("C:\\Users\\Zubair\\Documents\\CMS\\CMS\\Resources\\", imageName);

            try
            {
                // Check if the image file exists
                if (File.Exists(imagePath))
                {
                    // Attempt to load the image
                    importimage.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // If the file does not exist, show error message
                    MessageBox.Show("Image not found: " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void importimage_Click(object sender, EventArgs e)
        {
      
        }

        private void prod_price_TextChanged(object sender, EventArgs e)
        {

        }


        private void UpdateProduct(int productId, string productName, decimal price, string stock, string type, string status, string image, int categoryID)
        {
            string query = "UPDATE Product SET ProductName = @ProductName, Price = @Price, StockCategory = @Stock, Type = @Type, Status = @Status, UpdateDate = GETDATE(), Image = @Image, CategoryID = @CategoryID WHERE ProductID = @ProductID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@Image", image);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                    connection.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update product!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void DeleteProduct(int productId)
        {
            string query = "DELETE FROM Product WHERE ProductID = @ProductID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    connection.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete product!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ClearFields();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView2.CurrentRow;

            if (selectedRow == null)
            {
                MessageBox.Show("Please select a product to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int productId = Convert.ToInt32(selectedRow.Cells["ProductID"].Value);
            string productName = prod_name.Text;
            decimal price = Convert.ToDecimal(prod_price.Text);
            string stock = (prod_stock.Text);
            string type = prod_type.Text;
            string status = prod_status.Text;
            string image = image_path;

            // Retrieve the selected category ID from the interface
            int categoryID = Convert.ToInt32(catg_id.Text);

            UpdateProduct(productId, productName, price, stock, type, status, image, categoryID);
            ProductDisplay();
            ClearFields();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView2.CurrentRow;

            if (selectedRow == null)
            {
                MessageBox.Show("Please select a product to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int productId = Convert.ToInt32(selectedRow.Cells["ProductID"].Value);

            DeleteProduct(productId);
            ProductDisplay(); 
        }




        private void AddProductData(string productName, decimal price, string stock, string type, string status, string image, int categoryID)
        {
            string query = "INSERT INTO Product (ProductName, Price, StockCategory, Type, Status, CategoryID, InsertDate, UpdateDate, Image) " +
                           "VALUES (@ProductName, @Price, @Stock, @Type, @Status, @CategoryID, GETDATE(), NULL, @Image)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Image", image);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID); // Add CategoryID parameter
                    connection.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ProductDisplay();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
  
           
            string productName = prod_name.Text;
            decimal price = Convert.ToDecimal(prod_price.Text);
            string stock = prod_stock.Text;
            string type = prod_type.Text;
            string status = prod_status.Text;
            int categoryID = Convert.ToInt32(catg_id.Text);

            string image = "";
            if (image_path!="")
            {

                image = Path.GetFileName(image_path);
            }
            else
            {
                MessageBox.Show("Please select an image for the product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            Console.WriteLine("Image URL \tp the database: " + image);

            AddProductData( productName, price, stock, type, status, image, categoryID);
        
            ClearFields();
        }


        private void prod_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void import_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Image Files (*.jpg; *.png) | *.jpg; *.png";
            openFileDialog.Title = "Select an image file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image_path = openFileDialog.FileName;
                importimage.ImageLocation = image_path;
            }
        }

        private void prod_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void prod_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void prod_stock_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Admin  admin = new Admin(username);
            admin.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
