using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace tea_collection_system
{
    class Input : Input_tables
    {
        Database_connector db = new Database_connector();
        private String query;

        private int input_id;
        private Users supplier;                   
        private System_details system_data;          
        private double full_weight;
        private double net_weight;
        private DateTime date_time;

        public int Input_id
        {
            get { return input_id; }
            set { input_id = value; }
        }

        public Users Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        public System_details System_data
        {
            get { return system_data; }
            set { system_data = value; }
        }

        public double Net_weight
        {
            get { return net_weight; }
            set { net_weight = value; }
        }

        public double Full_weight
        {
            get { return full_weight; }
            set { full_weight = value; }
        }

        public DateTime Date_time
        {
            get { return date_time; }
            set { date_time = value; }
        }



        public void write_dataBase(int supplierID, double FullWeight)
        {
            try
            {
                query = " INSERT INTO `input` (`input_id`, `user_id`, `date_time`, `full_weight`) VALUES ('null', '" + supplierID + "', '" + Date_time.ToString("yyyy'-'MM'-'dd HH':'mm tt") + "','" + FullWeight + "') ";
                db.setData(query, "සාර්තකව දත්ත ඇතුලත් කරන ලදී.");
            }
            catch (Exception)
            {
                MessageBox.Show("(e-00029)", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void delete_dataBase(int inputId)
        {
            if (MessageBox.Show("Are you Suver?.", "Delete conform !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    query = "DELETE FROM `input` WHERE `input_id` = '" + inputId + "'";
                    db.setData(query, "සාර්තකව දත්ත මකා දමන ලදී");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



		public DataSet read_dataBase()
        {
			query = "select input.input_id , input.date_time , input.user_id , user.name , input.full_weight , (input.full_weight * (1 - (system_details.subtraction_weight_percentage / 100))) , system_details.year_and_month , system_details.subtraction_weight_percentage , system_details.price_of_1kg , system_details.transport_cost_percentage " +
                "from system_details, input , user " +
                "where system_details.year_and_month = DATE_FORMAT(input.date_time, '%Y-%m') " +
                "AND user.user_id = input.user_id ";

			DataSet ds = db.GetData(query);
			return ds;
		}

		public DataSet read_dataBase(String yearMonth)
		{
			
			query = "select input.input_id , input.date_time , input.user_id , user.name , input.full_weight , (input.full_weight * (1 - (system_details.subtraction_weight_percentage / 100))) , system_details.year_and_month , system_details.subtraction_weight_percentage , system_details.price_of_1kg , system_details.transport_cost_percentage " +
                "from system_details, input , user " +
                "where system_details.year_and_month = DATE_FORMAT(input.date_time, '%Y-%m') " +
                "AND system_details.year_and_month = '" + yearMonth + "' " +
                "AND user.user_id = input.user_id ";
			DataSet ds = db.GetData(query);
			return ds;
		}

        public DataSet S_read_dataBase(String user_id)
        {
			query = "select input.input_id , input.date_time , input.user_id , user.name , input.full_weight , (input.full_weight * (1 - (system_details.subtraction_weight_percentage / 100))) , system_details.year_and_month , system_details.subtraction_weight_percentage , system_details.price_of_1kg , system_details.transport_cost_percentage " +
                "from system_details, input , user " +
                "where system_details.year_and_month = DATE_FORMAT(input.date_time, '%Y-%m') " +
                "AND user.user_id = input.user_id " +
                "AND user.user_id like '" + user_id + "%' "; //a%";
			DataSet ds = db.GetData(query);
			return ds;
		}

        public DataSet S_read_dataBase(String yearMonth, String user_id)
        {
			query = "select input.input_id , input.date_time , input.user_id , user.name , input.full_weight , (input.full_weight * (1 - (system_details.subtraction_weight_percentage / 100))) , system_details.year_and_month , system_details.subtraction_weight_percentage , system_details.price_of_1kg , system_details.transport_cost_percentage " +
                "from system_details, input , user " +
                    "where system_details.year_and_month = DATE_FORMAT(input.date_time, '%Y-%m') " +
                    "AND user.user_id = input.user_id " +
                    "AND system_details.year_and_month = '" + yearMonth + "' " +
                    "AND user.user_id like '" + user_id + "%' "; //a%";

			DataSet ds = db.GetData(query);
			return ds;
		}




		/*public DataSet read_dataBase(String user_id)
		{
			query = "select input.input_id , input.date_time , input.user_id , user.name , input.full_weight , (input.full_weight * (1 - (system_details.subtraction_weight_percentage / 100))) , system_details.year_and_month , system_details.subtraction_weight_percentage , system_details.price_of_1kg , system_details.transport_cost_percentage from system_details, input , user where system_details.year_and_month = DATE_FORMAT(input.date_time, '%Y-%m') AND user.user_id = input.user_id AND user.user_id = '" + user_id + "' ";
			DataSet ds = db.GetData(query);
			return ds;
		}*/


		/*public DataSet read_dataBase(int user_id, DateTime date_time)
        {
			query = "select input.input_id , input.date_time , input.user_id , user.name , input.full_weight , (input.full_weight * (1 - (system_details.subtraction_weight_percentage / 100))) , system_details.year_and_month , system_details.subtraction_weight_percentage , system_details.price_of_1kg , system_details.transport_cost_percentage from system_details, input , user where system_details.year_and_month = DATE_FORMAT(input.date_time, '%Y-%m') AND system_details.year_and_month = '" + date_time + "' AND user.user_id = input.user_id AND user.user_id = '" + user_id + "' ";
			DataSet ds = db.GetData(query);
			return ds;
		}*/


		//****************finantial**********************//
		public DataSet sum_full_weight_dataBase(String month, String year, int user_id_Search)
        {
            query = "SELECT SUM(`full_weight`) FROM `input` WHERE month(`date_time`)= '" + month + "' AND year(`date_time`) = '" + year + "' AND `user_id` = '" + user_id_Search + "' ";
            DataSet ds = db.GetData(query);
            return ds;
        }

		public DataSet read_dataBase(String month, String year, int user_id)
		{
			query = "Select * from input WHERE month(date_time)= '" + month + "' and year(date_time) = '" + year + "' and user_id  like '" + user_id + "' ";
			DataSet ds = db.GetData(query);
			return ds;
		}
	}
}
