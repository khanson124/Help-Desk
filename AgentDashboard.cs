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
    public partial class AgentDashboard : Form
    {
        public int CurrentUserID { get; set; }
        private MainContainer mainContainer;

        public AgentDashboard(MainContainer mainContainer)
        {
            InitializeComponent();
            this.mainContainer = mainContainer;
        }

        private void btnDashboardTicket_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmFileTicket tickets = new FrmFileTicket();
            tickets.CurrentUserID = this.CurrentUserID;
            mainContainer.ShowFormInPanel(tickets);
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
    }
}
