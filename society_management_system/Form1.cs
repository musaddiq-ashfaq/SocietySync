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

            bool pass_check = false;
            if (password.Length < 8)
            {
                pass_check = false;
            }
            else
                pass_check = true;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(role) || string.IsNullOrEmpty(batch) || string.IsNullOrEmpty(degree))
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if(pass_check==false)
                    MessageBox.Show("Password must have 8 digits or more.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    bool flag = signup(username, password, name, role, batch, degree);
                    if (flag == true)
                    {
                        home home_page = new home(username);
                        home_page.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("Username is taken. Try any other username");
                }
            }
        }

        private bool signup(string username, string password, string name, string role, string batch, string degree)
        {
            string query = @"
            INSERT INTO users (username, password, name, role, batch, degree)
            SELECT @username, @password, @name, @role, @batch, @degree
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

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected>0;
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
