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
using LibraryHelpDesk;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Help_Desk
{
    public partial class FrmFileTicket : Form
    {
        public int CurrentUserID { get; set; }
        public FrmFileTicket()
        {
            InitializeComponent();
            LoadCategories();
           
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
          SaveDataToDatabase();
        }
   

        private void btnChart_Click(object sender, EventArgs e)
        {

        }

        private void btnTicket_Click_1(object sender, EventArgs e)
        {
            /*TicketsView frmTicket = new TicketsView();
            this.MdiParent = this.MdiParent;*/
            TicketsView frmTicket = new TicketsView((MainContainer)this.MdiParent);
            this.MdiParent = this.MdiParent;
            frmTicket.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnKnowlegeBase_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

        }

        private void btnProfilePic_Click(object sender, EventArgs e)
        {

        }

        private void btnAlert_Click(object sender, EventArgs e)
        {

        }

        private void btnMessages_Click(object sender, EventArgs e)
        {

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void SaveDataToDatabase()
        {
            if (cmbRequest1.SelectedItem == null || cmbSeverity.SelectedItem == null)
            {
                MessageBox.Show("Please select a request type and severity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            HelpDesk.ProblemCategory category = (HelpDesk.ProblemCategory)Enum.Parse(typeof(HelpDesk.ProblemCategory), cmbRequest1.SelectedItem.ToString());
            HelpDesk.ProblemSeverity severity = (HelpDesk.ProblemSeverity)Enum.Parse(typeof(HelpDesk.ProblemSeverity), cmbSeverity.SelectedItem.ToString());
            HelpDesk.PriorityLevel priority = HelpDesk.PriorityHelper.DeterminePriority(category, severity);
            TicketStatus status = TicketStatus.Pending;

            int agentId = HelpDesk.GetNextSupportAgentId();

            Ticket ticket = new Ticket
            {
                Subject = txtSubject.Text,
                Description = txtDescription.Text,
                Priority = (PriorityLevel)priority,
                Status = status,
                AgentID = agentId,
                UserID = CurrentUserID,
                Created = DateTime.Now,
                DueDate = DateTime.Now

            };



            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int category_id = Convert.ToInt32(cmbCategory.SelectedValue);
                connection.Open();

                string sql = "INSERT INTO Tickets (subject, description, priority, status, agent_id, user_id, date_created, date_updated, category_id) VALUES (@subject, @description, @priority, @status, @agent_id, @user_id, @date_created, @date_updated, @category_id)";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@subject", txtSubject.Text);
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@priority", priority.ToString());
                    cmd.Parameters.AddWithValue("@agent_id", agentId);
                    cmd.Parameters.AddWithValue("@status", status.ToString());
                    cmd.Parameters.AddWithValue("@user_id", 1);
                    cmd.Parameters.AddWithValue("@date_created", ticket.Created);
                    cmd.Parameters.AddWithValue("@date_updated", ticket.DueDate);
                    cmd.Parameters.AddWithValue("@category_id", category_id);


                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            MessageBox.Show("Ticket saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Close form
            //this.Close();
        }

        private void LoadCategories()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            string queryCategory = "SELECT category_id, category_name FROM Categories";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryCategory, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable categories = new DataTable();

                    adapter.Fill(categories);

                    cmbCategory.DataSource = categories;

                    cmbCategory.ValueMember = "category_id";

                    cmbCategory.DisplayMember = "category_name";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading categories: " + ex.Message);
                }
            }
        }
    }
}
