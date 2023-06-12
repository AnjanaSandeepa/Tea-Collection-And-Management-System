using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pos_system_0._0._1
{
    public partial class progress : Form
    {
        public progress()
        {
            InitializeComponent();
            log.Hide();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (guna2CircleProgressBar1.Value == 100)
            {
                timer.Stop();
                label_loading.Hide();
                log.Show();
            }
            else
            {
                guna2CircleProgressBar1.Value += 1;
                label_val.Text = (Convert.ToInt32(label_val.Text) +1).ToString();
            }
        }

        private void progress_Load(object sender, EventArgs e)
        {
            guna2ShadowForm.SetShadowForm(this);
            timer.Start();
        }

        private void log_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            
            this.Hide();
            //this.Dispose();
            // this.Close();

            //clear memory
            GC.Collect();
        }
    }
}
