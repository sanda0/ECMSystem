using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECMSystem.model
{
    class Payment
    {
        includes.DBhelper db = new includes.DBhelper();
        includes.Util util = new includes.Util();

        //public string id { get; set; }
        public string class_id { get; set; }
        public string student_id { get; set; }
        //public string date { get; set; }

        public int save(string sid = null, string cid = null)
        {
            if (sid == null && cid == null)
            {
                string q = "insert into payment (class_id,student_id) values('" + class_id + "','" + student_id + "')";
                int r = db.execute_qry(q);
                return r;
            }
            else
            {
               
                string q = "insert into payment (class_id,student_id) values('" + cid + "','" + sid + "')";
                int r = db.execute_qry(q);
                return r;
            }

        }


        public int delete(string sid = null, string cid = null)
        {

                if (sid != null)
                {
                    int r = db.execute_qry("delete from payment where student_id = '" + sid + "' ");
                    return r;
                }
                else if (cid != null)
                {
                    int r = db.execute_qry("delete from payment where class_id = '" + cid + "' ");
                    return r;
                }
                else
                {
                    return 0;
                }
        }


        public bool checkPayment(string sid = null, string cid = null)
        {
            DateTime sDate = DateTime.Now;
            string y = sDate.Year.ToString();
            string m = sDate.Month.ToString();
            try
            {
                _ = util.DTto2D(db.filter("select * from payment where class_id='" + cid + "' and student_id = '" + sid + "' and month(date) = '" + m + "' and year(date)= '" + y + "' "))[0, 0];
                return true;

            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
