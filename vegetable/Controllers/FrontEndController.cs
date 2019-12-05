using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vegetable.Models;

namespace vegetable.Controllers
{
    public class FrontEndController : Controller
    {
        ItemContext Item = new ItemContext();
        // GET: FrontEnd

        public ActionResult Index()
        {
            return View();
        }
        [Route("product")]
        [HttpGet]
        public ActionResult ShowProducts(SearchCondition SearchCondition)
        {
            if (SearchCondition.page is null)
            {
                SearchCondition.page = 1;
            }
            if (SearchCondition.Condition is null)
            {
                SearchCondition.Condition = "";
            }
            else
            {
                SearchCondition.Condition = SearchCondition.Condition.ToLower();
            }

            var products = from p in Item.Products
                           join c in Item.Categories
                           on p.CategoryID equals c.CategoryID
                           where p.ProductName.ToLower().Contains(SearchCondition.Condition) || c.CategoryName.ToLower().Contains(SearchCondition.Condition)
                           select p;

            var pageshowitems = 12.0;
            ViewBag.page = SearchCondition.page;
            ViewBag.pageshowitems = pageshowitems;
            ViewBag.pages = Math.Ceiling(products.Count() / pageshowitems);



            return View(products.ToList());
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
            return View();
        }

        public ActionResult ProductIndex ()
        {
            return View();
        }
        public ActionResult MemberPageAddress()
        {
            return View();
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
        public ActionResult Cart ()
        {
            return View();
        }
        public ActionResult MemberCart()
        {
            return View();
        }
    }
}