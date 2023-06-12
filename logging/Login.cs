using System;
using System.Data;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using tea_collection_system;

namespace pos_system_0._0._1
{
    public partial class Login : Form
    {
        const String macId = "A87EEA84A3FA";

        String User;
        String Password;
        DateTime date1;
        DateTime date2;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //set interface shadow appearance 
            guna2ShadowForm1.SetShadowForm(this);

            //set first tab
            this.ActiveControl = txt_user;
            txt_user.Select();

            //set password char
            txt_password.UseSystemPasswordChar = true;

            //set date
            date1 = DateTime.Parse(DateTime.Now.ToString("yyyy'-'MM'-'dd"));
            date2 = DateTime.Parse(DateTime.Now.ToString("2023'-'12'-'25"));
        }

        private void log_Click(object sender, EventArgs e)
        {
            User = txt_user.Text;
            Password = txt_password.Text;
            

            if (User == "admin" && Password == "adminfuck")
            {
                    Main_form mainForm = new Main_form();
                    mainForm.Show();
                    this.Hide();
            }
            else if (User == "admin" && Password == "admin_qsc")
            {
                string mac =  GetMacAddress();

                if (mac.Equals(macId))
                {
                    Main_form mainForm = new Main_form();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("බාවිතයට අවසර නොමැත. \n  කරුණාකර සහය සදහා සැකසුම්කරු අමතන්න", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (User == "admin" && Password == "admin")
            {
                if (date1 <= date2)
                {
                    Main_form mainForm = new Main_form();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("පද්ධතිය කල්ඉකුත්වී ඇත. \n  කරුණාකර සහය සදහා සැකසුම්කරු අමතන්න", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("username හෝ password වැරදී... \n නැවත උත්සාහ කරන්න", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // password box handle
        private void show_CheckedChanged(object sender, EventArgs e)
        {
            if (chex_show.Checked)
            {
                txt_password.UseSystemPasswordChar = false;
            }
            else
            {
                txt_password.UseSystemPasswordChar = true;
            }
        }

        // get mac id
        private string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }
    }
}
