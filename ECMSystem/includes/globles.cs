using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMSystem.includes
{
    class globles
    {
        //by sandakelum priyamantha
        private static string _user_id, _f_name, _nic, _pw, _user_type;

        public static string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        public static string f_name
        {
            get { return _f_name; }
            set { _f_name = value; }
        }

        public static string nic
        {
            get { return _nic; }
            set { _nic = value; }
        }

        public static string pw
        {
            get { return _pw; }
            set { _pw = value; }
        }

        public static string user_type
        {
            get { return _user_type; }
            set { _user_type = value; }
        }




    }
}
