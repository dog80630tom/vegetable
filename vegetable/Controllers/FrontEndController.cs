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
        // GET: FrontEnd
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowProducts()
        {
            List<Product> result = new List<Product>();
            using(MyDBContext item = new MyDBContext())
            {
                result = (from s in item.Products select s ).ToList();
                return View(result);
            }
            
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
            Product result = new Product();
            using (MyDBContext item = new MyDBContext())
            {
                result = (from s in item.Products select s).FirstOrDefault();
                return View(result);
            }
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