using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace vegetable.Services
{
    public class CheckoutService
    {
        string connectionStr = ConfigurationManager.ConnectionStrings ["DefaultConnection"].ConnectionString;
        public int GetOrderPrice (int memberId)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                string sql = $@"select sum(od.Quantity*p.ProductPrice) from OrderDetails as od
                             join Orders as o  on o.OrderID = od.OrderID
                             join Products as p on p.ProductID = od.ProductID
                             where od.MemberID = {memberId} and DeliverName is null";
            
                return conn.Query<int>(sql).ToList().FirstOrDefault();
            }
        }
    }
}