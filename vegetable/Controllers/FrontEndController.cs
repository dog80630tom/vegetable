
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Security;
using vegetable.Cors;
using vegetable.Models;
using vegetable.Models.LinePay;
using vegetable.Respository.MemberResp;
using vegetable.Services;


namespace vegetable.Controllers
{
    [RoutePrefix("frontend")]
    
    public class FrontEndController : Controller
    {
        ItemContext item = new ItemContext();
        initMember init = new initMember();
        Encryption Encryption = new Encryption();
        // GET: FrontEnd

        public ActionResult Index()
        {
            HttpContext.Response.Cookies.Clear();
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
            var products = from p in item.Products
                           join c in item.Categories
                           on p.CategoryID equals c.CategoryID
                           where p.ProductName.ToLower().Contains(query) || c.CategoryName.ToLower().Contains(query)
                           select p;
                           
            return View(products.ToList());
        }
        
        [Route("product")]
        [HttpPost]
        public ActionResult ShowProducts(SearchCondition SearchCondition)
        {
            if (SearchCondition.Page is null)
            {
                SearchCondition.Page = 1;
            }

            if (SearchCondition.Condition is null)
            {
                SearchCondition.Condition = "";
            }
            else
            {
                SearchCondition.Condition = SearchCondition.Condition.ToLower();
            }

            var allproducts = from p in item.Products
                           join c in item.Categories
                           on p.CategoryID equals c.CategoryID
                           where p.ProductName.ToLower().Contains(SearchCondition.Condition) || c.CategoryName.ToLower().Contains(SearchCondition.Condition)
                           select p;
            //List<Product> result = new List<Product>();
            //using(ItemContext item = new ItemContext())
            //{
            //    result = (from s in item.Products select s ).ToList();
            //    return View(result);
            //}

            var pageshowitems = 12.0;
            ViewBag.pageshowitems = pageshowitems;
            ViewBag.pages = Math.Ceiling(allproducts.Count() / pageshowitems);

            var products = allproducts;
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
        [AllowCrossSite]
        public ActionResult MemberPageOrderDetail()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult getLine()
        {
            LinePay line = new LinePay();
            var data= line.passdata(10);
            TempData["Pay"] = 10;
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public ActionResult checkPay() {
            var code = Request.QueryString["transactionId"];
            int doll = (int)TempData["Pay"];
            LinePay line = new LinePay();
            line.confirm(doll, code);
            return View();
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
        public ActionResult MemberPageAddresschange()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
            var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
            TempData["username"] = memberData.MemberName;
            if (rqstCookie.Value.Length < 0)
            {
                return View("LoginPage");
            }
            return RedirectToAction("Index");
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
        public ActionResult Logout() {

            FormsAuthentication.SignOut();
            Session.RemoveAll();
            HttpCookie cookie1 = new HttpCookie("myaccount", "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            TempData["username"] = null;
            TempData["roles"] = null;
            return Redirect("Index");
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

        //會員新增功能
        [HttpPost]
        public ActionResult FrontCreate(Member Member)
        {
            MemberServices services = new MemberServices();
            Member.MemberPassword = Encryption.EncryptionMethod(Member.MemberPassword, Member.MemberName);
            services.CreateMember(Member);
            return Redirect("/FrontEnd/Index");
        }
        public ActionResult LineLogin()
        {
            var code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
                return Content("沒有收到 Code");

            var token =isRock.LineLoginV21.Utility.GetTokenFromCode(code,
                 "1653659088",
                 "27d426186987ed6e5d69cb7601129805",
                 "https://localhost:44394/frontend/LineLogin");

            var UserInfoResult = isRock.LineLoginV21.Utility.GetUserProfile(token.access_token);
            // 這邊不建議直接把 Token 當做參數傳給 CallAPI 可以避免 Token 洩漏

            int i = 0;
            var email = UserInfoResult.statusMessage;
            var name = UserInfoResult.displayName;
            var password2 = UserInfoResult.userId;
            if (!item.Members.Any(x => x.MemberEmail == email))
            {
                Member member = new Member();
                MemberServices services = new MemberServices();
                member.MemberPassword = Encryption.EncryptionMethod(password2, email);
                member.MemberName = name;
                member.MemberEmail = email;
                member.MemberGender = "Line";
                member.MemberPhone = "Line";

                services.CreateMember(member);

            }
            var membership = (from m in item.Members where m.MemberEmail == email select m).FirstOrDefault();
            var password = Encryption.EncryptionMethod(password2, membership.MemberName);
            LoginProcessmdfity("Client", membership.MemberName, true, membership);

            return RedirectToAction("MemberPageAddresschange");
        }
        public ActionResult GoogleLogin() {
            var code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
                return Content("沒有收到 Code");

            var token = Utility.GetTokenFromCode(code,
                 "145015126077-5afcqbo9rc629k3ilceajnbfrlrdamlj.apps.googleusercontent.com",
                 "At2kDe1L5weKB4Xf7dpf6rmx",
                 "https://localhost:44394/FrontEnd/GoogleLogin");

            var UserInfoResult = Utility.GetUserInfo(token.access_token);
            // 這邊不建議直接把 Token 當做參數傳給 CallAPI 可以避免 Token 洩漏

            var email = UserInfoResult.email;
            var name = UserInfoResult.name;
            var password2 = UserInfoResult.id;
            if (!item.Members.Any(x => x.MemberEmail == email))
            {
                Member member = new Member();
                MemberServices services = new MemberServices();
                member.MemberPassword = Encryption.EncryptionMethod(password2, email);
                member.MemberName = name;
                member.MemberEmail = email;
                member.MemberGender = "Google";
                member.MemberPhone = "google";
                
                services.CreateMember(member);
             
            }
            var membership = (from m in item.Members where m.MemberEmail == email select m).FirstOrDefault();
            var password = Encryption.EncryptionMethod(password2, membership.MemberName);
            LoginProcessmdfity("Client", membership.MemberName, true, membership);
         
            return RedirectToAction("MemberPageAddresschange");
        }

        //會員登入功能
        [HttpPost]
        public string Login(string uname, string psw)
        {
            //get code from queryString
           
            //var initdata = initMemberData();
            var temp = item.Members.Any(x => x.MemberEmail == uname);
            if (temp)
            {
                var membership = (from m in item.Members where m.MemberEmail == uname select m).ToList();
                var password = Encryption.EncryptionMethod(psw, membership[0].MemberName);
                if (membership[0].MemberEmail == uname && password == membership[0].MemberPassword)
                {
                    LoginProcess("Client", membership[0].MemberName, true, membership[0]);

                    return "1";
                }
                return "3";

            }
            return "2";
        }
        //會員登入功能
        private void LoginProcess(string level, string Name, bool isRemeber, object user)
        {
            var now = DateTime.Now;
            string roles = level;
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: Name, 
                issueDate: now,//現在時間
                expiration: DateTime.Now.AddDays(1),//Cookie有效時間=現在時間往後+30分鐘
                isPersistent: isRemeber,//記住我 true or false
                userData: JsonConvert.SerializeObject(user), //放會員資料
                cookiePath: "/");

            var encryptedTicket = FormsAuthentication.Encrypt(ticket); //把驗證的表單加密
            var cookie = new HttpCookie("myaccount", encryptedTicket);
            HttpContext.Response.Cookies.Add(cookie);

        }
        private void LoginProcessmdfity(string level, string Name, bool isRemeber, object user)
        {
            var now = DateTime.Now;
            string roles = level;
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: Name,
                issueDate: now,//現在時間
                expiration: DateTime.Now.AddDays(1),//Cookie有效時間=現在時間往後+30分鐘
                isPersistent: isRemeber,//記住我 true or false
                userData: JsonConvert.SerializeObject(user), //放會員資料
                cookiePath: "/");

            var encryptedTicket = FormsAuthentication.Encrypt(ticket); //把驗證的表單加密
            var cookie = new HttpCookie("myaccount", encryptedTicket);
            HttpContext.Response.Cookies.Add(cookie);
            TempData["roles"] = roles;
        }
        //會員資料修改功能
        public ActionResult MemberPageSetting()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
            var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);

            if (rqstCookie.Value.Length > 0)
            {
                return View(init.initMemberData().Find(x => x.MemberID == memberData.MemberID));
            }
            return RedirectToAction("LoginPage");
           
        }

        //會員資料修改功能
        [HttpPost]
        public ActionResult MemberPageSetting(Member Member)
        {

            MemberServices services = new MemberServices();
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
            var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
            Member.MemberID = memberData.MemberID;
            Member.MemberGender = memberData.MemberGender;


            services.EditMember(Member);
            return RedirectToAction("Index", "FrontEnd");

        }
    }
}