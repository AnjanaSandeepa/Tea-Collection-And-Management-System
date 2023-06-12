using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tea_collection_system
{
    public abstract class Input_table
    {
        protected DateTime date_time;

        public Input_table()
        {
            date_time = DateTime.Parse(DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm tt"));
        }

        public abstract void read_dataBase();
        public abstract void write_dataBase();
        public abstract void update_dataBase();

    }
}
