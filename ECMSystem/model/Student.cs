using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMSystem.model
{
    class Student
    {
        //by sandakelum priyamantha
        includes.DBhelper db = new includes.DBhelper();
        includes.Util util = new includes.Util();
        model.Stu_class stu_Class = new model.Stu_class();
        model.Class_ class_ = new model.Class_();
        model.Payment payment = new model.Payment();
        

        public string id { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string dob { get; set; }
        public string nic { get; set; }
        public string tp { get; set; }
        public string addr { get; set; }
        public string gender { get; set; }


        public string clz1 { get; set; }
        public string clz2 { get; set; }
        public string clz3 { get; set; }

        public string barcode_path { get; set; }


        public int save()
        {
            string q = "insert into student  (f_name,l_name,dob,nic,tp,addr,gender,barcode_path) values('" + f_name+"','"+l_name+"','"+dob+"','"+nic+"','"+tp+"','"+addr+"','"+gender+"','"+barcode_path+"');";
            int r = db.execute_qry(q);
            return r;
        }

        public int delete(string sid)
        {
            payment.delete(sid: sid);
            stu_Class.delete(sid: sid);
            //string q = "delete from stu_class where student_id = '" + sid + "'";
            string q1 = "delete from student where id = '" + sid + "'";
            //int r = db.execute_qry(q);
            int  r = db.execute_qry(q1);
            return r;
        }

        public DataTable filter(string by = null, string key = "", string sd = null, string ed = null)
        {

            string q = "select * from student";
            if (by != null)
            {
                
                q = q + " where " + by + " like " + "'" + key + "%'";
                return db.filter(q);


            }else if (sd != null && ed != null)
            {
                q = q + " where regi_date between '" + sd + "' and '" + ed + "'";
                return db.filter(q);
            }
            else
            {
                return db.filter(q);
            }

        }

        public bool nic_in_table(string nic)
        {
            if (db.getOneColumn("student","nic").Contains(nic))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataTable filterByclass(string class_id)
        {
            string q = "select student.*,class.name from student,stu_class,class where stu_class.student_id = student.id and stu_class.class_id = class.id and class.id = '"+class_id+"'";
            return db.filter(q);
        }

        public void setData(string[,] data)
        {

            id = data[0, 0];
            f_name = data[0, 1];
            l_name = data[0, 2];
            dob = data[0, 3];
            nic = data[0, 4];
            tp = data[0, 5];
            addr = data[0, 6];
            gender = data[0, 7];

            model.Stu_class stu_Class = new model.Stu_class();
            stu_Class.find(id, "c1");
            clz1 = stu_Class.class_name;

            model.Stu_class stu_Class2 = new model.Stu_class();
            stu_Class2.find(id, "c2");
            clz2 = stu_Class2.class_name;

            model.Stu_class stu_Class3 = new model.Stu_class();
            stu_Class3.find(id, "c3");
            clz3 = stu_Class3.class_name; 

        }

        public int find(string sid=null,string nic_ = null)
        {



            try
            {
                
                if (sid != null)
                {
                    string[,]  data = util.DTto2D(filter(by: "id", key: sid));
                    setData(data);

                    return 1;


                }
                else if (nic_ != null)
                {
                    string[,] data = util.DTto2D(filter(by: "nic", key: nic_));
                    setData(data);
                    return 1;
                }
                else
                {
                    return 0;
                }
               
                

            }catch (Exception)
            {
                return 0;
            }
 
        }

        public int update()
        {

            string q1 = "update student set f_name='"+f_name+"' ,l_name='"+l_name+"',dob='"+dob+"',tp='"+tp+"',addr='"+addr+"' where id = '"+id+"'";
            int r = db.execute_qry(q1);
            return r;
        }

    }
}
