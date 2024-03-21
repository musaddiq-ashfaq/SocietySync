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
    public partial class login_form : Form
    {
        SqlConnection connection_string = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        public login_form()
        {
            InitializeComponent();
        }
        private void login_button_Click(object sender, EventArgs e)
        {
            String username = username_box.Text;
            String password = password_box.Text;
            bool flag = check_user(username, password);
            if(flag==true)
            {
                //MessageBox.Show("Login Successful");
                home homePage = new home(username);
                homePage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Check username and password");
            }
        }
        private bool check_user(string username, string password)
        {
            string query = @"SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking user credentials: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 signup_form = new Form1();
            signup_form.Show();
            this.Hide();
        }
    }
}
