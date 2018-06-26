using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SEP_Team1.API
{
    public class CreateSessionID
    {
        Utility ut = new Utility();
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlCommandBuilder scd;
        static string strConection = System.Configuration.ConfigurationManager.ConnectionStrings["sep21t21Entities"].ToString();
        SqlConnection conn = new SqlConnection(strConection);
        public int GetLastID(string nameTable, string nameSelectColumn, string maKH)
        {
            conn = ut.OpenDb();
            conn.Open();
            cmd = new SqlCommand("select *from BangBuoiHoc", conn);
            cmd.CommandType = CommandType.Text;
            string sql = "SELECT TOP 1 " + nameSelectColumn + " FROM " + nameTable + " where MaKH = " + "'" + maKH + "'" + " ORDER BY " + nameSelectColumn + " DESC";
            cmd.CommandText = sql;
            try
            {
                return (int)cmd.ExecuteScalar();
            }
            catch (NullReferenceException)
            {
                return 0;
            }

        }
    }
}