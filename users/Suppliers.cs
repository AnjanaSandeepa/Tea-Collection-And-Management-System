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
    public partial class Suppliers : UserControl 
    {
        DataSet ds;
        Users supplier = new Users();

        public Suppliers()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        //supplier load
        private void Suppliers_Load(object sender, EventArgs e)
        {
            ds = supplier.read_dataBase();        
            setdataGridView(ds);
            lab_supplierCount.Text = ds.Tables[0].Rows.Count.ToString();

		}

        //refresh
        public void refresh()
        {
            Suppliers_Load(this, null);
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        //set datagridview
        private void setdataGridView(DataSet ds)
        {
            dataGridView.Rows.Clear();

            //rowcount variable danna ba search eke awlk eno
            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                n = dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = int.Parse(ds.Tables[0].Rows[n][0].ToString());
                dataGridView.Rows[n].Cells[1].Value = ds.Tables[0].Rows[n][1].ToString();
                dataGridView.Rows[n].Cells[2].Value = ds.Tables[0].Rows[n][2].ToString();
                dataGridView.Rows[n].Cells[3].Value = ds.Tables[0].Rows[n][3].ToString();         
            }
        }

        //select row
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                supplier.Id = int.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
  
        //search
        private void btn_search_TextChanged(object sender, EventArgs e)
        {
            setdataGridView(supplier.s_read_database(btn_search.Text));    
        }

        private void btn_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            supplier.validation_number(e);
        }

        //add supplier
        private void btn_add_Click(object sender, EventArgs e)
        {
            Input_form f1 = new Input_form();
            f1.AddSuplyer();
        }

        //update supplier
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (supplier.Id != null)
                {
                    Input_form our_supplier = new Input_form();
                    our_supplier.UpdatesSpplier(supplier.Id);
                }
                else
                {
                    MessageBox.Show("please select collum.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //delete supplier
        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (supplier.Id != null)
            {
                supplier.delete_dataBase(supplier.Id);

                Globle.S1.refresh();
                Globle.F1.refresh();
            }
            else
            {
                MessageBox.Show("please select collum.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
