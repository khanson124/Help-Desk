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
    public partial class SupportDashboard : Form
    {
        public int CurrentUserID { get; set; }
        public string CurrentUserRole { get; set; }
        private MainContainer mainContainer;
        public SupportDashboard(MainContainer mainContainer)
        {
            InitializeComponent();
            this.mainContainer = mainContainer;
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminTicketsView ticketsView = new AdminTicketsView(mainContainer, this.CurrentUserID, this.CurrentUserRole);
            //MainContainer mainContainer = (MainContainer)this.ParentForm;
            ticketsView.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(ticketsView);
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
            mainContainer.ShowFormInPanel(knowledgeBase);
        }
    }
}
