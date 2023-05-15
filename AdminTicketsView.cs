 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using LibraryHelpDesk;

namespace Help_Desk
{
    public partial class AdminTicketsView : Form
    {
        public int CurrentUserID { get; set; }
        public string CurrentUserRole { get; set; }
        private List<Ticket> tickets;
        private MainContainer mainContainer;

        public AdminTicketsView(MainContainer mainContainer, int userID, string userRole)
        {
            InitializeComponent();
            this.mainContainer = mainContainer;
           this.CurrentUserID = userID;
            this.CurrentUserRole = userRole;

            (this.CurrentUserID, this.CurrentUserRole) = (userID, userRole);
            this.Load += TicketsView_Load;

            this.tickets = FetchTicketData(CurrentUserID);  // fetch tickets from database

            InitializeDataGridView();

         
        }

        private void dataGridViewTicketHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void InitializeDataGridView()
        {
            dataGridViewTicketHistory.DataSource = null;
            dataGridViewTicketHistory.AutoGenerateColumns = false;
            dataGridViewTicketHistory.ColumnCount = 7;

            dataGridViewTicketHistory.Columns[0].Name = "TicketId";
            dataGridViewTicketHistory.Columns[0].HeaderText = "Ticket ID";
            dataGridViewTicketHistory.Columns[0].DataPropertyName = "TicketId";

            dataGridViewTicketHistory.Columns[1].Name = "UserID";
            dataGridViewTicketHistory.Columns[1].HeaderText = "User ID";
            dataGridViewTicketHistory.Columns[1].DataPropertyName = "UserID";

            dataGridViewTicketHistory.Columns[2].DataPropertyName = "AgentId";
            dataGridViewTicketHistory.Columns[2].HeaderText = "Assigned To";
            dataGridViewTicketHistory.Columns[2].DataPropertyName = "AgentId";

            dataGridViewTicketHistory.Columns[3].Name = "Priority";
            dataGridViewTicketHistory.Columns[3].HeaderText = "Priority";
            dataGridViewTicketHistory.Columns[3].DataPropertyName = "Priority";

            dataGridViewTicketHistory.Columns[4].Name = "Status";
            dataGridViewTicketHistory.Columns[4].HeaderText = "Status";
            dataGridViewTicketHistory.Columns[4].DataPropertyName = "Status";

            dataGridViewTicketHistory.Columns[5].Name = "Subject";
            dataGridViewTicketHistory.Columns[5].HeaderText = "Subject";
            dataGridViewTicketHistory.Columns[5].DataPropertyName = "Subject";

            dataGridViewTicketHistory.Columns[6].Name = "Created";
            dataGridViewTicketHistory.Columns[6].HeaderText = "Date Created";
            dataGridViewTicketHistory.Columns[6].DataPropertyName = "Created";


            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            dataGridViewTicketHistory.DataSource = null;
            dataGridViewTicketHistory.DataSource = tickets;
        }

        private List<Ticket> FetchTicketData(int userID)
        {
            List<Ticket> tickets = new List<Ticket>();
            string userRole = "";

            string connectionString = "Data Source =.\\SQLEXPRESS; Initial Catalog = HELPDESK; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to fetch the role of the user
                string roleSql = "SELECT role FROM Users WHERE user_id = @userId";

                using (SqlCommand roleCmd = new SqlCommand(roleSql, connection))
                {
                    roleCmd.Parameters.AddWithValue("@userId", userID);

                    using (SqlDataReader reader = roleCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userRole = reader.GetString(0);
                        }
                    }
                }

                string sql = "SELECT ticket_id, user_id, agent_id, priority, status, subject, description, date_created, date_updated FROM Tickets";

