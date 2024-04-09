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
        private string logged_admin;
        private string logged_user;

        public edit_info(string username, string admin = null)
        {
            InitializeComponent();
            logged_user = username;
            logged_admin = admin;
            PopulateSocietyDropdown();
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

                        // Set the selected item in the society dropdown
                        societyDropdown.SelectedItem = reader["society"].ToString();
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
            string newSociety = societyDropdown.SelectedItem.ToString();

            string query = @"
        UPDATE users
        SET degree = @newDegree, role = @newRole, password = @newpassword, name = @newName, batch = @newBatch, username = @newUsername, society = @newSociety
        WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@newBatch", newBatch);
                command.Parameters.AddWithValue("@newDegree", newDegree);
                command.Parameters.AddWithValue("@newpassword", newpassword);
                command.Parameters.AddWithValue("@newUsername", newUsername);
                command.Parameters.AddWithValue("@newRole", newRole);
                command.Parameters.AddWithValue("@username", logged_user);
                command.Parameters.AddWithValue("@newName", newName);
                command.Parameters.AddWithValue("@newSociety", newSociety);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("User information updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to update user information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating user information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void home_btn_Click(object sender, EventArgs e)
        {
            if (logged_admin != null)
            {
                home homePage = new home(logged_admin);
                homePage.Show();
                this.Close();
            }
            else
            {
                home homePage = new home(logged_user);
                homePage.Show();
                this.Close();
            }
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void PopulateSocietyDropdown()
        {
            try
            {
                string query = "SELECT name FROM societies";
                SqlCommand cmd = new SqlCommand(query, connection_string);

                connection_string.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> societyNames = new List<string>();
                while (reader.Read())
                {
                    societyNames.Add(reader["name"].ToString());
                }

                societyDropdown.DataSource = societyNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection_string.Close();
            }
        }
    }
}
