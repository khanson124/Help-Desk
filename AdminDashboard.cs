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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Help_Desk
{
    public partial class AdminDashboard : Form
    {
        public int CurrentUserID { get; set; }
        public string CurrentUserRole { get; set; }
        private MainContainer mainContainer;

        public AdminDashboard(MainContainer mainContainer)
        {
            InitializeComponent();
            this.mainContainer = mainContainer;

            this.Load += new EventHandler(AdminDashboard_Load);
        }




        private void DashboardTicket_Click(object sender, EventArgs e)
        {
       
                this.Hide();
                AdminTicketsView ticketsView = new AdminTicketsView(mainContainer, this.CurrentUserID, this.CurrentUserRole);
                //MainContainer mainContainer = (MainContainer)this.ParentForm;
                ticketsView.CurrentUserID = this.CurrentUserID;
                mainContainer.ShowFormInPanel(ticketsView);
            

        }

        private void AdminDashboard_Load(object sender, EventArgs e)
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
                    txtOpenTicket.Text = $"{pendingTickets}";

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


        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();

        }

        private void btnKnowledgeBase_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmKnowledgeBase knowledgeBase = new FrmKnowledgeBase();
            MainContainer mainContainer = (MainContainer)this.ParentForm;
            mainContainer.ShowFormInPanel(knowledgeBase);
        }


        private void btnTicket_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminTicketsView ticketsView = new AdminTicketsView(mainContainer, this.CurrentUserID, this.CurrentUserRole);
         
            //MainContainer mainContainer = (MainContainer)this.ParentForm;
            ticketsView.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(ticketsView);

        }

        private void btnProfilePic_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserProfile profile = new UserProfile();
            profile.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(profile);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCreateUserAdmin newUser = new FrmCreateUserAdmin(mainContainer);
            newUser.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(newUser);
        }

        private void btnAdminHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard dashboard = new AdminDashboard(this.mainContainer);  // use class level mainContainer directly
            dashboard.CurrentUserID = this.CurrentUserID;
            this.mainContainer.ShowFormInPanel(dashboard);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MainContainer mainContainer = (MainContainer)this.ParentForm;

            mainContainer.Close();

            this.Close();

            Login login = new Login();
            login.Show();
        }

        private void btnKnowlegeBase_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmKnowledgeBase knowledgeBase = new FrmKnowledgeBase();
            MainContainer mainContainer = (MainContainer)this.ParentForm;
            mainContainer.ShowFormInPanel(knowledgeBase);
        }
 
    }

}