                // If the user is an agent, add a WHERE clause to only get their tickets
                if (userRole == "agent")
                {
                    sql += " WHERE user_id = @userId";
                }
                // If the user is a user, add a WHERE clause to only get their tickets
                else if (userRole == "support")
                {
                    sql += " WHERE agent_id = @userId";
                }
                // If user is an admin, we do not need to add a WHERE clause as they can see all tickets.

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // If the user is an agent or a user, add the parameter to the query
                    if (userRole == "agent" || userRole == "support")
                    {
                        cmd.Parameters.AddWithValue("@userId", userID);
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ticketId = reader.GetInt32(0);
                            int user_ID = reader.GetInt32(1);
                            int agentID = reader.GetInt32(2);
                            PriorityLevel priority = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), reader.GetString(3));
                            TicketStatus status = (TicketStatus)Enum.Parse(typeof(TicketStatus), reader.GetString(4));
                            string subject = reader.GetString(5);
                            string description = reader.GetString(6);
                            DateTime dateCreated = reader.GetDateTime(7);
                            DateTime dateUpdated = reader.GetDateTime(8);

                            Ticket ticket = new Ticket(ticketId, subject, description, priority, status, agentID, user_ID, dateCreated, dateUpdated);
                            tickets.Add(ticket);
                        }
                    }
                }
            }

            return tickets;
        }

 



        private string PromptUserForInput(string prompt)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(prompt, "Enter Data");
        }

        private void txtSearchTicket_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchTicket.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                RefreshDataGridView(); // Reset the DataGridView to display all tickets
            }
            else
            {
                var filteredTickets = tickets
                    .Where(ticket => ticket.Subject.ToLower().Contains(searchQuery) || ticket.Description.ToLower().Contains(searchQuery))
                    .ToList();

                dataGridViewTicketHistory.DataSource = null;
                dataGridViewTicketHistory.DataSource = filteredTickets;
            }
        }

        private void ticketToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            FrmFileTicket frmFileTicket = new FrmFileTicket();
            frmFileTicket.CurrentUserID = this.CurrentUserID;
            this.mainContainer.ShowFormInPanel(frmFileTicket);  // use class level mainContainer directly
            this.Hide();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
 
                this.Hide();
                AdminDashboard dashboard = new AdminDashboard(this.mainContainer);  // use class level mainContainer directly
                dashboard.CurrentUserID = this.CurrentUserID;
                this.mainContainer.ShowFormInPanel(dashboard);
     
        }
        private void TicketsView_Load(object sender, EventArgs e)
        {

            // Disable the menu option for agents
            if (this.CurrentUserRole == "agent")
            {
                updateTicketToolStripMenuItem.Enabled = false;
            }
        }


        private void updateTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            // Get the selected row
            if (dataGridViewTicketHistory.CurrentRow != null)
            {
                // Get the ticket from the selected row
                Ticket selectedTicket = (Ticket)dataGridViewTicketHistory.CurrentRow.DataBoundItem;

                // Prompt the user for the new status
                string newStatusString = PromptUserForInput("Enter new status");

                // Parse the new status, assuming status is an enum
                if (Enum.TryParse(newStatusString, out TicketStatus newStatus))
                {
                    // Set the new status
                    selectedTicket.Status = newStatus;

                    // Update in the database
                    UpdateTicketInDatabase(selectedTicket);

                    // Refresh the DataGridView
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Invalid status entered");
                }
            }
            else
            {
                MessageBox.Show("No ticket selected");
            }
        }

        // This method updates the status of a ticket in the database
        private void UpdateTicketInDatabase(Ticket ticket)
        {
            string connectionString = "Data Source =.\\SQLEXPRESS; Initial Catalog = HELPDESK; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE Tickets SET status = @Status WHERE ticket_id = @TicketId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", ticket.Status.ToString());
                    cmd.Parameters.AddWithValue("@TicketId", ticket.TicketId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MainContainer mainContainer = (MainContainer)this.ParentForm;

            mainContainer.Close();

            this.Close();

            Login login = new Login();
            login.Show();
        }

        private void ticketHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TicketHistory ticketHistory = new TicketHistory();
            ticketHistory.CurrentUserID = this.CurrentUserID;
            this.mainContainer.ShowFormInPanel(ticketHistory);  // use class level mainContainer directly
            this.Hide();
        }
    }
}

