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
    public partial class TicketsView : Form
    {
        public int CurrentUserID { get; set; }
        private List<Ticket> tickets;
        private MainContainer mainContainer;

        public TicketsView(MainContainer mainContainer)
        {
            InitializeComponent();
            tickets = new List<Ticket>();
            InitializeDataGridView();
            this.mainContainer = mainContainer;
        }

        private void dataGridViewTickets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void InitializeDataGridView()
        {
            dataGridViewTickets.DataSource = null;
            dataGridViewTickets.AutoGenerateColumns = false;
            dataGridViewTickets.ColumnCount = 7;

            dataGridViewTickets.Columns[0].Name = "TicketId";
            dataGridViewTickets.Columns[0].HeaderText = "Ticket ID";
            dataGridViewTickets.Columns[0].DataPropertyName = "ticket_id";

            dataGridViewTickets.Columns[4].Name = "User ID";
            dataGridViewTickets.Columns[4].HeaderText = "User ID";
            dataGridViewTickets.Columns[4].DataPropertyName = "user_id";

            dataGridViewTickets.Columns[4].Name = "AssignTo";
            dataGridViewTickets.Columns[4].HeaderText = "Assigned To";
            dataGridViewTickets.Columns[4].DataPropertyName = "agent_id";

            dataGridViewTickets.Columns[2].Name = "Priority";
            dataGridViewTickets.Columns[2].HeaderText = "Priority";
            dataGridViewTickets.Columns[2].DataPropertyName = "Priority";

            dataGridViewTickets.Columns[3].Name = "Status";
            dataGridViewTickets.Columns[3].HeaderText = "Status";
            dataGridViewTickets.Columns[3].DataPropertyName = "Status";

            dataGridViewTickets.Columns[1].Name = "Subject";
            dataGridViewTickets.Columns[1].HeaderText = "Subject";
            dataGridViewTickets.Columns[1].DataPropertyName = "Subject";

            dataGridViewTickets.Columns[4].Name = "Date Created";
            dataGridViewTickets.Columns[4].HeaderText = "Date Created";
            dataGridViewTickets.Columns[4].DataPropertyName = "date_created";

            dataGridViewTickets.Columns[4].Name = "Date Updated";
            dataGridViewTickets.Columns[4].HeaderText = "Date Updated";
            dataGridViewTickets.Columns[4].DataPropertyName = "date_updated";

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            dataGridViewTickets.DataSource = null;
            dataGridViewTickets.DataSource = tickets;
        }
        /*private List<Ticket> FetchTicketData()
        {
            List<Ticket> tickets = new List<Ticket>();

            string connectionString = "Data Source =.\\SQLEXPRESS; Initial Catalog = HELPDESK; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT ticket_id, user_id, agent_id, priority, status, subject, description,  date_created, date_updated FROM Tickets";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ticketId = reader.GetInt32(0);
                            int userID = reader.GetInt32(1);
                            int agentID = reader.GetInt32(2);
                            PriorityLevel priority = (PriorityLevel)reader.GetInt32(3);
                            TicketStatus status = (TicketStatus)reader.GetInt32(4);
                            string subject = reader.GetString(5);
                            string description = reader.GetString(6);
                            DateTime dateCreated = reader.GetDateTime(7);
                            DateTime dateUpdated = reader.GetDateTime(8);

                            Ticket ticket = new Ticket(ticketId, subject, description, priority, status, agentID, userID, dateCreated, dateUpdated);
                            tickets.Add(ticket);
                        }
                    }
                }
            }

            return tickets;
        }*/



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

                dataGridViewTickets.DataSource = null;
                dataGridViewTickets.DataSource = filteredTickets;
            }
        }

        private void ticketToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           /* FrmFileTicket frmFileTicket = new FrmFileTicket();
            MainContainer mainContainer = (MainContainer)this.ParentForm;
            frmFileTicket.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(frmFileTicket);
            this.Hide();*/

            FrmFileTicket frmFileTicket = new FrmFileTicket();
            frmFileTicket.CurrentUserID = this.CurrentUserID;
            this.mainContainer.ShowFormInPanel(frmFileTicket);  // use class level mainContainer directly
            this.Hide();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            /*this.Hide();
            //AdminDashboard dashboard = new AdminDashboard();
            AdminDashboard dashboard = new AdminDashboard(mainContainer);

            MainContainer mainContainer = (MainContainer)this.ParentForm;
            dashboard.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(dashboard);*/

                this.Hide();
                AdminDashboard dashboard = new AdminDashboard(this.mainContainer);  // use class level mainContainer directly
                dashboard.CurrentUserID = this.CurrentUserID;
                this.mainContainer.ShowFormInPanel(dashboard);
     
        }
    }
}

