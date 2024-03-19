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

             bool flag = InsertUser(username,password);
            if (flag == false)
            {
                MessageBox.Show("Username Already Exist");
            }
            else
            {
                MessageBox.Show("SignUp Successful");
            }
        }
        private bool InsertUser(string username, string password)
        {
            string query = @"
            INSERT INTO users (username, password)
            SELECT @username, @password
            WHERE NOT EXISTS (
                SELECT 1 FROM users WHERE username = @username
            )";

            using (SqlConnection connection = new SqlConnection(connection_string.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

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
    }
}
