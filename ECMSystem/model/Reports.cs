using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMSystem.model
{

    class Reports
    {
        includes.Util util = new includes.Util();
        includes.DBhelper db = new includes.DBhelper();

        public double[] getTotalPayment(string y,string m,string cid)
        {
            double[] data = new double[2];

            try
            {
                data[0] = double.Parse(util.DTto2D(db.filter("select sum(class.free) as 'sum' from student,class,stu_class where student.id = stu_class.student_id and stu_class.class_id = '"+cid+"' and class.id = '"+cid+"'"))[0, 0]);
            }
            catch (Exception)
            {
                data[0] = 0;
            }
            try
            {
                data[1] = double.Parse(util.DTto2D(db.filter("select sum(class.free) as 'get' from student,class,payment where student.id = payment.student_id and class.id = '"+cid+"' and payment.class_id='"+cid+"' and year(payment.date) = '"+y+"' and month(payment.date) = '"+m+"'"))[0, 0]);
            }
            catch (Exception)
            {
                data[1] = 0;
            }

            return data;
           
        }


        public DataTable getClass(string table)
        {
            return db.filter("select distinct class.id,class.name  from class,"+table+" where class.id = " + table + ".class_id");
        }

        public DataTable getClassToday(string table)
        {
            return db.filter("select distinct class.id,class.name  from class," + table + " where class.id = " + table + ".class_id and class.day_ = '"+DateTime.Now.DayOfWeek.ToString()+"'");
        }

        public DataTable getYearOrMonthList(string what,string table)
        {
            return db.filter("select distinct  "+what+"(date) from "+table);
        }

        public DataTable getPaiedOrUnpaiedList(bool paied,string cid,string y,string m)
        {
            if (paied)
            {

                return db.filter("select student.nic as 'NIC' ,student.f_name as 'Student First Name',class.name as 'Class Name',class.free as 'Class fee' from student,class where student.id  in (select student_id from payment where class_id = '" + cid + "' and YEAR(date) = '" + y + "' and MONTH(date) = '" + m + "') and class.id = '" + cid + "'");

            }
            else
            {
                return db.filter("select student.nic as 'NIC' ,student.f_name as 'Student First Name',class.name as 'Class Name',class.free as 'Class fee' from student,class where student.id not in (select student_id from payment where class_id = '"+cid+"' and YEAR(date) = '"+y+"' and MONTH(date) = '"+m+"') and class.id = '"+cid+"'");

            }
        }


        public int[] getTotalAttendance(string y, string m, string cid)
        {
            int[] data = new int[2];

            try
            {
                data[0] = int.Parse(util.DTto2D(db.filter("select count(student.id) as 'tot' from student,class,stu_class where student.id = stu_class.student_id and stu_class.class_id = '"+cid+"' and class.id = '"+cid+"'"))[0, 0]);
            }
            catch (Exception)
            {
                data[0] = 0;
            }
            try
            {
                data[1] = int.Parse(util.DTto2D(db.filter("select count(student.id) as 'now' from student,class,attendance where student.id = attendance.student_id and class.id = '"+cid+"' and attendance.class_id='"+cid+"' and year(attendance.date) = '"+y+"' and month(attendance.date) = '"+m+"'"))[0, 0]);
            }
            catch (Exception)
            {
                data[1] = 0;
            }

            return data;

        }

        public DataTable getAttendOrNotList(bool attend, string cid, string y, string m)
        {
            if (attend)
            {

                return db.filter("select student.nic as 'NIC',student.f_name as 'Student First Name',class.name as 'Class Name',class.start_time as 'Class Start Time', convert(varchar, attendance.date, 106) as 'Attend Date',  convert(varchar, attendance.time, 8) as 'Attend Time' from stu_class, student,class,attendance where student.id = attendance.student_id and stu_class.student_id = student.id and stu_class.class_id ='" + cid+"' and attendance.class_id = '"+cid+"' and class.id = '"+cid+"' and year(attendance.date) = '"+y+"' and month(attendance.date) = '"+m+"'");

            }
            else
            {
                return db.filter("select student.nic as 'NIC',student.f_name as 'Student First Name',class.name as 'Class Name',class.start_time as 'Class Start Time' from stu_class, student,class,attendance where student.id != attendance.student_id and stu_class.student_id = student.id and stu_class.class_id ='" + cid + "' and attendance.class_id = '" + cid + "' and class.id = '" + cid + "' and year(attendance.date) = '" + y + "' and month(attendance.date) = '" + m + "'");

            }
        }

        public DataTable getClassesIncome(string y,string m)
        {
            return db.filter("select class.id as 'Class id',class.name as 'Class Name',sum(class.free) as 'Imcome' from class,payment where payment.class_id = class.id and year(payment.date) = '" + y + "' and month(payment.date) = '" + m + "' group by class.id,class.name");
        }



        public int[] getTodayAttendance( string cid)
        {

            int[] data = new int[2];

            try
            {
                data[0] = int.Parse(util.DTto2D(db.filter("select count(student.id) as 'tot' from student,class,stu_class where student.id = stu_class.student_id and stu_class.class_id = '" + cid + "' and class.id = '" + cid + "'"))[0, 0]);
            }
            catch (Exception)
            {
                data[0] = 0;
            }
            try
            {
                data[1] = int.Parse(util.DTto2D(db.filter("select count(student.id) as 'now' from student,class,attendance where student.id = attendance.student_id and class.id = '" + cid + "' and attendance.class_id='" + cid + "' and attendance.date = '" + DateTime.Now.ToShortDateString() + "'"))[0, 0]);
            }
            catch (Exception)
            {
                data[1] = 0;
            }

            return data;

        }


    }
}
