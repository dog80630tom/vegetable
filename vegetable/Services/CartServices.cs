using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using vegetable.Models;

namespace vegetable.Services
{
    public class CartServices
    {
        string connectionStr = ConfigurationManager.ConnectionStrings ["DefaultConnection"].ConnectionString;
        public OrderDetail [] GetCartItems (int memberID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                string strSql = $@"select * from Orders o
                     join OrderDetails  od 
                     on o.OrderID = od.OrderID
                     where o.DeliverName is null 
                     and od.MemberID =  {memberID}";
                return conn.Query<OrderDetail>(strSql).ToArray();
            }
        }
    }
}