using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMSystem.model
{
    class Stu_class
    {
        //by sandakelum priyamantha
        includes.DBhelper db = new includes.DBhelper();
        includes.Util util = new includes.Util();

        public string student_id { get; set; }
        public string class_id { get; set; }
        public string class_name { get; set; }
        public string date { get; set; }
        public string cno { get; set; }

        public int save(string c_no,string sid = null,string cid = null)
        {
            if(sid == null && cid == null)
            {
                string q = "insert into stu_class (student_id,class_id,c_no) values('" + student_id + "','" + class_id + "','" + c_no + "')";
                int r = db.execute_qry(q);
                return r;
            }
            else
            {
                string q = "insert into stu_class (student_id,class_id,c_no) values('" + sid + "','" + cid + "','" + c_no + "')";
                int r = db.execute_qry(q);
                return r;
            }
            
        }

        public DataTable filter(string by=null,string key = null,string c_no= null)
        {
            string q = "select stu_class.student_id,stu_class.class_id,class.name,stu_class.date,stu_class.c_no from class,stu_class where class.id = stu_class.class_id ";
            if(by != null)
            {
                q = q + " and " + by + " = '" + key + "'";
                if (c_no == null)
                {
                    return db.filter(q);
                }
                else
                {
                    q = q + " and stu_class.c_no = '" + c_no + "'";
                    return db.filter(q);
                }
                

            }
            else
            {
                if (c_no == null)
                {
                    return db.filter(q);
                }
                else
                {
                    q = q + " and stu_class.c_no = '" + c_no + "'";
                    return db.filter(q);
                }
            }

        }

        public int find(string sid,string c_no)
        {
            try
            {
                string[,] data = util.DTto2D(filter("stu_class.student_id", sid, c_no));
                student_id = data[0, 0];
                class_id = data[0, 1];
                class_name = data[0, 2];
                date = data[0, 3];
                cno = data[0, 4];

                return 1;
            }catch (Exception)
            {
                return 0;
            }



        }

        public int delete(string sid=null,string cid=null,string c_no=null)
        {
            if(c_no == null)
            {
                c_no = "%";
            }

            if(sid != null)
            {
                int r = db.execute_qry("delete from stu_class where student_id = '" + sid + "' and c_no like '" + c_no + "'");
                return r;
            }else if( cid != null)
            {
                int r = db.execute_qry("delete from stu_class where class_id = '" + cid + "' and c_no like '" + c_no + "'");
                return r;
            }
            else
            {
                return 0;
            }
        }

        public int update(string c_no,string sid, string cid)
        {
            if (sid != null)
            {
                int r = db.execute_qry("update stu_class set class_id = '"+cid+"' where student_id = '"+sid+"' and c_no = '"+c_no+"'");
                return r;
            }
            else
            {
                return 0;
            }
        }




    }
}
