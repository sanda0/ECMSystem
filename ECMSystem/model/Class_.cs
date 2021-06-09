using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMSystem.model
{
    class Class_
    {
        //by sandakelum priyamantha
        includes.DBhelper db = new includes.DBhelper();
        includes.Util util = new includes.Util();
        model.Payment payment = new model.Payment();
        model.Stu_class stu_Class = new model.Stu_class();


        public string id { get; set; }
        public string name { get; set; }
        public string day { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string fee { get; set; }
        public string teacher_id { get; set; }
        public string subject_id { get; set; }


        public int find(string cid)
        {
           
            try
            {
                string[,] data = util.DTto2D(filter("class.id", cid));
                id = data[0, 0];
                name = data[0, 1];
                day = data[0, 2];
                start_time = data[0, 3];
                end_time = data[0, 4];
                fee = data[0, 5];
                teacher_id = data[0, 6];
                subject_id = data[0, 7];

                return 1;

            }
            catch (Exception)
            {
                return 0;
            }
        
        }

        public int save()
        {
            string q = "insert into class values('" + name + "','"+day+"','"+start_time+"','"+end_time+"','"+fee+"','"+teacher_id+"','"+subject_id+"')";
            int r = db.execute_qry(q);
            return r;
        }

        public DataTable filter(string by = null, string key = "")
        {

            string q = " select class.id as 'ID',class.name as 'Class Name',class.day_ as 'Day',class.start_time as 'Start Time',class.end_time as 'End Time',class.free as 'Fee',teacher.name as 'Teacher' ,subject.name as 'Subject' from class,teacher,subject where teacher.id = class.teacher_id and subject.id = class.subject_id  ";
            if (by != null)
            {
                q = q + " and " + by + " like " + "'" + key + "%'";
                return db.filter(q);

            }
            else
            {
                return db.filter(q);
            }

        }

        public int update()
        {
            string q = "update class set name='"+name+"' , day_='"+day+"',start_time='"+start_time+"',end_time='"+end_time+"',free='"+fee+"',teacher_id='"+teacher_id+"',subject_id='"+subject_id+"' where id = '"+id+"' ";
            int r = db.execute_qry(q);
            return r;
        }

        public int delete(string cid)
        {
            payment.delete(cid: cid);
            stu_Class.delete(cid: cid);
            string q = "delete from class where id = '" + cid + "'";
            int r = db.execute_qry(q);

            return r;
        }


    }
}
