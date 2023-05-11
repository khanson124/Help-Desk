using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryHelpDesk;

namespace Help_Desk
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public PriorityLevel Priority { get; set; }
        public TicketStatus Status { get; set; }
        public int AgentID { get; set; }
        public int UserID { get; set; }

        public DateTime Created { get; set; }
        public DateTime DueDate { get; set; }

        List<Ticket> tickets = new List<Ticket>();

        public Ticket() { }

        public Ticket(int ticketId, string title, string description, PriorityLevel priority, TicketStatus status, int agentID, int userID, DateTime created, DateTime dueDate)
        {
            TicketId = ticketId;
            Subject = title;
            Description = description;
            Priority = priority;
            Status = status;
            AgentID = agentID;
            UserID = userID;
            Created = created;
            DueDate = dueDate;
        }

        public override string ToString()
        {
            return $"Ticket ID: {TicketId}, Title: {Subject}, Priority: {Priority}, Status: {Status}, Assigned To: {AgentID}, Requester: {UserID}, Created: {Created}, Due Date: {DueDate}";
        }

        public void UpdateTicket(PriorityLevel newPriority, TicketStatus newStatus, int newAssignTo, DateTime newDueDate)
        {
            Priority = newPriority;
            Status = newStatus;
            AgentID = newAssignTo;
            DueDate = newDueDate;
        }

        public void CalculateDueDate()
        {
            int daysToAdd;
            switch (Priority)
            {
                case PriorityLevel.Low:
                    daysToAdd = 7;
                    break;
                case PriorityLevel.Medium:
                    daysToAdd = 5;
                    break;
                case PriorityLevel.High:
                    daysToAdd = 3;
                    break;
                case PriorityLevel.Urgent:
                    daysToAdd = 1;
                    break;
                default:
                    daysToAdd = 0;
                    break;
            }

            DueDate = Created.AddDays(daysToAdd);
        }

        public bool IsOverdue()
        {
            return DateTime.Now > DueDate;
        }

    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Urgent
    }

    public enum TicketStatus
    {
        Pending,
        InProgress,
        Resolved,
        Escalated,
        Closed
    }


}
