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
    public partial class Add_supplier : UserControl
    {
        private bool is_name_valid;
        Users new_addSupplier = new Users();
        DataSet ds;

        public Add_supplier()
        {
            InitializeComponent();
        }

        private void Add_supplier_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txt_name;
            txt_name.Focus();
        }

        //name validation
        private void txt_name_TextChanged(object sender, EventArgs e)
        {
            Is_valid_name();
        }

        private void Is_valid_name()
        {
            ds = new_addSupplier.read_dataBase(txt_name.Text);

            if (ds.Tables[0].Rows.Count == 0 && txt_name.Text != "")
            {
                is_name_valid = true;
                pictureBox1.ImageLocation = @"C:\Users\ASUS\OneDrive\Documents\pos_system_0.0.1-20211018T025501Z-001\img\yes.png";
            }
            else
            {
                is_name_valid = false;
                pictureBox1.ImageLocation = @"C:\Users\ASUS\OneDrive\Documents\pos_system_0.0.1-20211018T025501Z-001\img\no.png";
            }
        }

        // click event
        private void submit_Click(object sender, EventArgs e)
        {
            //new_addSupplier.Id;                       //auto create supplier_id
            new_addSupplier.Name = txt_name.Text;
            new_addSupplier.Contact = txt_contact.Text;
            new_addSupplier.Address = txt_address.Text;

            if (new_addSupplier.Name != "" && is_name_valid == true)
            {
                string message = " සැපයුම්කරු ඇතුලත් කරන්නද ?";
                string title = "";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    new_addSupplier.write_dataBase(new_addSupplier.Name, new_addSupplier.Contact, new_addSupplier.Address);
                }
            
                Globle.S1.refresh();
                Globle.F1.refresh();
            }
            else
            {
                MessageBox.Show("some Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txt_name.Text = "";
            txt_address.Text = "";
            txt_contact.Text = "";

            this.ActiveControl = txt_name;
            txt_name.Focus();
        }


       //textbox validation

        private void txt_contact_KeyPress(object sender, KeyPressEventArgs e)
        {
            new_addSupplier.validation_number(e);
        }

        private void txt_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            new_addSupplier.validation_name(e);
        }

        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            new_addSupplier.validation_name(e);
        }
    }
}
