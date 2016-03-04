using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMG_DataModel
{
    public class dbConnection
    {
        private SqlConnection conn;
        public dbConnection()
        {
            this.conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ToString());
        }

        public SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State ==
                        ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        public SqlConnection closeConnection()
        {
            conn.Close();
            conn.Dispose();
            return conn;
        }
    }
}
