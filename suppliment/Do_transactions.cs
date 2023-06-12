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
    public partial class Do_transactions : UserControl
    {
        Suppliment sup_transaction = new Suppliment();
        Users new_user = new Users();
        private Boolean is_id_valid;
        DataSet ds;

        public Do_transactions()
        {
            InitializeComponent();
        }

        private void Do_transactions_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            sup_transaction.Date_time = DateTime.Now;

            if (txt_id.Text != "" && is_id_valid== true)
            {
                sup_transaction.Transactor = new Users();
                sup_transaction.Transactor.Id = int.Parse(txt_id.Text);

                if(txt_advance.Text != "")
                {
                    sup_transaction.Advance = double.Parse(txt_advance.Text);
                }
                if (txt_fertiliser.Text != "")
                {
                    sup_transaction.Fertiliser = double.Parse(txt_fertiliser.Text);
                }
                if (txt_teaPowder.Text != "")
                {
                    sup_transaction.Tea_powder = double.Parse(txt_teaPowder.Text);
                }
                if (txt_other.Text != "")
                {
                    sup_transaction.Other = double.Parse(txt_other.Text);
                }
                
                if(txt_advance.Text != "" || txt_fertiliser.Text != "" || txt_teaPowder.Text != "" || txt_other.Text != "")
                {
                    sup_transaction.write_dataBase(sup_transaction.Supliment_id, sup_transaction.Date_time, sup_transaction.Advance, sup_transaction.Fertiliser, sup_transaction.Tea_powder, sup_transaction.Other);
                }
                else
                {
                    MessageBox.Show("enter data", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                txt_id.Text = "";
                txt_name.Text = "";
                txt_advance.Text = "";
                txt_fertiliser.Text = "";
                txt_teaPowder.Text = "";
                txt_other.Text = "";

                Globle.F1.refresh();
                Globle.T1.refresh();
            }
            else
            {
                MessageBox.Show("unvalid id", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //is valid id
        private void txt_id_TextChanged(object sender, EventArgs e)
        {
            ds = new_user.read_dataBase_Id(txt_id.Text);

            if (ds.Tables[0].Rows.Count == 0)
            {
                is_id_valid = false;
                pictureBox1.ImageLocation = @"C:\Users\ASUS\OneDrive\Documents\pos_system_0.0.1-20211018T025501Z-001\img\no.png";
                txt_name.Text = "අංකය නොගැලපේ";
            }
            else
            {
                is_id_valid = true;
                pictureBox1.ImageLocation = @"C:\Users\ASUS\OneDrive\Documents\pos_system_0.0.1-20211018T025501Z-001\img\yes.png";
                txt_name.Text = new_user.read_dataBase_Id(txt_id.Text).Tables[0].Rows[0][1].ToString();
            }

        }

        private void txt_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            sup_transaction.validation_number(e);
        }

        private void txt_other_KeyPress(object sender, KeyPressEventArgs e)
        {
            sup_transaction.validation_double(e);
        }

        
    }
}
