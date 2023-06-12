using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tea_collection_system
{
    class Sub_Finantial : Input_tables
    {
        Database_connector db = new Database_connector();
        String query;

        private Users supplier; //id,name
        private Suppliment supplement; //advance,fertiliser,tea_powder, other.
        private Input weight; //full_weight , net_weight
        private System_details systemData; //price_of_1Kg, transport_cost_of_1Kg, subtraction_weight_precentage

        private int due_id;
        private double full_amount;
        private double net_amount;
        private double transport_amount;
        private double previous_month_due_amount;
        private double full_subtraction_amount;
        private DateTime date_time;


        public int Due_id
        {
            get { return due_id; }
            set { due_id = value; }
        }

        public Users Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        public Suppliment Supplement
        {
            get { return supplement; }
            set { supplement = value; }
        }

        public Input Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public System_details SystemData
        {
            get { return systemData; }
            set { systemData = value; }
        }


        public double Full_amount
        {
            get { return full_amount; }
            set { full_amount = value; }
        }

        public double Net_amount
        {
            get { return net_amount; }
            set { net_amount = value; }
        }

        public double Transport_amount
        {
            get { return transport_amount; }
            set { transport_amount = value; }
        }

        public double Previous_month_due_amount
        {
            get { return previous_month_due_amount; }
            set { previous_month_due_amount = value; }
        }

        public double Full_subtraction_amount
        {
            get { return full_subtraction_amount; }
            set { full_subtraction_amount = value; }
        }

        public DateTime Date_time
        {
            get { return date_time; }
            set { date_time = value; }
        }


        public void write_dataBase(int due_id, int user_id, String date, double due)
        {
            query = "INSERT INTO `month_due` (`due_id`,`user_id`, `year_month`, `due_amount`) VALUES('" + due_id + "','" + user_id + "', '" + date + "', '" + due + "')";
            db.setData(query, "added Successfull");
        }

        public void update_dataBase(int due_id, int user_id, String date, double due)
        {
            query = "UPDATE month_due SET `due_amount` = '" + due + "'   where `user_id`= '" + user_id + "'  AND `year_month`= '" + date + "' ";
            db.setData(query, "update Successfull");

        }

        public void delete_dataBase(int data_id)
        {
          
        }

        public DataSet read_dataBase()
        {
            query = "SELECT * FROM `month_due`";
            DataSet ds = db.GetData(query);
            return ds;
        }

        public DataSet read_dataBase(int user_id, String year_month)
        {
            query = "SELECT * FROM `month_due` WHERE `year_and_month`= '" + year_month + "' AND user_id = '" + user_id + "' ";
            DataSet ds = db.GetData(query);
            return ds;
        }
    }
}
