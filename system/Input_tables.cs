using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tea_collection_system
{
    class Input_tables
    {

        //public void read_dataBase() { }
        //public void write_dataBase() { }
        //public void update_dataBase() { }
        //public void delete_dataBase() { }

        public void validation_name(KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsControl(ch) && !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        public void validation_number(KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsControl(ch) && !char.IsDigit(ch))
            {
                e.Handled = true;
            }
        }

        public void validation_double(KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsControl(ch) && !char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
