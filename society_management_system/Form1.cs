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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void signup_button_Click(object sender, EventArgs e)
        {
            String username = username_box.Text;
            String password = password_box.Text;
            String name = name_box.Text;
            String role = role_box.Text;
            String batch = batch_box.Text;
            String degree = degree_box.Text;

            bool flag = InsertUser(username, password, name, role, batch, degree);
            if (flag)
            {
                MessageBox.Show("SignUp Successful");
                home homePage = new home(username);
                homePage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username Already Exists");
            }
        }

        private bool InsertUser(string username, string password, string name, string role, string batch, string degree)
        {
            string query = @"
            INSERT INTO users (username, password, name, role, batch, degree)
            SELECT @username, @password, @name, @role, @batch, @degree
            WHERE NOT EXISTS (
                SELECT 1 FROM users WHERE username = @username
            )";

            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@batch", batch);
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


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

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
    }
}
