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
    public partial class AnnouncementsPage : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        string loggedInUsername;

        public AnnouncementsPage(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            LoadEvents();
        }

        private void LoadEvents()
        {
            string query = "SELECT * FROM events_table WHERE society_name = @societyName";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@societyName", GetSocietyName(loggedInUsername));

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Retrieve event details from the reader and add them to the UI
                        string eventName = reader["name"].ToString();
                        string description = reader["description"].ToString();
                        DateTime eventDate = Convert.ToDateTime(reader["date"]);
                        string venue = reader["venue"].ToString();

                        // Example: Add event details to a list box
                        listBox1.Items.Add(eventName + " - " + eventDate.ToShortDateString() + " - " + venue);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading events: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private string GetSocietyName(string username)
        {
            // Implement a method to retrieve the society name associated with the logged-in user
            // You can query the database or use any other method to retrieve this information
            // For example:
            string query = "SELECT society FROM users WHERE username = @username";
            string societyName = "";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        societyName = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving society name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            return societyName;
        }
        public AnnouncementsPage()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void edit_info_button_Click(object sender, EventArgs e)
        {
            edit_info edit = new edit_info(loggedInUsername);
            edit.Show();
            this.Close();
        }

        private void home_btn_Click(object sender, EventArgs e)
        {
            home homePage = new home(loggedInUsername);
            homePage.Show();
            this.Close();
        }
    }
}
