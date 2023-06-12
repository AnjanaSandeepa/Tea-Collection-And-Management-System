using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tea_collection_system
{
    class Users : Input_tables
    {
        Database_connector db = new Database_connector();
        String query;

        private int id;
        private String name;
        private String address;
        private String contact;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        public void write_dataBase(string name, string contact, string address)
        {
            try
            {
                query = "INSERT INTO user(`user_id`, `name`, `contact`, `address`) VALUES ('null', '" + name + "', '" + contact + "', '" + address + "')";
                db.setData(query, "සාර්තකව දත්ත ඇතුලත් කරන ලදී.");

            }
            catch (Exception)
            {
                MessageBox.Show("Database Write Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void update_dataBase(int userId, string name, string contact, string address)
        {
            try
            {
                query = "UPDATE `user` SET `name` = '" + name + "', `address` ='" + address + "', `contact` = '" + contact + "' where `user_id` = '" + userId + "' ";
                db.setData(query, "සාර්තකව යාවත් කාලීන කරණ ලදී");
            }
            catch (Exception)
            {
                MessageBox.Show("Database Update error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void delete_dataBase(int userId)
        {
            if (MessageBox.Show("Are you Suver?.", "Delete conform !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    query = "DELETE FROM `user` WHERE `user_id` = '" + userId + "'";
                    db.setData(query, "සාර්තකව දත්ත මකා දමන ලදී");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("පරිශීලකයා ගණුදෙනු සිදුකර ඇති බැවින් දත්ත මැකීම සිදුකල නොහැක. \n" + ex.Message);
                }
            }
        }

        //read database

        public DataSet s_read_database(string user_id)
        {
            query = "SELECT * FROM `user` WHERE `user_id` LIKE '" + user_id + "%' "; //a%";
            DataSet ds = db.GetData(query);
            return ds;
        }

        public DataSet read_dataBase_Id(String user_id)
        {
            query = "SELECT * FROM `user` WHERE `user_id` = '" + user_id + "'";
            DataSet ds = db.GetData(query);
            return ds;
        }

        public DataSet read_dataBase()
        {
            query = "SELECT * FROM `user`";
            DataSet ds = db.GetData(query);
            return ds;
        }

		public DataSet read_dataBase(int user_id)
		{
			query = "SELECT * FROM `user` WHERE `user_id` = '" + user_id + "'";
			DataSet ds = db.GetData(query);
			return ds;
		}

		public DataSet read_dataBase(String name)
		{
			query = "SELECT * FROM `user` WHERE `name` = '" + name + "'";
			DataSet ds = db.GetData(query);
			return ds;
		}
	}
}