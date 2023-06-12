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
using System.Xml.Linq;
using tea_collection_system.Printer;

namespace tea_collection_system
{
    public partial class Financial : UserControl
    {
        Sub_Finantial finantial = new Sub_Finantial();
        Users user = new Users();
        
        private int due_id = 0;
        DataSet ds;

        public Financial()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;   
        }

        private void financial_Load(object sender, EventArgs e)
        {
            finantial.Date_time = DateTime.Now;

            com_selectMonth.SelectedIndex = 0;
            setdataGridView(finantial.Date_time);

            DateTimePicker.ShowUpDown = true;
            DateTimePicker.Format = DateTimePickerFormat.Custom;
            DateTimePicker.CustomFormat = "MMMM - yyyy";
            DateTimePicker.MaxDate = DateTime.Parse(finantial.Date_time.ToString("MMMM - yyyy"));
        
            lab_supplierCount.Text = user.read_dataBase().Tables[0].Rows.Count.ToString();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = this.DateTimePicker.Value.Date;
            setdataGridView(dt);
           
            if(this.DateTimePicker.Text == finantial.Date_time.ToString("MMMM - yyyy"))
            {
                com_selectMonth.SelectedIndex = 0;
            }
            else if (this.DateTimePicker.Text == finantial.Date_time.AddMonths(-1).ToString("MMMM - yyyy"))
            {
                com_selectMonth.SelectedIndex = 1;
            }
            else
            {
                com_selectMonth.SelectedIndex = 2;
            }
        }

        private void com_selectMonth_SelectedIndexChanged(object sender, EventArgs e)
        {  
            if (com_selectMonth.Text == "මෙම මාසය")
            {
                setdataGridView(finantial.Date_time);
                this.DateTimePicker.Text = finantial.Date_time.ToString("MMMM - yyyy");
            }

            else if (com_selectMonth.Text == "පසුගිය මාසය")
            {
                setdataGridView(finantial.Date_time.AddMonths(-1));
                this.DateTimePicker.Text = finantial.Date_time.AddMonths(-1).ToString("MMMM - yyyy");
            }
        }

        public void refresh()
        {
            financial_Load(this, null);
        }
      
        public double get_month_due( int id, String date_month)
        {
            double temp_due = 0;
          //  Name = "";

            DataSet ds = finantial.read_dataBase(id, date_month);

            if (ds.Tables[0].Rows.Count > 0)
            {
                temp_due = double.Parse(ds.Tables[0].Rows[0][3].ToString());             
            }

            return temp_due;
        }

