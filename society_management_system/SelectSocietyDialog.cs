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
    public partial class SelectSocietyDialog : Form
    {
        SqlConnection connection_string = new SqlConnection(@"Data Source=DESKTOP-LIM5U3M\SQLEXPRESS;Initial Catalog=society_db;Integrated Security=True");
        public string SelectedSociety { get; private set; }
        private string loggedInUsername;
        public SelectSocietyDialog(string uname)
        {
            InitializeComponent();
            PopulateSocietyDropdown();
            confirm_button.DialogResult = DialogResult.None;
            loggedInUsername = uname;

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
        private void SelectSocietyDialog_Load(object sender, EventArgs e)
        {

        }
        private void societyDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            confirm_button.DialogResult = societyDropdown.SelectedItem != null ? DialogResult.OK : DialogResult.None;
        }

        private void confirm_button_Click(object sender, EventArgs e)
        {
            if (societyDropdown.SelectedItem != null)
            {
                SelectedSociety = societyDropdown.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select a society.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
