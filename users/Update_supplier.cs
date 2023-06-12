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
    public partial class Update_supplier : UserControl
    {
        DataSet ds;
        Users updateSupplier = new Users();

        public Update_supplier(int id)
        {
            InitializeComponent();

            updateSupplier.Id = id;
            ds = updateSupplier.read_dataBase(id);

            txt_name.Text = ds.Tables[0].Rows[0][1].ToString();
            txt_contact.Text = ds.Tables[0].Rows[0][2].ToString();
            txt_address.Text = ds.Tables[0].Rows[0][3].ToString();
        }

        private void Update_supplier_Load(object sender, EventArgs e)
        {

        }

        //update button event
        private void btn_update_Click(object sender, EventArgs e)
        {
            updateSupplier.Name = txt_name.Text;
            updateSupplier.Contact = txt_contact.Text;
            updateSupplier.Address = txt_address.Text;

            updateSupplier.update_dataBase(updateSupplier.Id, updateSupplier.Name, updateSupplier.Contact, updateSupplier.Address);

            Globle.S1.refresh();
        }

        
        //text box validation

        private void txt_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            updateSupplier.validation_name(e);
        }

        private void txt_contact_KeyPress(object sender, KeyPressEventArgs e)
        {
            updateSupplier.validation_number(e);
        }
    }
}
