using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMS
{
    public partial class colors : Form
    {
        public colors(List<string> color, DateTime dob)
        {

            InitializeComponent();


            int age = DateTime.Now.Year - dob.Year;

            datee.Text = age.ToString();

            MessageBox.Show(age.ToString());
            


            string col = "";

            foreach(string itm in color)
            {
                col += $"\n - {itm}";
            }
            colorl.Text = col;
        }

        private void colors_Load(object sender, EventArgs e)
        {

        }

        private void datee_Click(object sender, EventArgs e)
        {

        }
    }
}
