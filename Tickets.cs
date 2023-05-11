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

namespace Help_Desk
{
    public partial class Tickets : Form
    {
        private List<Ticket> tickets;

        public Tickets()
        {
            InitializeComponent();
            tickets = new List<Ticket>();
            InitializeDataGridView();
        }

        private void dataGridViewTickets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void InitializeDataGridView()
        {
            dataGridViewTickets.DataSource = null;
            dataGridViewTickets.AutoGenerateColumns = false;
            dataGridViewTickets.ColumnCount = 5;

            dataGridViewTickets.Columns[0].Name = "TicketId";
            dataGridViewTickets.Columns[0].HeaderText = "Ticket ID";
            dataGridViewTickets.Columns[0].DataPropertyName = "TicketId";

            dataGridViewTickets.Columns[1].Name = "Title";
            dataGridViewTickets.Columns[1].HeaderText = "Title";
            dataGridViewTickets.Columns[1].DataPropertyName = "Title";

            dataGridViewTickets.Columns[2].Name = "Priority";
            dataGridViewTickets.Columns[2].HeaderText = "Priority";
            dataGridViewTickets.Columns[2].DataPropertyName = "Priority";

            dataGridViewTickets.Columns[3].Name = "Status";
            dataGridViewTickets.Columns[3].HeaderText = "Status";
            dataGridViewTickets.Columns[3].DataPropertyName = "Status";

            dataGridViewTickets.Columns[4].Name = "AssignTo";
            dataGridViewTickets.Columns[4].HeaderText = "Assigned To";
            dataGridViewTickets.Columns[4].DataPropertyName = "AssignTo";

            dataGridViewTickets.Columns[4].Name = "Date Created";
            dataGridViewTickets.Columns[4].HeaderText = "Date Created";
            dataGridViewTickets.Columns[4].DataPropertyName = "Date Created";

            dataGridViewTickets.Columns[4].Name = "Date Updated";
            dataGridViewTickets.Columns[4].HeaderText = "Date Updated";
            dataGridViewTickets.Columns[4].DataPropertyName = "Date Updated";

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            dataGridViewTickets.DataSource = null;
            dataGridViewTickets.DataSource = tickets;
        }

        private void buttonAddTicket_Click(object sender, EventArgs e)
        {
            // You can create a new form to collect the ticket data or use input dialogs.
            // For simplicity, we'll use input dialogs to gather ticket information.

            string title = PromptUserForInput("Enter ticket title:");
            string description = PromptUserForInput("Enter ticket description:");
            string priorityStr = PromptUserForInput("Enter ticket priority (Low, Medium, High, Urgent):");
            string agentID = PromptUserForInput("Enter the name of the person the ticket is assigned to:");
            string userID = PromptUserForInput("Enter the name of the person who requested the ticket:");

            if (!Enum.TryParse(priorityStr, out PriorityLevel priority))
            {
                MessageBox.Show("Invalid priority level. Ticket not created.");
                return;
            }

            int newTicketId = tickets.Count + 1;
            DateTime created = DateTime.Now;

            Ticket newTicket = new Ticket(newTicketId, title, description, priority, TicketStatus.Pending, agentID, userID, created, created);
            newTicket.CalculateDueDate();
            tickets.Add(newTicket);

            RefreshDataGridView();
        }

        private string PromptUserForInput(string prompt)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(prompt, "Enter Data");
        }

        private void btnCreateTicket_Click(object sender, EventArgs e)
        {

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

    }
}

