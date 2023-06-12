using CrystalDecisions.CrystalReports.Engine;
using pos_system_0._0._1.selling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tea_collection_system.Printer;

namespace tea_collection_system
{
    public partial class Input_collections : UserControl
    {
        Database_connector db = new Database_connector();
        DataSet ds;

        private bool is_valid_id;
        const String imageLocation = @"C:\Users\ASUS\OneDrive\Documents\pos_system_0.0.1-20211018T025501Z-001\img\no.png";

        Input input_collection = new Input();
        Users newUser = new Users();


        public Input_collections()
        {
            InitializeComponent();
        }

        //id validation

        private void txt_id_TextChanged(object sender, EventArgs e)
        {
            Is_valid_id();
        }

        private void Is_valid_id()
        {
            DataSet ds = newUser.read_dataBase_Id(txt_id.Text);

            if (ds.Tables[0].Rows.Count == 0)
            {
                is_valid_id = false;
                pictureBox1.ImageLocation = imageLocation;
                txt_name.Text = "අංකය නොගැලපේ";
            }
            else
            {
                is_valid_id = true;
                pictureBox1.ImageLocation = imageLocation;
                txt_name.Text = newUser.read_dataBase_Id(txt_id.Text).Tables[0].Rows[0][1].ToString();
            }
        }

        // button action
        private void btn_save_Click(object sender, EventArgs e)
        {
            input_collection.Date_time = DateTime.Now;

            if (txt_id.Text != "" && txt_weight.Text != "" && is_valid_id == true)
            {
                input_collection.Supplier = new Users();
                input_collection.Supplier.Id = int.Parse(txt_id.Text);
                input_collection.Full_weight = double.Parse(txt_weight.Text);
                input_collection.Supplier.Name = txt_name.Text;

                if (input_collection.read_dataBase(DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"), input_collection.Supplier.Id).Tables[0].Rows.Count == 0)
                {
                    input_collection.write_dataBase(newUser.Id, input_collection.Full_weight);

                    txt_id.Text = "";
                    txt_weight.Text = "";

                    Globle.C1.refresh();
                    Globle.F1.refresh();
                }
                else
                {
                    string message = "\n සැපයුම්කරු අද දින සැපයුම් ඇතුලත් කර ඇත.\n නැවත සැපයුම් ඇතුලත් කරනවාද? ";
                    string title = "Worning";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.Yes)
                    {
                        input_collection.write_dataBase(newUser.Id, input_collection.Full_weight);
					}
                    else
                    {
                        // form closing code
                    }
                }
            }
            else
            {
                MessageBox.Show("enter data", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //validate text box

        private void txt_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            input_collection.validation_number(e);
        }

        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            input_collection.validation_name(e);
        }

        private void txt_weight_KeyPress(object sender, KeyPressEventArgs e)
        {
            input_collection.validation_double(e);
        }
    }
}
