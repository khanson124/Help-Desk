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
            int userID = ValidateUserAndGetID(txtUsername.Text, txtPassword.Text);
            

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

                                    AdminDashboard frmAdmin = new AdminDashboard(frmMain); // Add the parameter here
                                    frmAdmin.CurrentUserID = userID;
                                    frmMain.ShowFormInPanel(frmAdmin);
                                    this.Hide();
                                    frmMain.ShowDialog();
                                }
                                else if (userRole == "agent")
                                {
                                    MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    AgentDashboard frmAgent = new AgentDashboard(frmMain);
                                    frmAgent.CurrentUserID = userID;
                                    frmMain.ShowFormInPanel(frmAgent);
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
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private int ValidateUserAndGetID(string username, string password)
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
        }

    }
}