        public void set_month_due(int user_id, String year_month, double temp_due)
        {
            due_id = 0;
            DataSet ds = finantial.read_dataBase(user_id, year_month);

            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //get due id
                    due_id = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                    if (Math.Round(double.Parse(ds.Tables[0].Rows[0][3].ToString()), 2) != Math.Round(temp_due, 2))
                    {
                        finantial.update_dataBase(due_id, user_id, year_month, temp_due);
                    }
                }
                else
                {
                    //add new due
                    //overwrite due_id 
                    DataSet ds1 = finantial.read_dataBase();
                    due_id = ds1.Tables[0].Rows.Count +1 ;

                    finantial.write_dataBase(due_id, user_id, year_month, temp_due);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("some error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setdataGridView(DateTime time)
        {
            finantial.Supplier = new Users();
            finantial.Weight = new Input();
            finantial.SystemData = new System_details();
            finantial.Supplement = new Suppliment();

            dataGridView.Rows.Clear();

            //*************************************************

            //get supplier id and name  ##################################################################################
            Users user = new Users();
            DataSet ds = user.read_dataBase();

            for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
            {
                
                finantial.Supplier.Id = int.Parse(ds.Tables[0].Rows[n][0].ToString());
                finantial.Supplier.Name = ds.Tables[0].Rows[n][1].ToString();

                //get supplier full weight ##################################################################################
                Input input = new Input();
                DataSet ds1 = input.read_dataBase(time.ToString("MM"), time.ToString("yyyy"), finantial.Supplier.Id);

                if (ds1.Tables[0].Rows.Count != 0)
                {
                    DataSet ds1_1 = input.sum_full_weight_dataBase(time.ToString("MM"), time.ToString("yyyy"), finantial.Supplier.Id);

                    finantial.Weight.Full_weight = double.Parse(ds1_1.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    finantial.Weight.Full_weight = 0;
                }

                //get system details for month #############################################################################
                System_details system_details = new System_details();
                DataSet ds2 = system_details.read_dataBase(time.ToString("yyyy") + "-" + time.ToString("MM"));

                if (ds2.Tables[0].Rows.Count != 0)
                {
                    finantial.SystemData.Price_of_1Kg = double.Parse(ds2.Tables[0].Rows[0][2].ToString());
                    finantial.SystemData.Subtraction_weight_percentage = float.Parse(ds2.Tables[0].Rows[0][3].ToString());
                    finantial.SystemData.Transport_cost_of_1kg = double.Parse(ds2.Tables[0].Rows[0][4].ToString());
                }
                else
                {
                    finantial.SystemData.Price_of_1Kg = 0;
                    finantial.SystemData.Subtraction_weight_percentage = 0;
                    finantial.SystemData.Transport_cost_of_1kg = 0;
                }

                finantial.Weight.Net_weight = finantial.Weight.Full_weight * (1 - (finantial.SystemData.Subtraction_weight_percentage) / 100);
                finantial.Full_amount = finantial.Weight.Net_weight * finantial.SystemData.Price_of_1Kg;
                finantial.Transport_amount = finantial.Weight.Net_weight * (finantial.SystemData.Transport_cost_of_1kg);

                //get supplier Supliment details ################################################################################
                Suppliment suppliments = new Suppliment(); 
                DataSet ds3 = suppliments.read_dataBase(time.ToString("MM"), time.ToString("yyyy"), finantial.Supplier.Id);

                finantial.Supplement.Advance = 0;
                finantial.Supplement.Fertiliser = 0;
                finantial.Supplement.Tea_powder = 0;
                finantial.Supplement.Other = 0;

                double temp_Advance, temp_Fertiliser, temp_Tea_powder, temp_Other;

                for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                {
                    temp_Advance = double.Parse(ds3.Tables[0].Rows[i][3].ToString());
                    temp_Fertiliser = double.Parse(ds3.Tables[0].Rows[i][4].ToString());
                    temp_Tea_powder = double.Parse(ds3.Tables[0].Rows[i][5].ToString());
                    temp_Other = double.Parse(ds3.Tables[0].Rows[i][6].ToString());

                    finantial.Supplement.Advance = finantial.Supplement.Advance + temp_Advance;
                    finantial.Supplement.Fertiliser = finantial.Supplement.Fertiliser + temp_Fertiliser;
                    finantial.Supplement.Tea_powder = finantial.Supplement.Tea_powder + temp_Tea_powder;
                    finantial.Supplement.Other = finantial.Supplement.Other + temp_Other;
                }

                //##############################################################################################################
                double due = get_month_due(finantial.Supplier.Id, time.AddMonths(-1).ToString("yyyy'-'MM"));

                if (due < 0)
                {
                    finantial.Previous_month_due_amount = due * (-1);
                }
                else
                {
                    finantial.Previous_month_due_amount = 0;
                }

                finantial.Full_subtraction_amount = finantial.Supplement.Advance + finantial.Supplement.Fertiliser + finantial.Supplement.Tea_powder + finantial.Supplement.Other + finantial.Transport_amount + (finantial.Previous_month_due_amount);
                finantial.Net_amount = finantial.Full_amount - finantial.Full_subtraction_amount;

                //#################################################################################################################

                //String w1 = finantial.Date_time.ToString("yyyy'-'MM");
                //String w2 = time.ToString("yyyy'-'MM");

                //this month
                if (finantial.Date_time.ToString("yyyy'-'MM") == time.ToString("yyyy'-'MM"))
                {
                    set_month_due(finantial.Supplier.Id, finantial.Date_time.ToString("yyyy'-'MM"), finantial.Net_amount);

                }

                n = dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = finantial.Supplier.Id;
                dataGridView.Rows[n].Cells[1].Value = finantial.Supplier.Name;
                dataGridView.Rows[n].Cells[2].Value = finantial.Weight.Net_weight;

                dataGridView.Rows[n].Cells[3].Value = finantial.Full_amount.ToString("n2");
                dataGridView.Rows[n].Cells[4].Value = finantial.Transport_amount.ToString("n2");

                dataGridView.Rows[n].Cells[5].Value = finantial.Previous_month_due_amount.ToString("n2");

                dataGridView.Rows[n].Cells[6].Value = finantial.Supplement.Advance.ToString("n2");
                dataGridView.Rows[n].Cells[7].Value = finantial.Supplement.Fertiliser.ToString("n2");
                dataGridView.Rows[n].Cells[8].Value = finantial.Supplement.Tea_powder.ToString("n2");
                dataGridView.Rows[n].Cells[9].Value = finantial.Supplement.Other.ToString("n2");

                dataGridView.Rows[n].Cells[10].Value = finantial.Full_subtraction_amount.ToString("n2");
                dataGridView.Rows[n].Cells[11].Value = finantial.Net_amount.ToString("n2");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //set your button column index instead of 13
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                try
                {
                    //dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

                    receipt_view fm = new receipt_view();
                    receipt_print cr = new receipt_print();

                    TextObject chashier = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["txt_chashier"];
                    chashier.Text = "admin";
                    TextObject date = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["txt_date"];
                    date.Text = DateTimePicker.Value.ToString("yyyy'-'MM");

                    TextObject idNo = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_idNo"];
                    idNo.Text = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    TextObject name = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_name"];
                    name.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                    TextObject netWeight = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_netWeight"];
                    netWeight.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                    TextObject fullAmount = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["txt_fullAmount"];
                    fullAmount.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();

                    TextObject transport = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_transport"];
                    transport.Text = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                    TextObject due = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_due"];
                    due.Text = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                    TextObject advance = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_advannce"];
                    advance.Text = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                    TextObject fertilizer = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_fertilizer"];
                    fertilizer.Text = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                    TextObject teaPowder = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_teaPowder"];
                    teaPowder.Text = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                    TextObject otherAmount = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_otherAmount"];
                    otherAmount.Text = dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();


                    TextObject fullSubtractionAmount = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_fullSubtractionAmount"];
                    fullSubtractionAmount.Text = dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                    TextObject netAmount = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["txt_netAmount"];
                    netAmount.Text = dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();

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