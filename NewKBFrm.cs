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
    public partial class NewKBFrm : Form
    {
        public NewKBFrm()
        {
            InitializeComponent();
            LoadCategories();
            LoadAuthors();
        }

     

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadCategories()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            string queryCategory = "SELECT category_id, category_name FROM Categories";
         

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryCategory, connection);
          
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable categories = new DataTable();

                    adapter.Fill(categories);

                    cmbCategory.DataSource = categories;

                    cmbCategory.ValueMember = "category_id";

                    cmbCategory.DisplayMember = "category_name";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading categories: " + ex.Message);
                }
            }
        }

        private void LoadAuthors()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            string queryAuthor = "SELECT user_id, name FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    SqlCommand commandAuthor = new SqlCommand(queryAuthor, connection);
                    SqlDataAdapter adapterAuthor = new SqlDataAdapter(commandAuthor);


                    DataTable categoriesAuthor = new DataTable();

                    adapterAuthor.Fill(categoriesAuthor);

                    cmbAuthor.DataSource = categoriesAuthor;

                    cmbAuthor.ValueMember = "user_id";

                    cmbAuthor.DisplayMember = "name";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading categories: " + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDataToDatabase();
        }

        private void SaveDataToDatabase()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            string query = "INSERT INTO Knowledge_Base (category_id, title, author, content, date_created, date_updated) VALUES (@category_id, @title, @author, @content, @date_created, @date_updated)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    // Get the selected category ID from the combo box
                    int category_id = Convert.ToInt32(cmbCategory.SelectedValue);
                    int user_id = Convert.ToInt32(cmbCategory.SelectedValue);
                    // Get the text from the input fields (replace "txtTitle" and "txtDescription" with your actual control names)
                    string title = txtTitle.Text;
                    string content = txtDescription.Text;
                   
                    DateTime dateCreated = dtpdateCreated.Value;
                    DateTime dateUpdated = dtpdateUpdated.Value;


                    // Add parameters to the command
                    command.Parameters.AddWithValue("@category_id", category_id);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@content", content);
                    command.Parameters.AddWithValue("@author", user_id);
                    command.Parameters.AddWithValue("@date_created", dateCreated);
                    command.Parameters.AddWithValue("@date_updated", dateUpdated);





                    // Execute the command
                    command.ExecuteNonQuery();

                    MessageBox.Show("Data saved successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving data: " + ex.Message);
                }
            }
        }
    }
}
    

