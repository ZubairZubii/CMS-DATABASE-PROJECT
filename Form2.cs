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
    public partial class Form2 : System.Windows.Forms.Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void Register_btn_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (name_tetx_box.Text == "" ||
                password_textBox.Text == "" ||
                design_comboBox.Text == "")
            {
                MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password AND Role = @Role AND status = 'Active'";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", name_tetx_box.Text);
                        cmd.Parameters.AddWithValue("@Password", password_textBox.Text);
                        cmd.Parameters.AddWithValue("@Role", design_comboBox.Text);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            string role = reader["Role"].ToString();
                            int id  = Convert.ToInt32(reader["UserID"]);

                            MessageBox.Show($"Login successful! Role: {role}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Navigate to the appropriate page based on the user's role
                            switch (role)
                            {
                                case "Manager":
                                    Admin admin = new Admin(name_tetx_box.Text);
                                    admin.Show();
                                    this.Hide();
                                    break;
                                case "Cashier":
                                 
                                    Cashier cashier = new Cashier(name_tetx_box.Text, id);
                                    cashier.Show();
                                    this.Hide();
                                    break;
                                case "Customer":
                                    Customer customer = new Customer(name_tetx_box.Text,id);
                                    customer.Show();
                                    this.Hide();
                                    break;
                               /* case "CafeStaff":
                                    CafeStaff cafeStaff = new CafeStaff(name_tetx_box.Text);
                                    cafeStaff.Show();
                                    this.Hide();
                                    break;*/
                                default:
                                    MessageBox.Show("Invalid Role!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Username/Password/Role or account is inactive!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void design_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
