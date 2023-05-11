using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Help_Desk
{
    public partial class FrmIncident : Form
    {
        public FrmIncident()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRequestType = cmbRequest1.SelectedItem.ToString();
            cmbRequest2.Items.Clear();

            switch (selectedRequestType)
            {
                case "Hardware":
                    cmbRequest2.Items.Add("Computer won't turn on");
                    cmbRequest2.Items.Add("Monitor won't display an image");
                    cmbRequest2.Items.Add("Printer won't print");
                    cmbRequest2.Items.Add("Keyboard or mouse isn't working");
                    break;
                case "Software":
                    cmbRequest2.Items.Add("Application won't launch");
                    cmbRequest2.Items.Add("Error message appears when using an application");
                    cmbRequest2.Items.Add("File won't open or save");
                    cmbRequest2.Items.Add("Application is running slowly");
                    break;
                case "Network":
                    cmbRequest2.Items.Add("Unable to connect to the internet");
                    cmbRequest2.Items.Add("Slow internet speeds");
                    cmbRequest2.Items.Add("Can't access network resources");
                    cmbRequest2.Items.Add("Firewall is blocking access to a website or application");
                    break;
                case "Other":
                    cmbRequest2.Items.Add("Account locked out or password reset needed");
                    cmbRequest2.Items.Add("Request for new software to be installed");
                    cmbRequest2.Items.Add("General IT questions or inquiries");
                    cmbRequest2.Items.Add("Request for new hardware to be purchased");
                    break;
                default:
                    break;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
