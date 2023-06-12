using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tea_collection_system
{
    class Database_connector
    {
        const string connection_string = "server=localhost; port =3306; database=tea_collection_system; user id= root; Password = ";
        MySqlConnection con;
        DataSet ds;
        MySqlCommand cmd;
        MySqlDataAdapter da;

        protected MySqlConnection getConnection()
        {
        reconnect1:

            try
            {
                con = new MySqlConnection();
                con.ConnectionString = connection_string;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                goto reconnect1;
            }
            return con;
        }

        public DataSet GetData(String query)
        {
        reconnect2:

            ds = new DataSet();
            try
            {
                con = getConnection();
                cmd = new MySqlCommand();

                cmd.Connection = con;
                cmd.CommandText = query;
                da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                string message = ex.Message + "\n\t\t\t දත්ත ලබා ගැනීමේ අපහසුවක් පවතී. \n    Database නිවැරදිව ක්‍රියාත්මක වන්නේදැයි බලන්න";
                string title = "ඉවත් වෙනවාද?";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    System.Environment.Exit(1);
                }
                else
                {
                    goto reconnect2;
                }
            }
            return ds;
        }

        public void setData(String query)
        {
                con = getConnection();
                cmd = new MySqlCommand();

                cmd.Connection = con;
                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                con.Close();
        }

        public void setData(String query, String msg)
        {
                con = getConnection();
                cmd = new MySqlCommand();

                cmd.Connection = con;
                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
