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
    public class CartRepository
    {
        public IEnumerable<CartViewModel> GetAllCart (int memberId)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                string sql = $@"select	 
                    p.ProductName, p.ProductPrice, cd.Quantity, cd.MemberID
                    from CartDetails as cd
                    inner join Products as p on cd.ProductID=p.ProductID
                    where cd.MemberID ={memberId}
                    order by cd.CartID";


                return conn.Query<CartViewModel>(sql).ToList();
            }
        }

        string connectionStr = "data source = vegetable.database.windows.net; initial catalog = vegetableDB; user id = sean200365; password=800824arcARC;MultipleActiveResultSets=True;App=EntityFramework";
        public string GetConnectionString ()
        {
            return ConfigurationManager.ConnectionStrings ["DefaultConnection"].ConnectionString;
            //回傳值
            //data source = vegetable.database.windows.net; initial catalog = vegetableDB; user id = sean200365; password=800824arcARC;MultipleActiveResultSets=True;App=EntityFramework
        }
    }
}