using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using vegetable.Models.ViewModels;

namespace vegetable.Respository
{
    public class OrderDetailRepository
    {
        string connectionStr = "data source = vegetable.database.windows.net; initial catalog = vegetableDB; user id = sean200365; password=800824arcARC;MultipleActiveResultSets=True;App=EntityFramework";
        public IEnumerable<OrderDetailViewModel> GetAllCart (int memberId)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                string sql = $@"select
                    p.ProductName, p.ProductPrice, od.Quantity, od.MemberID,
                    o.DeliverName, od.OrderDetailsID, o.OrderID, c.CategoryName
                    from OrderDetails as od
                    join Products as p on od.ProductID= p.ProductID

                    join Orders as o on o.OrderID = od.OrderID

                    join Categories as c on c.CategoryID= p.CategoryID

                    where o.DeliverName IS NULL and od.MemberID = {memberId}

                    order by o.OrderID";
                return conn.Query<OrderDetailViewModel>(sql).ToList();
            }
        }


        public void DeleteCart (int cartId)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                string sql = $"delete OrderDetails where OrderDetailsID = @CartId";

                var datas = new
                {
                    CartId = cartId
                };
                conn.Execute(sql, datas);
            }
        }

    }
}