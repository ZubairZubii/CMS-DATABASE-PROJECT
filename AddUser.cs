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
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CMS
{
    public partial class AddUser : Form
    {

        string connectionString = "Data Source = LAPTOP-8V6R24A5\\SQLEXPRESS; Initial Catalog=CAFE_MANAGEMENT_SYSTEM; Integrated Security = True; Encrypt=false";
        string userName;
        public AddUser(string userName)
        {
            this.userName = userName;   
            InitializeComponent();
            InitializeDataGridView();
            userDisplay();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Add("UserID", "User ID");
            dataGridView1.Columns.Add("Username", "Username");
            dataGridView1.Columns.Add("Password", "Password");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("Role", "Role");
            dataGridView1.Columns.Add("Status", "Status");
            dataGridView1.Columns.Add("ProfileImage", "Profile Image");
            dataGridView1.Columns.Add("RegistrationDate", "Registration Date");
        }

        public void userDisplay()
        {
            UserManager userManager = new UserManager();
            List<User> Users = userManager.LoadUsers();

            dataGridView1.Rows.Clear(); 

            foreach (var user in Users)
            {
                dataGridView1.Rows.Add(
                    user.UserID,
                    user.Username,
                    user.Password,
                    user.Email,
                    user.Role,
                    user.Status,
                    user.ProfileImage,
                    user.RegistrationDate.ToString("yyyy-MM-dd")
                );
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void show_Click(object sender, EventArgs e)
        {


        }

        private void Label13_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Exit the application
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                return;
            }
        }

        public bool UserExists(string username)
        {
            string query = "SELECT Username FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    connection.Open();

                    SqlDataReader reader  =  cmd.ExecuteReader();
                    // Check if any rows are returned
                    if (reader.Read())
                    {
                        return true;


                    }

                 return false;  
                }
            }
        }

        private string image_path = "";

        public void AddUserdata(string username, string password, string role, string status,string image)
        {
            string query = "INSERT INTO Users (Username, Password, Email, Role, status, profile_image, reg_date) VALUES (@Username, @Password, @Email, @Role, 'Active', @ProfileImage, GETDATE())";
          
            string email = username + "@gmail.com";

          

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ProfileImage", image);
                    connection.Open();

                    // Execute the query to add the user
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string username = name_text_box.Text;
            string password = password_textBox.Text;
            string role = rolebox.Text;
            string status = statusbox.Text;

            if (UserExists(username))
            {
                MessageBox.Show("User already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string image = "";
            if (!string.IsNullOrEmpty(image_path))
            {
                image = image_path; // Set the image path to the selected file path
            }
            else
            {
                MessageBox.Show("Please select an image for the user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Return without adding the user if no image is selected
            }

            AddUserdata(username, password, role, status, image);
            userDisplay();

            name_text_box.Text = "";
            password_textBox.Text = "";
            rolebox.SelectedIndex = -1;
            statusbox.SelectedIndex = -1;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Image Files (*.jpg; *.png) | *.jpg; *.png";
            openFileDialog.Title = "Select an image file";
            
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image_path = openFileDialog.FileName;
                importimage.ImageLocation = image_path;
            }
        }

        private void ClearFields()
        {
            name_text_box.Text = "";
            password_textBox.Text = "";
            rolebox.SelectedIndex = -1;
            statusbox.SelectedIndex = -1;
            importimage.Image = null;
        }
        private void button2_Click(object sender, EventArgs e)
        {


            DataGridViewRow selectedRow = dataGridView1.CurrentRow;

            if (selectedRow == null)
            {
                MessageBox.Show("Please select a user to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string userId = selectedRow.Cells["UserID"].Value.ToString();
            string username = name_text_box.Text;
            string password = password_textBox.Text;
            string role = rolebox.Text;
            string status = statusbox.Text;
            string image = image_path;  

            UpdateUser(userId, username, password, role, status, image);
           

            ClearFields();

            userDisplay();
        }
   
        private void UpdateUser(string userId, string username, string password, string role, string status, string image)
        {
            string query = "UPDATE Users SET Username = @Username, Password = @Password, Role = @Role, Status = @Status, profile_image = @ProfileImage WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ProfileImage", image);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    connection.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
     

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

            name_text_box.Text = selectedRow.Cells["Username"].Value.ToString();
            password_textBox.Text = selectedRow.Cells["Password"].Value.ToString();
            rolebox.Text = selectedRow.Cells["Role"].Value.ToString();
            statusbox.Text = selectedRow.Cells["Status"].Value.ToString();


            image_path = selectedRow.Cells["ProfileImage"].Value.ToString();
            importimage.ImageLocation = image_path;

        }


        private void DeleteUser(string userId)
        {
            string query = "DELETE FROM Users WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    connection.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    
        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow selected = dataGridView1.CurrentRow;
            
            string selectedUserID = selected.Cells["UserID"].Value.ToString();

      
            DeleteUser(selectedUserID);

         
            userDisplay();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Admin admin = new Admin(userName);
            admin.Show();
            this.Hide();    
        }
    }
}
