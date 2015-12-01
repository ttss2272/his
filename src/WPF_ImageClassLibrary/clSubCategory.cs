using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WPF_ImageClassLibrary
{
  public  class clSubCategory
    {
        SqlConnection con = new SqlConnection();
        clDatabase conn = new clDatabase();
        public string SaveSubCategory(int SubCatId, int CategoryID,string SubCategoryName, int IsActive, int IsDeleted)
        {
            string Result;
            con = conn.getConnection();
            SqlCommand cmd = new SqlCommand("SaveSubCategory_SP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubCatID", SubCatId);
            cmd.Parameters.AddWithValue("@CatID", CategoryID);
            cmd.Parameters.AddWithValue("@SubCategoryName", SubCategoryName);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
            con.Open();
            Result = cmd.ExecuteScalar().ToString();
            con.Close();
            if ((Result == "Sub Category Save Sucessfully!!!") || (Result == "Sub Category Update Sucessfully!!!"))
            {
                return Result;
            }
            else if (Result == "Sub Category Name Already Exists")
            {
                return Result;
            }
            else
            {
                return "Error To Save";
            }
        }

        public DataSet BindCategoryInSub(int SubCatId, int CategoryID, string SubCatName)
        {
            con = conn.getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("BindCategoryForSub_SP", con);
            cmd.Parameters.AddWithValue("@SubCategoryID", SubCatId);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@SubCategoryName", SubCatName);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds);
            con.Close();
            return ds;
        }

        public string DeleteSubCategory(int SubCatId, string UpdatedDate)
        {
            string Result;
            con = conn.getConnection();
            SqlCommand cmd = new SqlCommand("DeleteSubCategory_SP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubCategoryID", SubCatId);
            cmd.Parameters.AddWithValue("@UpdatedDate", UpdatedDate);
            con.Open();
            Result = cmd.ExecuteScalar().ToString();
            con.Close();
            if (Result == "Sub Category Deleted Sucessfully!!!")
            {
                return Result;
            }
            else
            {
                return "Error To Delete";
            }
        }
    }
}
