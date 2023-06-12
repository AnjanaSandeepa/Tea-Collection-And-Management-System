using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tea_collection_system.logging
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //set interface shadow appearance 
            guna2ShadowForm1.SetShadowForm(this);

            //set first tab
            this.ActiveControl = txt_user;
            txt_user.Select();

            //set password char
            txt_password.UseSystemPasswordChar = true;
        }
    }
}
