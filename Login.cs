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
    public partial class Login : Form
    {
      

        private string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            txtUsername.Focus();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            //int userID = ValidateUserAndGetID(txtUsername.Text, txtPassword.Text);
            (int userID, string role) = ValidateUserAndGetIDAndRole(username, password);



            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT *, role FROM Users WHERE email=@email AND password=@password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", username);
                        command.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                string userRole = "";
                                if (reader.Read())
                                {
                                    userRole = reader["role"].ToString();
                                }

                                MainContainer frmMain = new MainContainer(); // Create a new MainContainer instance

                                if (userRole == "admin")
                                {
                                    MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    AdminDashboard frmAdmin = new AdminDashboard(frmMain);
                                    frmAdmin.CurrentUserID = userID;
                                    frmMain.ShowFormInPanel(frmAdmin);
                                    AdminTicketsView ticketsView = new AdminTicketsView(frmMain, userID, role); // Pass userID here
                                    this.Hide();
                                    frmMain.ShowDialog();
                                }
                                else if (userRole == "agent")
                                {
                                    MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    AgentDashboard frmAgent = new AgentDashboard(frmMain);
                                    frmAgent.CurrentUserID = userID;
                                    frmMain.ShowFormInPanel(frmAgent);
                                    AdminTicketsView ticketsView = new AdminTicketsView(frmMain, userID, userRole);
                                    this.Hide();
                                    frmMain.ShowDialog();


                                }
                                else if (userRole == "support")
                                {
                                    MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    SupportDashboard frmSupport = new SupportDashboard(frmMain);
                                    frmSupport.CurrentUserID = userID;
                                    frmMain.ShowFormInPanel(frmSupport);
                                    AdminTicketsView ticketsView = new AdminTicketsView(frmMain, userID, role); // Pass userID here

                                    this.Hide();
                                    frmMain.ShowDialog();
                                }

                                else
                                {
                                    MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    resetFields();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                resetFields();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.StackTrace + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void resetFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }




        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /*private int ValidateUserAndGetID(string username, string password)
        {
            int userID = -1; // Initialize the user ID to an invalid value

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT user_id FROM users WHERE email = @email AND password = @password";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@email", username);
                    cmd.Parameters.AddWithValue("@password", password); 

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        userID = Convert.ToInt32(result);
                    }
                }
            }

           return userID;
        }*/
        private (int, string) ValidateUserAndGetIDAndRole(string username, string password)
        {
            int userID = -1; // Initialize the user ID to an invalid value
            string role = null; // Initialize the role to null

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT user_id, role FROM users WHERE email = @email AND password = @password";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@email", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userID = reader.GetInt32(0);  // The first column is the user ID
                            role = reader.GetString(1);   // The second column is the role
                        }
                    }
                }
            }

            return (userID, role);
        }




        private User ValidateUserAndGetUser(string username, string password)
        {
            User user = null; // Initialize the user object to null

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT user_id, role FROM users WHERE email = @email AND password = @password";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@email", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string role = reader.GetString(1);

                            user = new User
                            {
                                Id = userId,
                                Role = role
                            };
                        }
                    }
                }
            }

            return user;
        }


    }
}
