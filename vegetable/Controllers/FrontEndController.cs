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
        public ActionResult AddCart (CartDetail cart)
        {
            if (ModelState.IsValid)
            {
                using (ItemContext item = new ItemContext())
                {
                    item.CartDetails.Add(cart);
                    item.SaveChanges();
                    //無法到檢視??
                    return RedirectToAction("Cart");
                }
            }
            return View();
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
            return View(cartVM);
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
    }
}