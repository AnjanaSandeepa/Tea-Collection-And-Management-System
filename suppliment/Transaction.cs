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
    public partial class Transaction : UserControl
    {
        private DataSet ds;
        private String selectMonth;

        Suppliment suppliment = new Suppliment();

        public Transaction()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            suppliment.Date_time = DateTime.Now;

            com_selectMonth.SelectedIndex = 0;

            setdataGridView(suppliment.read_dataBase(suppliment.Date_time.ToString("yyyy-MM")));

            lab_transactionCount.Text = (suppliment.read_dataBase().Tables[0].Rows.Count).ToString(); 
        }

        public void refresh()
        {
            Transaction_Load(this, null);
        }

        private void setdataGridView(DataSet ds)
        {
            dataGridView.Rows.Clear();

            if (ds.Tables[0].Rows.Count > 0)
            {            
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {           
                    n = dataGridView.Rows.Add();
                    dataGridView.Rows[n].Cells[1].Value = ds.Tables[0].Rows[n][0].ToString();                                                               //id
					dataGridView.Rows[n].Cells[0].Value = DateTime.Parse(ds.Tables[0].Rows[n][1].ToString()).ToString("yyyy'-'MM'-'dd HH':'mm tt");
                    dataGridView.Rows[n].Cells[2].Value = ds.Tables[0].Rows[n][2].ToString();                                                               //name
					dataGridView.Rows[n].Cells[3].Value = double.Parse(ds.Tables[0].Rows[n][3].ToString()).ToString("n2");                                  //advance
                    dataGridView.Rows[n].Cells[4].Value = double.Parse(ds.Tables[0].Rows[n][4].ToString()).ToString("n2");                                  //portilizer
                    dataGridView.Rows[n].Cells[5].Value = double.Parse(ds.Tables[0].Rows[n][5].ToString()).ToString("n2");                                  //tea powder
                    dataGridView.Rows[n].Cells[6].Value = double.Parse(ds.Tables[0].Rows[n][6].ToString()).ToString("n2");                                  //other
                }
                dataGridView.Sort(dataGridView.Columns["Column5"], System.ComponentModel.ListSortDirection.Descending);
                dataGridView.CurrentCell = null;
                dataGridView[0, 0].Selected = true;
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                suppliment.Transactor = new Users();
                suppliment.Date_time = DateTime.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                suppliment.Transactor.Id = int.Parse(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            catch
            { }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Input_form i1 = new Input_form();
            i1.Transactions();
        }

        

        private void btn_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();

            if (selectMonth == "සියල්ල")
            {
                setdataGridView(suppliment.sRead_dataBase(btn_search.Text));
            }

            else if (selectMonth == "මෙම මාසය")
            {
                setdataGridView(suppliment.sRead_dataBase(suppliment.Date_time.ToString("yyyy-MM"), btn_search.Text));
            }

            else if (selectMonth == "පසුගිය මාසය")
            {
                setdataGridView(suppliment.sRead_dataBase(suppliment.Date_time.AddMonths(-1).ToString("yyyy-MM"), btn_search.Text));
            }
        }

        private void com_selectMonth_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            selectMonth = com_selectMonth.Text;

            if (selectMonth == "සියල්ල")
            {
                setdataGridView(suppliment.read_dataBase());
            }
            else if (selectMonth == "මෙම මාසය")
            {
                setdataGridView(suppliment.read_dataBase(suppliment.Date_time.ToString("yyyy-MM")));
            }
            else if (selectMonth == "පසුගිය මාසය")
            {
                setdataGridView(suppliment.read_dataBase(suppliment.Date_time.AddMonths(-1).ToString("yyyy-MM")));
            }
        }

        private void btn_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            suppliment.validation_number(e);
        }


        //print bill
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
                    receipt_print_3 cr = new receipt_print_3();

                    
                    TextObject chashier = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["txt_chashier"];
                    chashier.Text = "admin";
                    TextObject date = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["txt_date"];
                    date.Text = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();

                    TextObject id = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_id"];
                    id.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                    TextObject name = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_name"];
                    name.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();


                    TextObject advannce = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_advannce"];
                    advannce.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                    TextObject fertilizer = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_fertilizer"];
                    fertilizer.Text = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                    TextObject teaPowder = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_teaPowder"];
                    teaPowder.Text = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                    TextObject otherAmount = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_otherAmount"];
                    otherAmount.Text = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();

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
