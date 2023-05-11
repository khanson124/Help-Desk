using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LibraryHelpDesk
{
    public class HelpDesk
    {
        /*      public enum TicketStatus
              {
                  Pending,
                  InProgress,
                  Resolved,
                  Closed
              }*/
        public enum ProblemCategory
        {
            Hardware,
            Software,
            Network,
            Other
        }

        public enum ProblemSeverity
        {
            Low,
            Medium,
            High
        }

        public enum PriorityLevel
        {
            Low,
            Medium,
            High,
            Urgent
        }

        public class PriorityHelper
        {
            public static PriorityLevel DeterminePriority(ProblemCategory category, ProblemSeverity severity)
            {
                // Default priority level
                PriorityLevel priority = PriorityLevel.Low;

                // Determine priority based on category and severity
                switch (category)
                {
                    case ProblemCategory.Hardware:
                        if (severity == ProblemSeverity.High)
                        {
                            priority = PriorityLevel.Urgent;
                        }
                        else if (severity == ProblemSeverity.Medium)
                        {
                            priority = PriorityLevel.High;
                        }
                        else
                        {
                            priority = PriorityLevel.Medium;
                        }
                        break;
                    case ProblemCategory.Software:
                    case ProblemCategory.Network:
                        if (severity == ProblemSeverity.High)
                        {
                            priority = PriorityLevel.High;
                        }
                        else if (severity == ProblemSeverity.Medium)
                        {
                            priority = PriorityLevel.Medium;
                        }
                        else
                        {
                            priority = PriorityLevel.Low;
                        }
                        break;
                    case ProblemCategory.Other:
                        if (severity == ProblemSeverity.High)
                        {
                            priority = PriorityLevel.Medium;
                        }
                        else if (severity == ProblemSeverity.Medium)
                        {
                            priority = PriorityLevel.Low;
                        }
                        else
                        {
                            priority = PriorityLevel.Low;
                        }
                        break;
                    default:
                        break;
                }

                return priority;
            }
        }


        public static int GetNextSupportAgentId()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";
            List<int> agentIds = new List<int>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT user_id FROM Users WHERE role = @role";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@role", "support");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int agentId = reader.GetInt32(0);
                            agentIds.Add(agentId);
                        }
                    }
                }
            }

            if (agentIds.Count == 0)
            {
                throw new InvalidOperationException("No support agents found in the database.");
            }

            // Use a simple round-robin approach to select the next agent ID
            int nextAgentIdIndex = DateTime.Now.Millisecond % agentIds.Count;
            return agentIds[nextAgentIdIndex];
        }

    }
}
