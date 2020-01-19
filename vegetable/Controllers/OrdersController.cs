using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vegetable.Models;
using vegetable.Services;

namespace vegetable.Controllers
{
    public class OrdersController : Controller
    {
        ItemContext item = new ItemContext();
        // GET: Order
        public List<Order> initOrderData()
        {
            List<Order> orderData = new List<Order>();
            try
            {
                orderData = (from c in item.Orders select c).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return (orderData);
        }

        public ActionResult Index()
        {
            var initdata = initOrderData();
            if (initdata == null)
            {
                return View(new List<Order>());
            }
            ViewBag.ISuccess = "false";
            return View(initdata);
        }

        public ActionResult Edit(int? Id)
        {
            TempData["OrderID"] = Id;
            return View(initOrderData().Find(x => x.OrderID == Id));
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            OrderServices services = new OrderServices();
            order.OrderID = (int)TempData["OrderID"];
            services.EditOrder(order);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            var delItem = initOrderData().Find(x => x.OrderID == id);
            var delOrderDetail = from d in item.OrderDetails
                          where d.OrderID == id
                          select d;
            OrderServices services = new OrderServices();
            services.DeleteOrder(delItem,delOrderDetail);
            return RedirectToAction("Index");
        }




    }
}