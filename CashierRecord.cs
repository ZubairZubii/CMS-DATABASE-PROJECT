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
    public partial class CashierRecord : Form
    {
        private string connectionString = "Data Source=LAPTOP-8V6R24A5\\SQLEXPRESS;Initial Catalog=CAFE_MANAGEMENT_SYSTEM;Integrated Security=True;Encrypt=false";

        int id;
        string name;
        public CashierRecord(string name,int id)
        {
            this.name = name;
            this .id = id;  
            InitializeComponent();
            LoadWeeklySalesRecords();
            LoadMonthlySalesRecords();
            LoadDailySalesRecords();
        }

        private void LoadWeeklySalesRecords()
        {
            string query = @"
                         SELECT *, DATEPART(week, SalesDate) AS WeekNumber 
                         FROM SalesRecords 
                          WHERE DATEPART(week, SalesDate) = (
                          SELECT DATEPART(week, GETDATE())
                             )";
            PopulateDataGridView(dataGridView1, query);
        }

        private void LoadMonthlySalesRecords()
        {
            string query = @"
                         SELECT *, DATEPART(month, SalesDate) AS MonthNumber 
                          FROM SalesRecords 
                          WHERE DATEPART(month, SalesDate) = (
                          SELECT DATEPART(month, GETDATE())
                              )";
            PopulateDataGridView(dataGridView2, query);
        }

        private void LoadDailySalesRecords()
        {
            string query = @"
                         SELECT *, CONVERT(DATE, SalesDate) AS SalesDateOnly 
                         FROM SalesRecords 
                         WHERE CONVERT(DATE, SalesDate) = CONVERT(DATE, GETDATE())";
            PopulateDataGridView(dataGridView3, query);
        }

        private void PopulateDataGridView(DataGridView dataGridView, string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CashierRecord_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Cashier cash = new Cashier(name,id);
            cash.Show();
            this.Hide();

        }
    }
}
