using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ECMSystem.model
{
    class teacher
    {
        //by sandakelum priyamantha
        includes.DBhelper db = new includes.DBhelper();
        public string id { get; set; }
        public string name { get; set; }
        public string tp { get; set; }


        public DataTable filter(string by = null, string key = "")
        {

            string q = "select id as 'ID' , name as 'Name' , tp as 'Telephone' from teacher ";
            if (by != null)
            {
                q = q + " where " + by + " like " + "'" + key + "%'";
                return db.filter(q);

            }
            else
            {
                return db.filter(q);
            }

        }

        public int save()
        {
            string q = "insert into teacher values('" + name + "','" + tp + "')";
            int r = db.execute_qry(q);
            return r;
        }

        public int find(string tid)
        {
            db.open();
            try
            {
                SqlDataReader dr = db.find("teacher", tid);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr["id"].ToString();
                        name = dr["name"].ToString();
                        tp = dr["tp"].ToString();
                    }

                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                db.close();
            }
        }


        public int delete(string tid)
        {
            string q = "delete from teacher where id = '" + tid + "'";
            int r = db.execute_qry(q);

            return r;
        }

        public int update()
        {
            string q = "update teacher set name='" + name + "' ,tp = '" + tp + "' where id = '" + id + "' ";
            int r = db.execute_qry(q);
            return r;
        }

    }
}
