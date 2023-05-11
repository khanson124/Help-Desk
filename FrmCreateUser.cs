using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Help_Desk
{
    public partial class FrmCreateUserAdmin : Form
    {
        public int CurrentUserID { get; set; }
        private MainContainer mainContainer;
        public FrmCreateUserAdmin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SaveDataToDatabase();
        }

        private void SaveDataToDatabase()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Assuming txtConfirmPassword is your Confirm Password TextBox
                    if (txtPassword.Text != txtReenterPass.Text)
                    {
                        MessageBox.Show("Passwords do not match!");
                        return;
                    }

                    string name = txtName.Text;
                    string[] nameParts = name.Split(' ');
                    if (nameParts.Length < 2)
                    {
                        MessageBox.Show("Name must include both first and last name!");
                        return;
                    }

                    string emailBase = $"{nameParts[0].ToLower()}{nameParts[1].ToLower()}@xyz.com";
                    string email = emailBase;
                    string password = "Company123";

                    // Checking if the email already exists
                    int counter = 1;
                    string checkSql = "SELECT COUNT(*) FROM Users WHERE email = @email";
                    using (SqlCommand cmd = new SqlCommand(checkSql, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        int count = (int)cmd.ExecuteScalar();
                        while (count > 0)
                        {
                            email = emailBase + counter.ToString();
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@email", email);
                            count = (int)cmd.ExecuteScalar();
                            counter++;
                        }
                    }

                    string sql = "INSERT INTO Users (name, email, phone_number, password, role) VALUES (@name, @email, @phone_number, @password, @role)";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone_number", txtPhoneNumber.Text);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@role", txtRole.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("User created successfully.");
                    ClearTextBoxes();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        // SQL Error Code 2627 will be thrown when there's violation of unique constraint
                        // i.e., attempting to insert a duplicate key
                        MessageBox.Show("User already exists.");
                    }
                    else
                    {
                        // Re-throw the exception if it's not about duplicate key
                        throw;
                    }
                }
            }
        }


        private void ClearTextBoxes()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPhoneNumber.Clear();
            txtPassword.Clear();
            txtReenterPass.Clear();
            txtRole.Clear();
        }



        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                int userId;
                if (!int.TryParse(txtIdNumber.Text, out userId))
                {
                    MessageBox.Show("Please enter a valid user id.");
                    return;
                }

                string sql = "SELECT user_id, name, email, phone_number, role FROM Users WHERE user_id = @userId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtIdNumber.Text = reader["user_id"].ToString();
                            txtName.Text = reader["name"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            txtPhoneNumber.Text = reader["phone_number"].ToString();
                            txtRole.Text = reader["role"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No user found with this user id.");
                        }
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                int userId;
                if (!int.TryParse(txtIdNumber.Text, out userId))
                {
                    MessageBox.Show("Please enter a valid user id.");
                    return;
                }

                string sql = "UPDATE Users SET name = @name, email = @email, phone_number = @phoneNumber, role = @role WHERE user_id = @userId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@phoneNumber", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@role", txtRole.Text);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User information updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No user found with this user id.");
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Assuming that you have a TextBox named txtUserId for entering the user id
                int userId;
                if (!int.TryParse(txtIdNumber.Text, out userId))
                {
                    MessageBox.Show("Please enter a valid user id.");
                    return;
                }

                string sql = "DELETE FROM Users WHERE user_id = @userId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No user found with this user id.");
                    }
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard dashboard = new AdminDashboard(this.mainContainer);  // use class level mainContainer directly
            dashboard.CurrentUserID = this.CurrentUserID;
            this.mainContainer.ShowFormInPanel(dashboard);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}


