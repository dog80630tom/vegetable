using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using vegetable.Models;


namespace vegetable.Controllers
{
    [RoutePrefix("frontend")]
    public class FrontEndController : Controller
    {
        ItemContext Item = new ItemContext();
        // GET: FrontEnd

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowProducts(string query)
        {


            if (query is null)
            {
                query = "";

            }
            else
            {
                query = query.ToLower();
            }
            var products = from p in Item.Products
                           join c in Item.Categories
                           on p.CategoryID equals c.CategoryID
                           where p.ProductName.ToLower().Contains(query) || c.CategoryName.ToLower().Contains(query)
                           select p;
            var ffff = products.Count();
            return View(products.ToList());
            //List<Product> result = new List<Product>();
            //using(ItemContext item = new ItemContext())
            //{
            //    result = (from s in item.Products select s ).ToList();
            //    return View(result);
            //}

        }
        public ActionResult MemberRegist()
        {
            return View();
        }
        public ActionResult MemberLogInModel()
        {
            return View();
        }
        public ActionResult MemberPageOrder()
        {
            return View();
        }
        public ActionResult MemberPageOrderDetail()
        {
            return View();
        }
        public ActionResult MemberPageSetting()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");

            if (rqstCookie.Value.Length > 0)
            {
                return RedirectToAction("MemberPageSetting", "Members");
            }
            return RedirectToAction("LoginPage");
        }

        public ActionResult ProductIndex(int? id)
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
        public ActionResult MemberPageAddress()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");

            if (rqstCookie.Value.Length>0)
            {
                return View();
            }
            return RedirectToAction("LoginPage");
        }
        public ActionResult MemberPageWishlist()
        {
            return View();
        }
        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult MemberCart()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddCart([Bind(Include = "CartID,MemberID,ProductID,Quantity")] CartDetail cart)
        {
            if (ModelState.IsValid)
            {
                using (ItemContext item = new ItemContext())
                {
                    item.CartDetails.Add(cart);
                    item.SaveChanges();
                    return RedirectToAction("Cart");
                }
            }
            return View();
        }
    }
}