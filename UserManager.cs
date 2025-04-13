using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS
{
    internal class UserManager
    {


        private string connectionString = "Data Source = LAPTOP-8V6R24A5\\SQLEXPRESS; Initial Catalog=CAFE_MANAGEMENT_SYSTEM; Integrated Security = True; Encrypt=false"; 

        public List<User> Users { get; set; } = new List<User>();

        public List<User> LoadUsers()
        {
            Users.Clear(); 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Users";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            User user = new User();

                            user.UserID = Convert.ToInt32(reader["UserID"]);
                            user.Username = reader["Username"].ToString();
                            user.Password = reader["Password"].ToString();
                            user.Email = reader["Email"].ToString();
                            user.Role = reader["Role"].ToString();
                            user.Status = reader["status"].ToString();
                            user.ProfileImage = reader["profile_image"].ToString();
                            user.RegistrationDate = Convert.ToDateTime(reader["reg_date"]);
                            

                            Users.Add(user);
                        }

                        reader.Close();
                    
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return Users;
        }

      

    }
}



public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Status { get; set; }
    public string ProfileImage { get; set; }
    public DateTime RegistrationDate { get; set; }
}
