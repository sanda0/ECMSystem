using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMSystem.model
{
    class Attendance
    {
        includes.DBhelper db = new includes.DBhelper();
        //includes.Util util = new includes.Util();

       
        public string class_id { get; set; }
        public string student_id { get; set; }
       

        public int save(string sid = null, string cid = null)
        {
            if (sid == null && cid == null)
            {
                string q = "insert into attendance (class_id,student_id) values('" + class_id + "','" + student_id + "')";
                int r = db.execute_qry(q);
                return r;
            }
            else
            {

                string q = "insert into attendance (class_id,student_id) values('" + cid + "','" + sid + "')";
                int r = db.execute_qry(q);
                return r;
            }

        }


        public int delete(string sid = null, string cid = null)
        {

            if (sid != null)
            {
                int r = db.execute_qry("delete from attendance where student_id = '" + sid + "' ");
                return r;
            }
            else if (cid != null)
            {
                int r = db.execute_qry("delete from attendance where class_id = '" + cid + "' ");
                return r;
            }
            else
            {
                return 0;
            }
        }

        public DataTable getToday()
        {
            DateTime sDate = DateTime.Now;
            return db.filter("select student.nic as 'NIC',student.f_name as 'First Name',class.name as 'Class Name',convert(varchar,class.start_time, 0) as 'Class Start time',convert(varchar,attendance.time, 0)  as 'Time' from attendance,class,student where attendance.class_id = class.id and attendance.student_id = student.id and attendance.date = '" + sDate.ToShortDateString()+"'");

        }
    }
}
