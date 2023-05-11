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
    public partial class FrmKnowledgeBase : Form
    {
        public FrmKnowledgeBase()
        {
            InitializeComponent();
        }

        private void txtSearchKB_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Replace with the actual SQL Server connection string
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HELPDESK;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT content FROM Knowledge_Base WHERE title LIKE @title";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@title", "%" + txtSearchKB.Text + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewKB.DataSource = dt;
                        dataGridViewKB.Columns[0].HeaderText = "Content";
                        dataGridViewKB.Columns["Content"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


                    }
                }
            }
        }

        private void knowledgeBaseArticleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
