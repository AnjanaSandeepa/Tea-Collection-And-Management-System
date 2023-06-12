using CrystalDecisions.CrystalReports.Engine;
using pos_system_0._0._1.selling;
using System;
using System.Data;
using System.Windows.Forms;
using tea_collection_system.Printer;

namespace tea_collection_system
{
    public partial class collections : UserControl
    {
        Input collection = new Input();
        DataSet ds1;


        String selectMonth;
        DateTime check_date;

        public collections()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void collections_Load(object sender, EventArgs e)
        {
            collection.Date_time = DateTime.Now;

            lab_suppyCount.Text = (collection.read_dataBase().Tables[0].Rows.Count).ToString();

            com_selectMonth.SelectedIndex = 0;
            setdataGridView(collection.read_dataBase(collection.Date_time.ToString("yyyy-MM")));
        }

        public void refresh()
        {
            collections_Load(this, null);
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        //find datagridviwe cellname (day and id)
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                collection.Input_id = int.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                check_date = DateTime.Parse(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void com_selectMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectMonth = com_selectMonth.Text;

            if (selectMonth == "සියල්ල")
            {
                setdataGridView(collection.read_dataBase());            
            }
            else if(selectMonth == "මෙම මාසය")
            {
                setdataGridView(collection.read_dataBase(collection.Date_time.ToString("yyyy-MM")));
            }
            else if (selectMonth == "පසුගිය මාසය")
            {
                setdataGridView(collection.read_dataBase(DateTime.Now.AddMonths(-1).ToString("yyyy-MM")));
            }
        }

        //search part
        private void btn_search_TextChanged_1(object sender, EventArgs e)
        {
			if (selectMonth == "සියල්ල")
            {
                setdataGridView(collection.S_read_dataBase(btn_search.Text));  
            }

            else if (selectMonth == "මෙම මාසය")
            {
                setdataGridView(collection.S_read_dataBase(collection.Date_time.ToString("yyyy-MM"), btn_search.Text));
            }

            else if (selectMonth == "පසුගිය මාසය")
            {
                setdataGridView(collection.S_read_dataBase(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"), btn_search.Text));
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Input_form I1 = new Input_form();
            I1.Supplier_input();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (collection.Input_id != null)
            {
                if(check_date.ToString("yyyy'-'MM'-'dd") == collection.Date_time.ToString("yyyy'-'MM'-'dd"))
                {
                    collection.delete_dataBase(collection.Input_id);

                    refresh();
                    Globle.S1.refresh();
                    Globle.F1.refresh();
                }
                else
                {
                    MessageBox.Show("ada dina athulath kala dhaththa pamanak makima sidukala haka", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("please select collum.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btn_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            collection.validation_number(e);
        }

		private void setdataGridView(DataSet ds)
        {
            dataGridView.Rows.Clear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {

                n = dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = ds.Tables[0].Rows[n][0].ToString();                                                 //number
                dataGridView.Rows[n].Cells[1].Value = ds.Tables[0].Rows[n][1].ToString();                                                 //date
				dataGridView.Rows[n].Cells[2].Value = ds.Tables[0].Rows[n][2].ToString();                                                 //id
				dataGridView.Rows[n].Cells[3].Value = ds.Tables[0].Rows[n][3].ToString();                                                 // user_name;
                dataGridView.Rows[n].Cells[4].Value = double.Parse(ds.Tables[0].Rows[n][4].ToString()).ToString("n1");                    //temp_full_weight.ToString("n1");
				dataGridView.Rows[n].Cells[5].Value = double.Parse(ds.Tables[0].Rows[n][5].ToString()).ToString("n1");                    //  temp_Net_weight.ToString("n1");

			    dataGridView.Rows[n].Cells[7].Value = ds.Tables[0].Rows[n][6].ToString();                                                 //Year_month;
				dataGridView.Rows[n].Cells[8].Value = double.Parse(ds.Tables[0].Rows[n][7].ToString());                                   //Subtraction_weight_percentage;            
				dataGridView.Rows[n].Cells[9].Value = double.Parse(ds.Tables[0].Rows[n][8].ToString());                                   //Price_of_1Kg;                  
				dataGridView.Rows[n].Cells[10].Value = double.Parse(ds.Tables[0].Rows[n][9].ToString());                                  // Transport_cost_of_1kg;

				}
                dataGridView.Sort(dataGridView.Columns["Column5"], System.ComponentModel.ListSortDirection.Descending);
                dataGridView.CurrentCell = null;
                dataGridView[0, 0].Selected = true;
            }
        }


		//bill print
		private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //set your button column index instead of 6
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                try
                {
                    //dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

                    receipt_view fm = new receipt_view();
                    receipt_print_2 cr = new receipt_print_2();

                    TextObject idNo = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["txt_idNo"];
                    idNo.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                    TextObject chashier = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["txt_chashier"];
                    chashier.Text = "admin";
                    TextObject date = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["txt_date"];
                    date.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

                    TextObject name = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_name"];
                    name.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                    TextObject fullWeight = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_fullWeight"];
                    fullWeight.Text = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                    TextObject netWeight = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_netWeight"];
                    netWeight.Text = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();

                    TextObject year_month = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_yearMonth"];
                    year_month.Text = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                    TextObject priceOf1kg = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_priceOf1kg"];
                    priceOf1kg.Text = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                    TextObject transport_cost_percentage = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_transport_cost_percentage"];
                    transport_cost_percentage.Text = dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
                    TextObject subtraction_weight_percentage = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_subtraction_weight_percentage"];
                    subtraction_weight_percentage.Text = dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();




                    fm.crystalReportViewer1.ReportSource = cr;
                    fm.crystalReportViewer1.Refresh();
                    fm.Show();
                }
                catch
                {
                    MessageBox.Show("plese select collum.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
	}
}