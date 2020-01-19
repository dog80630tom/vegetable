using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using vegetable.Controllers;
using vegetable.Models;
using vegetable.Respository;

namespace vegetable.Services
{
    public class OrderServices
    {
        ItemContext item = new ItemContext();
        public ErrorMessage EditOrder(Order order)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            try
            {
                item.Entry(order).State = EntityState.Modified;
                item.SaveChanges();
            }
            catch (Exception ex)
            {
                error.IsSuccess = false;
                error.Message = ex.Message;
                return error;
            }
            return error;
        }
        public ErrorMessage DeleteOrder(Order order, IQueryable<OrderDetail> orderDetail)
        {
            ErrorMessage error = new ErrorMessage();
            error.IsSuccess = true;
            try
            {

                string connectionStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


                List<OrderDetail> od = orderDetail.ToList();

                    var memberId = od[0].MemberID;
                    var orderId = od[0].OrderID;
                    using (SqlConnection conn = new SqlConnection(connectionStr))
                    {
                        string sqlstr = $"delete OrderDetails where OrderID = @orderId and MemberID= @memberId";

                        var datas = new
                        { orderId = orderId , memberId = memberId };
                        conn.Execute(sqlstr, datas);
                    };
               
                Order temp = item.Orders.Find(order.OrderID);
                item.Orders.Remove(temp);
                item.SaveChanges();
            }
            catch (Exception ex)
            {
                error.IsSuccess = false;
                error.Message = ex.Message;
                return error;
            }
            return error;

        }

           
        



    }
}