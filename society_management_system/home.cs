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
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace society_management_system
{
    public partial class home : Form
    {
        SqlConnection connection_string = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        private string loggedInUsername;
        public home(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            DisplayUserInfo(username);
        }
        private void DisplayUserInfo(string username)
        {
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
                        name_label.Text = reader["name"].ToString();
                        role_label.Text = reader["role"].ToString();
                        username_label.Text = reader["username"].ToString();
                        password_label.Text = reader["password"].ToString();
                        batch_label.Text = reader["batch"].ToString();
                        degree_label.Text = reader["degree"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching user information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void home_btn_Click(object sender, EventArgs e)
        {

        }

        private void home_Load(object sender, EventArgs e)
        {

        }

        private void edit_info_button_Click(object sender, EventArgs e)
        {
            edit_info edit = new edit_info(loggedInUsername);
            edit.Show();
            this.Close();
        }

        private void edit_info_button_Click_1(object sender, EventArgs e)
        {
            edit_info edit = new edit_info(loggedInUsername);
            edit.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
