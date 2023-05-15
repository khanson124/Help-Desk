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

namespace Help_Desk
{
    public partial class TicketHistory : Form
    {
        public int CurrentUserID { get; set; }
        public string CurrentUserRole { get; set; }
        private List<Ticket> tickets;
        private MainContainer mainContainer;
        public TicketHistory()
        {
            InitializeComponent();
            LoadClosedTickets();
            FetchTicketData(CurrentUserID);
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
                string sql = "SELECT * FROM Tickets WHERE status = 'Closed'";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Bind the data to your DataGridView
                        dataGridViewTicketHistory.DataSource = dataTable;
                    }
                }

                //string sql = "SELECT ticket_id, user_id, agent_id, priority, status, subject, description, date_created, date_updated FROM Tickets";

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

        private void LoadClosedTickets()
        {
            string connectionString = "Data Source =.\\SQLEXPRESS; Initial Catalog = HELPDESK; Integrated Security = True"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

             
                string sql = "SELECT * FROM Tickets WHERE status = 'Closed'";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Bind the data to your DataGridView
                        dataGridViewTicketHistory.DataSource = dataTable;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
