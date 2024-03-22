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

namespace society_management_system
{
    public partial class edit_info : Form
    {
        SqlConnection connection_string = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        private string loggedInUsername;
        public edit_info(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private void edit_info_Load(object sender, EventArgs e)
        {
            string username = loggedInUsername;
            string query = "SELECT * FROM users WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        username_box.Text = reader["username"].ToString();
                        password_box.Text = reader["password"].ToString();
                        name_box.Text = reader["name"].ToString();
                        role_box.Text = reader["role"].ToString();
                        batch_box.Text = reader["batch"].ToString();
                        degree_box.Text = reader["degree"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching user information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
