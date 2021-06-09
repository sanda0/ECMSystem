using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ECMSystem.model
{
    class user
    {
        //by sandakelum priyamantha
        private string _user_id, _f_name, _l_name, _nic, _pw, _user_type, _dob, _tp, _gender, _address;

        includes.DBhelper db = new includes.DBhelper();

        public string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        public string f_name
        {
            get { return _f_name; }
            set { _f_name = value; }
        }
        public string l_name
        {
            get { return _l_name; }
            set { _l_name = value; }
        }

        public string nic
        {
            get { return _nic; }
            set { _nic = value; }
        }

        public string pw
        {
            get { return _pw; }
            set { _pw = value; }
        }

        public string user_type
        {
            get { return _user_type; }
            set { _user_type = value; }
        }

        public string dob
        {
            get { return _dob; }
            set { _dob = value; }
        }

        public string gender
        {
            get { return _gender; }
            set { _gender = value; }

        }
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string tp
        {
            get { return _tp; }
            set { _tp = value; }
        }





        public int check_login(string nic = "0", string pw = "0")
        {

            db.open();
            db.cmd = new SqlCommand("select id,f_name,nic,user_type,passwd from e_user where nic = '" + nic + "' and passwd = '" + pw + "'", db.con);

            try
            {
                db.dr = db.cmd.ExecuteReader();
                if (db.dr.HasRows)
                {
                    while (db.dr.Read())
                    {
                        includes.globles.user_id = db.dr["id"].ToString();
                        includes.globles.f_name = db.dr["f_name"].ToString();
                        includes.globles.nic = db.dr["nic"].ToString();
                        includes.globles.user_type = db.dr["user_type"].ToString();
                        includes.globles.pw = db.dr["passwd"].ToString();


                        return 1;
                    }
                }
                db.close();
            }
            catch (SqlException)
            {
                return 2;

            }
            finally
            {
                db.close();
            }

            return 0;


        }

        public int update()
        {
            dob = dob.Split(' ')[0];
            string q = "update e_user set f_name='" + f_name + "' ,l_name='" + l_name + "',dob = '" + dob + "',tp= '" + tp + "',user_type = '" + user_type + "',addr = '"+address+"',passwd = '"+pw+"' where id = '"+user_id+"' ";
            int r = db.execute_qry(q);
            return r;
        }

        public int save()
        {
            //insert query
           
            string q = "insert into e_user values('" + f_name + "','" + l_name + "'," +
            "'" + dob + "','" + nic + "','" + tp + "','" + address + "','" + user_type +
            "','" + gender + "','" + pw + "')";
            //

            int r = db.execute_qry(q);
          
          
            return r;
        }

        public DataTable filter(string by = null, string key = "")
        {
            
            string q = "select id  , f_name  , l_name , dob ,nic,tp,addr,gender, user_type from e_user ";
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

        public int delete(string id)
        {
            string q= "delete from e_user where id = '" + id + "'";
            int r = db.execute_qry(q);
     
            return r;
        }

        public int find(string uid)
        {


            db.open();
            try
            {
                SqlDataReader dr = db.find("e_user", uid);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        user_id = dr["id"].ToString();
                        f_name = dr["f_name"].ToString();
                        l_name = dr["l_name"].ToString();
                        nic = dr["nic"].ToString();
                        dob = dr["dob"].ToString();
                        tp = dr["tp"].ToString();
                        address = dr["addr"].ToString();
                        user_type = dr["user_type"].ToString();
                        gender = dr["gender"].ToString();
                        pw = dr["passwd"].ToString();

                       
                    }

                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception)
            {
                return 0;
            }
            finally
            {
                db.close();
            }


  
        }

    }

}
