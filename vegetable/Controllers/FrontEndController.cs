using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using vegetable.Models;
using vegetable.Models.ViewModels;
using vegetable.Respository;

namespace vegetable.Controllers
{
    public class FrontEndController : Controller
    {
        ItemContext Item = new ItemContext();
        // GET: FrontEnd

        public ActionResult Index ()
        {
            return View();
        }
        [Route("product")]
        [HttpGet]
        public ActionResult ShowProducts (string SearchCondition)
        {


            if (SearchCondition is null)
            {
                SearchCondition = "";

            }

            var products = from p in Item.Products
                           join c in Item.Categories
                           on p.CategoryID equals c.CategoryID
                           where p.ProductName.Contains(SearchCondition) || c.CategoryName.Contains(SearchCondition)
                           select p;

            return View(products.ToList());
            //List<Product> result = new List<Product>();
            //using(ItemContext item = new ItemContext())
            //{
            //    result = (from s in item.Products select s ).ToList();
            //    return View(result);
            //}

        }
        public ActionResult MemberRegist ()
        {
            return View();
        }
        public ActionResult MemberLogInModel ()
        {
            return View();
        }
        public ActionResult MemberPageOrder ()
        {
            return View();
        }
        public ActionResult MemberPageOrderDetail ()
        {
            return View();
        }
        public ActionResult MemberPageSetting ()
        {
            return View();
        }

        public ActionResult ProductIndex (int? id)
        {
            //沒有傳入id
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ItemContext item = new ItemContext())
            {
                Product product = item.Products.Find(id);
                //傳入的id找不到商品
                if (product == null)
                {
                    return HttpNotFound();
                }
                //先預設Id = 1 之後要改
                ViewBag.MemberID = 1;
                //預設為1
                ViewBag.CartID = 1;
                ViewBag.ProductID = id;
                ViewBag.ProductDescription = product.ProductDescription;
                ViewBag.ProductName = product.ProductName;
                ViewBag.ProductPrice = product.ProductPrice;
                return View();
            }
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        //改成ajax之前的寫法
        //public ActionResult AddCart ([Bind(Include = "CartID,MemberID,ProductID,Quantity")] CartDetail cart)
        public void CheckOrder (OrderDetail cart)
        {
            if (ModelState.IsValid)
            {
                using (ItemContext item = new ItemContext())
                {
                    // 確認這個memberID 在資料庫目前有多少orders
                    // orders 可能是 null || 一筆 || 多筆
                    var orders = from o in item.Orders
                                 where o.MemberID == cart.MemberID
                                 select o;

                    // if true 此會員目前沒下過任何訂單
                    if (orders.ToList().Count() == 0)
                    {
                        //如果orders沒資料, 創建一筆order, 並寫入資料庫
                        Order newOrder = NewOrder(item, cart);
                        AddCart(newOrder, cart, item);
                    }
                    // if true 此會員已有訂單 但不確定是否結帳
                    else if (orders.ToList().Count() != 0)
                    {
                        // 如果DeliverName == null 表示未結帳
                        // 此會員若有未結帳的訂單 理論上 只會有1筆
                        // 判斷此會員的所有訂單是否都已結帳
                        bool hasNotCheckoutOrders = orders.All(x => x.DeliverName != null);
                        //都已經結帳 產生一筆新訂單
                        if (hasNotCheckoutOrders)
                        {
                            Order newOrder = NewOrder(item, cart);
                            AddCart(newOrder, cart, item);
                        }
                        //有尚未結帳的訂單, 新增的購物車可以繼續寫入
                        else
                        {
                            //理論上未結帳訂單最多一筆
                            var notCheckoutOrder = orders.Where(x => x.DeliverName == null).FirstOrDefault();
                            cart.OrderID = notCheckoutOrder.OrderID;
                            AddCart(notCheckoutOrder, cart, item);
                        }
                    }
                }
            }
        }

        //創建一筆新order
        public Order NewOrder(ItemContext item, OrderDetail cart) {
            Order newOrder = new Order();
            newOrder.MemberID = cart.MemberID;
            item.Orders.Add(newOrder);
            item.SaveChanges();
            return newOrder;
        }

        public void AddCart (Order order , OrderDetail cart, ItemContext item)
        {
            cart.OrderID = order.OrderID;
            item.OrderDetails.Add(cart);
            item.SaveChanges();
        }



        public ActionResult MemberPageAddress ()
        {
            return View();
        }
        public ActionResult MemberPageWishlist ()
        {
            return View();
        }
        public ActionResult LoginPage ()
        {
            return View();
        }
        public ActionResult ForgotPassword ()
        {
            return View();
        }

        CartRepository CartRepository = new CartRepository();
        public ActionResult Cart ()
        {
            //預設為會員1
            int memberId = 1;
            IEnumerable<CartViewModel> cartVM = CartRepository.GetAllCart(memberId);

            return View("Cart", cartVM);
        }

        public void DeleteCart (int cartId)
        {
            CartRepository.DeleteCart(cartId);
        }

        [HttpPost]
        public ActionResult GoBackToCart ()
        {
            return Json(Url.Action("Cart"));
        }


        public ActionResult MemberCart ()
        {
            return View();
        }

        //貨運FAQ
        public ActionResult Shipping ()
        {
            return View();
        }

        //結帳頁
        public ActionResult Checkout ()
        {
            //預設為會員1
            int memberId = 1;
            IEnumerable<CartViewModel> cartVM = CartRepository.GetAllCart(memberId);

            return View("Checkout", cartVM);
        }
    }
}