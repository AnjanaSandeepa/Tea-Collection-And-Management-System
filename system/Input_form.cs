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
    public partial class Input_form : Form
    {
        public Input_form()
        {
            InitializeComponent();
        }

        private void Input_Load(object sender, EventArgs e)
        {}

        public void AddSuplyer()
        {    
            panel1.Controls.Clear();
            panel1.Controls.Add(new Add_supplier());
            this.ShowDialog();
        }

        public void UpdatesSpplier(int id)
        {    
            panel1.Controls.Clear();
            panel1.Controls.Add(new Update_supplier(id));
            this.ShowDialog();
        }

        public void Supplier_input()
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(new Input_collections());
            this.ShowDialog();
        }

        public void Transactions()
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(new Do_transactions());
            this.ShowDialog();
        }
    }
}
