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
    public partial class Delivered_order : Form
    {
        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        string username;
        public Delivered_order(string username)
        {
            this.username = username;
            InitializeComponent();
            LoadWeeklyPendingOrders();
        }

        private void Delivered_order_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            order_management manage_order = new order_management(username);
            bar.Top = manage_order.Top;
            bar.Height = manage_order.Height;
            manage_order.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pending_order pen = new Pending_order(username);
            bar.Top = pen.Top;
            bar.Height = pen.Height;
            pen.Show();
            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            In_progress_order prog = new In_progress_order(username);
            bar.Top = prog.Top;
            bar.Height = prog.Height;
            prog.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Delivered_order del = new Delivered_order(username);
            del.Show();
            bar.Top = del.Top;
            bar.Height = del.Height;
            this.Hide();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    LoadWeeklyPendingOrders();
                    break;
                case 1:
                    LoadMonthlyPendingOrders();
                    break;
                case 2:
                    LoadDailyPendingOrders();
                    break;
                default:
                    break;
            }
        }
        private void LoadWeeklyPendingOrders()
        {
            string query = @"
                        SELECT *, DATEPART(week, OrderDate) AS WeekNumber 
                        FROM Orders 
                        WHERE Status = 'Delivered' 
                        AND DATEPART(week, OrderDate) = (
                        SELECT DATEPART(week, GETDATE())
                           )";
            LoadPendingOrders(query, dataGridView3);
        }

        private void LoadMonthlyPendingOrders()
        {
            string query = @"
                    SELECT *, DATEPART(month, OrderDate) AS MonthNumber 
                    FROM Orders 
                    WHERE Status = 'Delivered' 
                    AND DATEPART(month, OrderDate) = (
                    SELECT DATEPART(month, GETDATE())
                        )";
            LoadPendingOrders(query, dataGridView2);
        }

        private void LoadDailyPendingOrders()
        {
            string query = @"
                    SELECT *, CONVERT(DATE, OrderDate) AS OrderDateOnly 
                    FROM Orders 
                    WHERE Status = 'Delivered' 
                    AND CONVERT(DATE, OrderDate) = (
                    SELECT CONVERT(DATE, GETDATE())
                        )";
            LoadPendingOrders(query, dataGridView1);
        }



        private void LoadPendingOrders(string query, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView.DataSource = dataTable;


                    total_order.Text = "Total Orders: " + dataTable.Rows.Count.ToString();



                    int pendingCount = dataTable.Select("Status = 'Pending'").Length;
                    int inProgressCount = dataTable.Select("Status = 'In Progress'").Length;
                    int deliveredCount = dataTable.Select("Status = 'Delivered'").Length;

                    no_pen.Text = "Pending Orders: " + pendingCount.ToString();
                    no_prog.Text = "In Progress Orders: " + inProgressCount.ToString();
                    no_del.Text = "Delivered Orders: " + deliveredCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading pending orders: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin(username);
            admin.Show();
            this.Hide();



        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
