using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace society_management_system
{
    public partial class society_reg : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        public event EventHandler SocietyRegistered;
        public society_reg(string head)
        {
            InitializeComponent();
            Head = head;
        }

        public string Head { get; set; }

        private void signup_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(society_name_box.Text) || string.IsNullOrEmpty(desc_box.Text) || string.IsNullOrEmpty(num_users_box.Text))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int numUsers;
            if (!int.TryParse(num_users_box.Text, out numUsers))
            {
                MessageBox.Show("Number of users must be an integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = @"INSERT INTO societies (name, description, num_users, head)
                             VALUES (@name, @description, @num_users, @head)";

            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", society_name_box.Text);
                    cmd.Parameters.AddWithValue("@description", desc_box.Text);
                    cmd.Parameters.AddWithValue("@num_users", numUsers);
                    cmd.Parameters.AddWithValue("@head", Head);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Society registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SocietyRegistered?.Invoke(this, EventArgs.Empty);
                            society_name_box.Clear();
                            desc_box.Clear();
                            num_users_box.Clear();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to register society.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error registering society: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
