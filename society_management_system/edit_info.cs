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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace society_management_system
{
    public partial class edit_info : Form
    {
        SqlConnection connection_string = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        private string logged_user;
        public edit_info(string username)
        {
            InitializeComponent();
            logged_user = username;
        }

        private void edit_info_Load(object sender, EventArgs e)
        {
            string username = logged_user;
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

        private void save_button_Click(object sender, EventArgs e)
        {
            string newUsername = username_box.Text;
            string newpassword = password_box.Text;
            string newName = name_box.Text;
            string newRole = role_box.Text;
            string newBatch = batch_box.Text;
            string newDegree = degree_box.Text;

            string query = @"
        UPDATE users
        SET name = @newName, role = @newRole, batch = @newBatch, degree = @newDegree, password = @newpassword, username = @newUsername
        WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@newName", newName);
                command.Parameters.AddWithValue("@newRole", newRole);
                command.Parameters.AddWithValue("@newBatch", newBatch);
                command.Parameters.AddWithValue("@newDegree", newDegree);
                command.Parameters.AddWithValue("@newUsername", newUsername);
                command.Parameters.AddWithValue("@newpassword", newpassword);
                command.Parameters.AddWithValue("@username", logged_user);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User information updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        home homePage = new home(newUsername);
                        homePage.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating user information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void home_btn_Click(object sender, EventArgs e)
        {
            home homePage = new home(logged_user);
            homePage.Show();
            this.Close();
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
