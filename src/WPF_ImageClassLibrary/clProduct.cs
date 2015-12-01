using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WPF_ImageClassLibrary
{

   public class clProduct
    {
        SqlConnection con = new SqlConnection();
        clDatabase conn = new clDatabase();

        public string SaveProduct(int ProductId, int CategoryID, int SubCategoryID, string ProductName, string ImagePath, int IsActive, int IsDeleted)
        {
            string Result;
            con = conn.getConnection();
            SqlCommand cmd = new SqlCommand("SaveProduct_SP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@SubCategoryID", SubCategoryID);
            cmd.Parameters.AddWithValue("@ProductName", ProductName);
            cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
            con.Open();
            Result = cmd.ExecuteScalar().ToString();
            con.Close();
            if ((Result == "Product Save Sucessfully!!!") || (Result == "Product Update Sucessfully!!!"))
            {
                return Result;
            }
            else if (Result == "Product Name Already Exists")
            {
                return Result;
            }
            else
            {
                return "Error To Save";
            }
        }
    }
}
