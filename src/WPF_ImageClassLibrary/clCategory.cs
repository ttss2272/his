using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WPF_ImageClassLibrary
{
    public class clCategory
    {
        SqlConnection con = new SqlConnection();
        clDatabase conn = new clDatabase();

        public string saveCategory(int CatID, string CategoryName, int IsActive, int IsDeleted)
        {
            int Result;
            con = conn.getConnection();
            SqlCommand cmd = new SqlCommand("saveCategory_SP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CatID", CatID);
            cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
            con.Open();
            Result = cmd.ExecuteNonQuery();
            con.Close();
            if (Result == 1)
            {
                return "Category Save Successfully...!";
            }
            else
            {
                return "Error To Save";
            }
            
        }

        public DataSet GetCategory(int CatID)
        {
            con = conn.getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("GetCategory_SP", con);
            cmd.Parameters.AddWithValue("@CatID", CatID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds);
            con.Close();
            return ds;
        }
    }
}
