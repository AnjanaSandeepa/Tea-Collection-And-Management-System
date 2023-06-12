using System;
using System.Data;
using System.Windows.Forms;

namespace tea_collection_system
{
    public partial class Home : UserControl
    {
        private DataSet ds;
        System_details systemaData = new System_details();

        public Home()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void Home1_Load(object sender, EventArgs e)
        {
            //get year month
            systemaData.Year_month = DateTime.Now.ToString("yyyy'-'MM");

            //read the database and get data
            ds = systemaData.read_dataBase(systemaData.Year_month);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                systemaData.Id = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txt_price_of_1Kg.Text = (double.Parse(ds.Tables[0].Rows[0][2].ToString())).ToString("n2");
                txt_subtraction_weight_percentage.Text = (float.Parse(ds.Tables[0].Rows[0][3].ToString())).ToString("n1");
                txt_transport_cost.Text = (double.Parse(ds.Tables[0].Rows[0][4].ToString())).ToString("n2");
            }
            else
            {
                txt_price_of_1Kg.Text = "0.00";
                txt_subtraction_weight_percentage.Text = "0.0";
                txt_transport_cost.Text = "0.00";

                //write the 0 data for empty database 
                systemaData.write_dataBase(systemaData.Year_month, 0, 0, 0);
            }

           //read the database and fill the datagridview
            setdataGridView(systemaData.read_dataBase());

            //date banner
            date_label.Text = DateTime.Now.ToString("yyyy") + " වසරේ " + DateTime.Now.ToString("MM") + " මාසය සදහා මිල සටහන";
        }

        public void refresh()
        {
            Home1_Load(this, null);
        }

        private void setdataGridView(DataSet ds)
        {
            dataGridView.Rows.Clear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    n = dataGridView.Rows.Add();
                    dataGridView.Rows[n].Cells[0].Value = ds.Tables[0].Rows[n][1].ToString();
                    dataGridView.Rows[n].Cells[1].Value = double.Parse(ds.Tables[0].Rows[n][2].ToString()).ToString("n2");
                    dataGridView.Rows[n].Cells[2].Value = double.Parse(ds.Tables[0].Rows[n][3].ToString()).ToString("n1") + "%";
                    dataGridView.Rows[n].Cells[3].Value = double.Parse(ds.Tables[0].Rows[n][4].ToString()).ToString("n2");
                }
                dataGridView.Sort(dataGridView.Columns["Column1"], System.ComponentModel.ListSortDirection.Descending);
                dataGridView.CurrentCell = null;
                dataGridView[0, 0].Selected = true;
            }
        }


        //set the value into the variable
        private void btn_update_Click(object sender, EventArgs e)
        {
            if(txt_subtraction_weight_percentage.Text != "")
            {
                systemaData.Subtraction_weight_percentage = float.Parse(txt_subtraction_weight_percentage.Text);
            } 

            if(txt_transport_cost.Text != "")
            {
                systemaData.Transport_cost_of_1kg = double.Parse(txt_transport_cost.Text);
            }

            if(txt_price_of_1Kg.Text != "")
            {
                systemaData.Price_of_1Kg = double.Parse(txt_price_of_1Kg.Text);
            }
 
            if (systemaData.read_dataBase().Tables[0].Rows.Count == 0)
            {
                systemaData.write_dataBase(systemaData.Year_month, systemaData.Price_of_1Kg, systemaData.Subtraction_weight_percentage, systemaData.Transport_cost_of_1kg);
            }
            else
            {
                systemaData.update_dataBase(systemaData.Id, systemaData.Year_month, systemaData.Price_of_1Kg, systemaData.Subtraction_weight_percentage, systemaData.Transport_cost_of_1kg);
            }

            refresh();
            Globle.F1.refresh();
            Globle.C1.refresh();
        }


        //text box validate for floting value

        private void txt_price_of_1Kg_KeyPress(object sender, KeyPressEventArgs e)
        {
            systemaData.validation_double(e);
        }

        private void txt_subtraction_weight_percentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            systemaData.validation_double(e);
        }

        private void txt_transport_cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            systemaData.validation_double(e);
        }

        //set placeholder text for textbox

        private void txt_price_of_1Kg_Enter(object sender, EventArgs e)
        {
            txt_price_of_1Kg.Text = "";
        }

        private void txt_subtraction_weight_percentage_Enter(object sender, EventArgs e)
        {
            txt_subtraction_weight_percentage.Text = "";
        }

        private void txt_transport_cost_Enter(object sender, EventArgs e)
        {
            txt_transport_cost.Text = "";
        }

        private void txt_price_of_1Kg_Leave(object sender, EventArgs e)
        {
            if (txt_price_of_1Kg.Text =="")
            {
                refresh();
            } 
        }

        private void txt_subtraction_weight_percentage_Leave(object sender, EventArgs e)
        {
            if (txt_subtraction_weight_percentage.Text == "")
            {
                refresh();
            }
        }

        private void txt_transport_cost_Leave(object sender, EventArgs e)
        {
            if (txt_transport_cost.Text == "")
            {
                refresh();
            }
        }
    }
}
