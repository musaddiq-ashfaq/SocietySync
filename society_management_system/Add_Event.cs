using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace society_management_system
{
    public partial class Add_Event : Form
    {
        private SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        private string loggedInUsername;
        private string loggedInSocietyName;

        public Add_Event(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            loggedInSocietyName = GetSocietyName(username);
        }

        private string GetSocietyName(string username)
        {
            string societyName = "";
            string query = "SELECT society FROM users WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True"))
            {
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
                }
            }

            return societyName;
        }


        private void add_event_button_Click(object sender, EventArgs e)
        {
            string eventName = eventname_box.Text.Trim();
            string description = description_box.Text.Trim();
            DateTime? eventDate = dateTimePicker1.Value;
            string venue = venue_box.Text.Trim();

            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(description) || eventDate == null || string.IsNullOrEmpty(venue))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO events_table (name, description, date, venue, society_name) VALUES (@name, @description, @date, @venue, @societyName)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", eventName);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@date", eventDate);
                command.Parameters.AddWithValue("@venue", venue);
                command.Parameters.AddWithValue("@societyName", loggedInSocietyName);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Event added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding event: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ClearInputs()
        {
            eventname_box.Clear();
            description_box.Clear();
            dateTimePicker1.Value = DateTime.Today;
            venue_box.Clear();
        }

        private void edit_info_button_Click(object sender, EventArgs e)
        {
            edit_info edit = new edit_info(loggedInUsername);
            edit.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void home_btn_Click(object sender, EventArgs e)
        {
            home homePage = new home(loggedInUsername);
            homePage.Show();
            this.Close();
        }
    }
}
