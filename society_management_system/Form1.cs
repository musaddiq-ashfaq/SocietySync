using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace society_management_system
{
    public partial class Form1 : Form
    {
        SqlConnection connection_string = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void signup_button_Click(object sender, EventArgs e)
        {
            string username = username_box.Text;
            string password = password_box.Text;
            string name = name_box.Text;
            string role = role_box.Text;
            string batch = batch_box.Text;
            string degree = degree_box.Text;
            string society = null;

            bool pass_check = password.Length >= 8;
            bool allFieldsFilled = !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(batch) && !string.IsNullOrEmpty(degree);

            if (!allFieldsFilled)
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!pass_check)
            {
                MessageBox.Show("Password must have 8 digits or more.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (role == "head")
            {
                society_reg sr = new society_reg(username);
                sr.SocietyRegistered += (s, args) =>
                {
                    string societyName = sr.SocietyName; // Retrieve the society name from the property
                    bool flag = signup(username, password, name, role, batch, degree, societyName);
                    if (flag)
                    {
                        MessageBox.Show("User signed up successfully with society: " + societyName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        home home_page = new home(username);
                        home_page.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Failed to sign up user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                sr.Show();
            }
            else
            {
                SelectSocietyDialog dialog = new SelectSocietyDialog(username);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    society = dialog.SelectedSociety;
                    bool flag = signup(username, password, name, role, batch, degree,society);
                    if (flag)
                    {
                        MessageBox.Show("User signed up successfully with society: " + society, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        home home_page = new home(username);
                        home_page.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Failed to sign up user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("User cancelled society selection.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool signup(string username, string password, string name, string role, string batch, string degree,string society)
        {
            string query = @"
            INSERT INTO users (username, password, name, role, batch, degree, society)
            SELECT @username, @password, @name, @role, @batch, @degree, @society
            WHERE NOT EXISTS (SELECT 1 FROM users WHERE username = @username)";

            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@batch", batch);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@degree", degree);

                if (!string.IsNullOrEmpty(society))
                {
                    command.Parameters.AddWithValue("@society", society);
                }
                else
                {
                    // Handle the case where society is null or empty
                    MessageBox.Show("Society is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // Return false indicating failure
                }


                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login_form loginForm = new login_form();
            loginForm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void degree_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}