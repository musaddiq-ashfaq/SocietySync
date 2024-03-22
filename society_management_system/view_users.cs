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
    public partial class view_users : Form
    {
        SqlConnection connection_string = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        private string loggedInUsername;
        public view_users(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private void view_users_Load(object sender, EventArgs e)
        {
            //users_table.CellContentClick += dataGridViewUsers_CellContentClick;
            DisplayAllUsers();
        }
        private void DisplayAllUsers()
        {
            string query = "SELECT name, role, username, degree, batch, password FROM users";

            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Display data in DataGridView
                    users_table.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching user information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void exit_btn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void home_btn_Click(object sender, EventArgs e)
        {
            home home_page= new home(loggedInUsername);
            home_page.Show();
            this.Close();
        }

        private void users_table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            string username = username_input_box.Text;

            // Pass both the username of the user and the admin's username to the edit_info form
            edit_info edit = new edit_info(username, loggedInUsername);
            edit.Show();
            this.Close();
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            string username = username_input_box.Text;

            // Prompt the admin for confirmation
            DialogResult result = MessageBox.Show($"Are you sure you want to delete the user {username}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM users WHERE username = @username";
                using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@username", username);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DisplayAllUsers();
                        }
                        else
                        {
                            MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
