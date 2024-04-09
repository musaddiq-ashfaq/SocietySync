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
                        society_label.Text = reader["society"].ToString();
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
            view_users_button.Visible = false;
            add_event_button.Visible = false;
            if (IsHead(loggedInUsername))
                add_event_button.Visible = true;
            if (IsAdmin(loggedInUsername))
                view_users_button.Visible = true;
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
        private bool IsAdmin(string username)
        {
            string query = "SELECT COUNT(*) FROM users WHERE username = @username AND role = 'admin'";

            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking user role: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        private bool IsHead(string username)
        {
            string query = "SELECT COUNT(*) FROM users WHERE username = @username AND role = 'head'";
            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking user role: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void view_users_button_Click(object sender, EventArgs e)
        {
            view_users viewusers = new view_users(loggedInUsername);
            viewusers.Show();
            this.Close();
        }

        private void add_event_button_Click(object sender, EventArgs e)
        {
            Add_Event addEvent = new Add_Event(loggedInUsername);
            addEvent.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnnouncementsPage ann = new AnnouncementsPage(loggedInUsername);
            ann.Show();
            this.Close();
        }
    }
}
