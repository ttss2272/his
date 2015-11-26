using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WPF_ImageClassLibrary
{
    public class clDatabase
    {
        public SqlConnection getConnection()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
            return conn;
        }   
    }
}
