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
        string connectionStr = "data source = vegetable.database.windows.net; initial catalog = vegetableDB; user id = sean200365; password=800824arcARC;MultipleActiveResultSets=True;App=EntityFramework";
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