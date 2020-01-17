using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace vegetable.Respository
{
    public static class DapperRepository
    {
        static string connectionStr = ConfigurationManager.ConnectionStrings ["DefaultConnection"].ConnectionString;
        public static void ExcuteDapper (string sql, object datas)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
                //交易
                //using (var tran = conn.BeginTransaction())
                //{
                //    conn.Execute(sql, datas);
                //    tran.Commit();
                //}
                conn.Execute(sql, datas);
        }




    }

}