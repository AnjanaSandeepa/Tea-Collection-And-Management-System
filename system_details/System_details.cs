using Microsoft.VisualBasic;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tea_collection_system
{
     class System_details : Input_tables
     {
        Database_connector db = new Database_connector();
        String query;
        DataSet ds;

        private int id;
        private String year_month;
        private double price_of_1Kg;
        private float subtraction_weight_percentage;
        private double transport_cost_of_1kg;
        

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Year_month
        {
            get { return year_month; }
            set { year_month = value; }
        }

        public double Price_of_1Kg
        {
            get { return price_of_1Kg; }
            set { price_of_1Kg = value; }
        }

        public float Subtraction_weight_percentage
        {
            get { return subtraction_weight_percentage; }
            set { subtraction_weight_percentage = value; }
        }

        public double Transport_cost_of_1kg
        {
            get { return transport_cost_of_1kg; }
            set { transport_cost_of_1kg = value; }
        }
        
        public void write_dataBase(string yearMonth, double priceOf1KG, float SubtractionWeightPercentage, double TransportCost)
        {
            try
            {
                query = "INSERT INTO `system_details` (`system_details_id`, year_and_month`, `price_of_1kg`, `subtraction_weight_percentage`, `transport_cost_percentage`) VALUES ('null','" + yearMonth + "', '" + priceOf1KG + "', '" + SubtractionWeightPercentage + "','" + TransportCost + "')";
                db.setData(query, "සාර්තකව දත්ත ඇතුලත් කරන ලදී");
            }
            catch (Exception)
            {
                MessageBox.Show("Database Write Error (e - 00020)", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void update_dataBase(int id,string yearMonth, double priceOf1KG, float SubtractionWeightPercentage, double TransportCost)
        {
            try 
            {
                query = "UPDATE `system_details` SET `price_of_1kg`= '" + priceOf1KG + "',`subtraction_weight_percentage`= '" + SubtractionWeightPercentage + "',`transport_cost_percentage`= '" + TransportCost + "' WHERE `system_details_id`= '" + id + "' AND `year_and_month`= '" + yearMonth + "'";
                db.setData(query, "සාර්තකව දත්ත යාවත්කාලීන කරන ලදී");
            }
            catch (Exception)
            {
                MessageBox.Show("Database update Error (e - 00021)", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

		public DataSet read_dataBase()
		{
            query = "SELECT * FROM `system_details`";

			ds = db.GetData(query);
			return ds;
		}

		public DataSet read_dataBase(String year_month)
        {	
			query = "SELECT * FROM `system_details` WHERE `year_and_month`= '" + year_month + "' ";
			          
	        ds = db.GetData(query);
            return ds;
        }
    }
}
