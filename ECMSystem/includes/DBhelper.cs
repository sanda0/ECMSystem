using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ECMSystem.includes
{
    class DBhelper
    {
        //by sandakelum priyamantha
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataReader dr;
        public SqlDataAdapter da;

        //Data Source = Lahiru - Data Source=LAPTOP-0M2J75HO;Initial Catalog=ecmsdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

        public DBhelper()
        {
            string constr = "Server=tcp:sandadev.database.windows.net,1433;Initial Catalog=ecmsdb;Persist Security Info=False;User ID=sandadev;Password=sanda1234#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //string constr = "Data Source=LAPTOP-0M2J75HO;Initial Catalog=ecmsdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(constr);
        }

        public void open()
        {
            con.Open();
        }
        public void close()
        {
            con.Close();
        }

        public int execute_qry(string q)
        {
            int r;
            open();
            cmd = new SqlCommand(q, con);

            try
            {
                r = cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException  e)
            {
                MessageBox.Show(e.ToString());
                r = 2;
            }

            close();
            return r;
        }

        public DataTable filter(string q)
        {
           
                open();
                da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                close();
                
                return dt;

            

        }

        public SqlDataReader find(string table,string id)
        {
            
            string q = "select * from "+table+" where id = '"+id+"'";
            cmd = new SqlCommand(q, con);
            dr = cmd.ExecuteReader();
            

            return dr;
        }

        public string[] getOneColumn(string table,string col)
        {
            string[] data = { };

            string q = "select " + col + " from " + table;
          
            cmd = new SqlCommand(q, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    data.Append(dr[col].ToString());
                }
            }


            return data;
        }

        







    }
}
