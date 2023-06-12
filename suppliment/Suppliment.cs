using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tea_collection_system
{
    class Suppliment : Input_tables
    {
        Database_connector db = new Database_connector();
        String query;
        DataSet ds;

        private int supliment_id;
        private Users transactor;
        private double advance;
        private double fertiliser;
        private double tea_powder;
        private double other;
        private DateTime date_time;

        public int Supliment_id
        {
            get { return supliment_id; }
            set { supliment_id = value; }
        }

        public Users Transactor
        {
            get { return transactor; }
            set { transactor = value; }
        }

        public DateTime Date_time
        {
            get { return date_time; }
            set { date_time = value; }
        }

        public double Advance
        {
            get { return advance; }
            set { advance = value; }
        }

        public double Fertiliser
        {
            get { return fertiliser; }
            set { fertiliser = value; }
        }

        public double Tea_powder
        {
            get { return tea_powder; }
            set { tea_powder = value; }
        }

        public double Other
        {
            get { return other; }
            set { other = value; }
        }


        public void write_dataBase(int id, DateTime dateTime, double advance, double fertilizer, double tea_powder, double other)
        {
            try
            {
                query = "INSERT INTO `supplement` (`supplement_id`, `user_id`, `date_time`, `advance`, `fertilizer`, `tea_powder`, `other`) VALUES('null', '" + id + "', '" + dateTime.ToString("yyyy'-'MM'-'dd HH':'mm tt") + "', '" + advance + "', '" + fertiliser + "', '" + tea_powder + "', '" + other + "')";
                db.setData(query, "added Successfull");
            }
            catch (Exception)
            {
                MessageBox.Show("some Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


		public DataSet read_dataBase()
		{
			query = "select supplement.user_id , supplement.date_time , user.name , supplement.advance  , supplement.fertilizer , supplement.tea_powder , supplement.other " +
				"from user , supplement " +
				"where user.user_id = supplement.user_id ";

			ds = db.GetData(query);
			return ds;
		}

		public DataSet read_dataBase(String year_month)
		{
			query = "select supplement.user_id , supplement.date_time , user.name , supplement.advance  , supplement.fertilizer , supplement.tea_powder , supplement.other " +
				"from user , supplement " +
				"where user.user_id = supplement.user_id " +
				"AND DATE_FORMAT(supplement.date_time, '%Y-%m') = '" + year_month + "' ";

			ds = db.GetData(query);
			return ds;
		}

		public DataSet sRead_dataBase(String year_month , String userId)
        {
			query = "select supplement.user_id , supplement.date_time , user.name , supplement.advance  , supplement.fertilizer , supplement.tea_powder , supplement.other " +
				"from user , supplement " +
				"where user.user_id = supplement.user_id " +
				"AND DATE_FORMAT(supplement.date_time, '%Y-%m') = '" + year_month + "' " +
				"AND supplement.user_id like '" + userId + "%' ";

			ds = db.GetData(query);
			return ds;
        }

		public DataSet sRead_dataBase(string userId)
		{
			query = "select supplement.user_id , supplement.date_time , user.name , supplement.advance  , supplement.fertilizer , supplement.tea_powder , supplement.other " +
				"from user , supplement " +
				"where user.user_id = supplement.user_id " +
				"AND supplement.user_id like '" + userId + "%' ";

			ds = db.GetData(query);
			return ds;
		}


		/* public DataSet read_dataBase(string user_id)
        {
	    query = "Select * from supplement where user_id = '" + user_id + "'";
	    DataSet ds = db.GetData(query);
	    return ds;
        }

        public DataSet read_dataBase(int user_id, DateTime date)
        {
	    query = "Select * from supplement where user_id = '" + user_id + "' AND date_time = '" + date + "'";
	    DataSet ds = db.GetData(query);
	    return ds;
        }*/


		//transaction
		public DataSet read_dataBase(String month, String year, int user_id)
		{
			query = "Select * from supplement WHERE month(date_time)= '" + month + "' and year(date_time) = '" + year + "' AND user_id = '" + user_id + "' ";
			DataSet ds = db.GetData(query);
			return ds;
		}

	}
}
