using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS
{
    internal class ProductManager
    {

        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        public List<Product> Products { get; set; } = new List<Product>();

        public List<Product> LoadProducts()
        {
            Products.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT p.*, c.CategoryID " +
                                   "FROM Product p " +
                                   "INNER JOIN Category c ON p.CategoryID = c.CategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Product product = new Product();

                            product.ProductID = Convert.ToInt32(reader["ProductID"]);
                            product.ProductName = reader["ProductName"].ToString();
                            product.Price = Convert.ToDecimal(reader["Price"]);
                            product.Stock = reader["StockCategory"].ToString();
                            product.Type = reader["Type"].ToString();
                            product.Status = reader["Status"].ToString();
                            product.InsertDate = reader["InsertDate"] != DBNull.Value ? Convert.ToDateTime(reader["InsertDate"]) : DateTime.Now;
                            product.UpdateDate = reader["UpdateDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdateDate"]) : DateTime.Now;
                            product.Image = reader["Image"].ToString();
                            product.CategoryID = Convert.ToInt32(reader["CategoryID"]); 

                            Products.Add(product);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return Products;
        }


    }
}




public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Stock { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string Image { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public int CategoryID { get; set; } // Add CategoryID property

}