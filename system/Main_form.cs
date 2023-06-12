using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tea_collection_system
{
    public partial class Main_form : Form
    {
        public Main_form()
        {
            InitializeComponent();

            //set screen setting
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(10, 5);
            this.Size = new Size(1260, 680);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            btn_Home.Focus();

            panel123.Controls.Clear();
            panel123.Controls.Add(Globle.H1);

            this.ActiveControl = btn_Home;
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            panel123.Controls.Clear();
            panel123.Controls.Add(Globle.H1);
        }

        private void btn_collection_Click(object sender, EventArgs e)
        {
            panel123.Controls.Clear();
            panel123.Controls.Add(Globle.C1);
        }

        private void btn_Supplier_Click(object sender, EventArgs e)
        {
            panel123.Controls.Clear();
            panel123.Controls.Add(Globle.S1);
        }

        private void btn_Finantial_Click(object sender, EventArgs e)
        {
            panel123.Controls.Clear();
            panel123.Controls.Add(Globle.F1);
        }

        private void btn_transaction_Click(object sender, EventArgs e)
        {
            panel123.Controls.Clear();
            panel123.Controls.Add(Globle.T1);
        }
    }
}
