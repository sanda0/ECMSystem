using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ECMSystem.model
{
    class Subject
    {
        //by sandakelum priyamantha
        includes.DBhelper db = new includes.DBhelper();
        public string id { get; set; }
        public string name { get; set; }

        public int save()
        {
            string q = "insert into subject values('" + name + "')";
            int r = db.execute_qry(q);
            return r;
        }

        public int find(string sid)
        {
            db.open();
            try
            {
                SqlDataReader dr = db.find("subject", sid);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr["id"].ToString();
                        name = dr["name"].ToString();                        
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

        public int delete(string sid)
        {
            string q = "delete from subject where id = '" + sid + "'";
            int r = db.execute_qry(q);

            return r;
        }

        public DataTable filter(string by = null, string key = "")
        {

            string q = "select id as 'ID' , name as 'Name' from subject ";
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

    }


}
