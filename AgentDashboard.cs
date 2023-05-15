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
    public partial class AgentDashboard : Form
    {
        public int CurrentUserID { get; set; }
        public string CurrentUserRole { get; set; }
        private MainContainer mainContainer;

        public AgentDashboard(MainContainer mainContainer)
        {
            InitializeComponent();
            this.mainContainer = mainContainer;
            this.Load += new EventHandler(AgentDashboard_Load);

        }

        private void btnDashboardTicket_Click(object sender, EventArgs e)
        {
            this.Hide();
            AgentTicketView ticketsView = new AgentTicketView(mainContainer, this.CurrentUserID, this.CurrentUserRole);
            ticketsView.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(ticketsView);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnKnowledgeBase_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmKnowledgeBase knowledgeBase = new FrmKnowledgeBase();
            mainContainer.ShowFormInPanel(knowledgeBase);
        }

        private void btnProfilePic_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserProfile profile = new UserProfile();
            profile.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(profile);
        }

        private void btnAgentLogout_Click(object sender, EventArgs e)
        {
            MainContainer mainContainer = (MainContainer)this.ParentForm;

            mainContainer.Close();

            this.Close();

            Login login = new Login();
            login.Show();
        }

        private void btnAgentHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            AgentDashboard dashboard = new AgentDashboard(this.mainContainer);  // use class level mainContainer directly
            dashboard.CurrentUserID = this.CurrentUserID;
            this.mainContainer.ShowFormInPanel(dashboard);
        }

        private void AgentDashboard_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source =.\\SQLEXPRESS; Initial Catalog = HELPDESK; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Fetch the count of Pending tickets
                    string sqlPending = "SELECT COUNT(*) FROM Tickets WHERE status = 'Pending'";
                    SqlCommand cmdPending = new SqlCommand(sqlPending, connection);
                    int pendingTickets = (int)cmdPending.ExecuteScalar();
                    lblOpenTicket.Text = $"{pendingTickets}";

                    // Fetch the count of Closed tickets
                    string sqlClosed = "SELECT COUNT(*) FROM Tickets WHERE status = 'Closed'";
                    SqlCommand cmdClosed = new SqlCommand(sqlClosed, connection);
                    int closedTickets = (int)cmdClosed.ExecuteScalar();
                    lblSolvedTicket.Text = $"{closedTickets}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
    }
}
