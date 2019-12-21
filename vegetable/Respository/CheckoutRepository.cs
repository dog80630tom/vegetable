using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using vegetable.Models;

namespace vegetable.Respository
{
    public class CheckoutRepository
    {
        string connectionStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings ["DefaultConnection"].ConnectionString;
        public void Update(Order order)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                string sql = $@"UPDATE Orders 
                    SET OrderDate=@orderDate, DeliverName = @deliverName,
                    DeliverAddress = @delivaerAddress, DeliverPhone = @deliverPhone
                    WHERE OrderID= {order.OrderID}";
                //修改多筆參數
                var datas = new {
                    deliverName = order.DeliverName,
                    delivaerAddress = order.DeliverAddress,
                    deliverPhone = order.DeliverPhone,
                    orderDate = order.OrderDate

                };
                conn.Execute(sql, datas);
            }
        }
    }
}