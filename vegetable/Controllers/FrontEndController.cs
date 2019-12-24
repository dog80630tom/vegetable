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
using vegetable.Respository.MemberResp;
using vegetable.Services;
using Member = vegetable.Models.Member;
using vegetable.Models.ViewModels;
using vegetable.Respository;

namespace vegetable.Controllers
{
    [RoutePrefix("frontend")]
    public class FrontEndController : Controller
    {
        ItemContext item = new ItemContext();
        initMember init = new initMember();
        Encryption Encryption = new Encryption();
        // GET: FrontEnd

        public ActionResult Index ()
        {
            HttpContext.Response.Cookies.Clear();
            return View();
        }

        //產品顯示功能
        [Route("product")]
        [HttpGet]
        public ActionResult ShowProducts (string query)
        {
            List<int> wishproducts = new List<int>();
            //先抓取是否登入
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            if (rqstCookie != null)
            {
                var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
                var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
                wishproducts = (from p in item.Products
                                join w in item.WishLists
                                on p.ProductID equals w.ProductID
                                where memberData.MemberID == w.MemberID
                                select p.ProductID).ToList();
            }
            
            //若沒有搜尋字串則顯示全部
            //尚未做找不到的功能
            if (query is null)
            {
                query = "";
            }
            else
            {
                query = query.ToLower();
            }

            var allproducts = from p in item.Products
                              join c in item.Categories
                              on p.CategoryID equals c.CategoryID
                              where p.ProductName.ToLower().Contains(query) || c.CategoryName.ToLower().Contains(query)
                              select p;

            var JSONTO = allproducts.ToList();
            foreach (Product p in JSONTO)
            {
                //用viewbag丟json格式到view
                //判斷會員登入
                //if (rqstCookie != null)
                //{
                //    foreach (int id in wishproducts)
                //    {
                //        if (p.ProductID == id)
                //        {
                //            ViewBag.products += "{ProductID:" + p.ProductID + ",CategoryID:" + p.CategoryID + ",ProductDescription:'" + p.ProductDescription + "',ProductName:'" + p.ProductName + "',UnitsInStock:" + p.UnitsInStock + ",ProductPrice:" + p.ProductPrice + ",IsRed:'color:red'},";
                //        }
                //        else
                //        {
                //            ViewBag.products += "{ProductID:" + p.ProductID + ",CategoryID:" + p.CategoryID + ",ProductDescription:'" + p.ProductDescription + "',ProductName:'" + p.ProductName + "',UnitsInStock:" + p.UnitsInStock + ",ProductPrice:" + p.ProductPrice + ",IsRed:''},";
                //        }
                //    }
                //} else
                //{
                //    ViewBag.products += "{ProductID:" + p.ProductID + ",CategoryID:" + p.CategoryID + ",ProductDescription:'" + p.ProductDescription + "',ProductName:'" + p.ProductName + "',UnitsInStock:" + p.UnitsInStock + ",ProductPrice:" + p.ProductPrice + ",IsRed:''},";
                //}
                //沒有description
                if (rqstCookie != null)
                {
                    foreach (int id in wishproducts)
                    {
                        if (p.ProductID == id)
                        {
                            ViewBag.products += "{ProductID:" + p.ProductID + ",CategoryID:" + p.CategoryID + ",ProductName:'" + p.ProductName + "',UnitsInStock:" + p.UnitsInStock + ",ProductPrice:" + p.ProductPrice + ",IsRed:'color:red'},";
                        }
                        else
                        {
                            ViewBag.products += "{ProductID:" + p.ProductID + ",CategoryID:" + p.CategoryID + ",ProductName:'" + p.ProductName + "',UnitsInStock:" + p.UnitsInStock + ",ProductPrice:" + p.ProductPrice + ",IsRed:''},";
                        }
                    }
                }
                else
                {
                    ViewBag.products += "{ProductID:" + p.ProductID + ",CategoryID:" + p.CategoryID + ",ProductName:'" + p.ProductName + "',UnitsInStock:" + p.UnitsInStock + ",ProductPrice:" + p.ProductPrice + ",IsRed:''},";
                }
            }
            ViewBag.products = ViewBag.products.TrimEnd(',');
            return View();
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
                Category category = item.Categories.Find(product.CategoryID);
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
                ViewBag.CategoryName = category.CategoryName;
                return View();
            }
        }
        public ActionResult MemberPageAddress ()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");


            if (rqstCookie.Value.Length > 0)
            {
                return View();
            }
            return RedirectToAction("LoginPage");
        }
        public ActionResult MemberPageAddresschange ()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
            var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
            TempData ["username"] = memberData.MemberName;
            if (rqstCookie.Value.Length < 0)
            {
                return View("LoginPage");
            }
            return RedirectToAction("Index");
        }


        public ActionResult MemberPageWishlist ()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
            var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
            var wishproducts = from p in item.Products
                               join w in item.WishLists
                               on p.ProductID equals w.ProductID
                               where memberData.MemberID == w.MemberID
                               select p;
            return View(wishproducts.ToList());
        }
        [HttpPost]
        public bool AddWish ([Bind(Include = "MemberID,ProductID")] WishList wish)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                using (ItemContext item = new ItemContext())
                {
                    HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
                    var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
                    var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
                    wish.MemberID = memberData.MemberID;
                    item.WishLists.Add(wish);
                    try
                    {
                        item.SaveChanges();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return isSuccess;
        }

        [HttpPost]
        public ActionResult DeleteWish ([Bind(Include = "MemberID,ProductID")] WishList wish)
        {
            if (ModelState.IsValid)
            {
                using (ItemContext item = new ItemContext())
                {
                    HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
                    var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
                    var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
                    wish.MemberID = memberData.MemberID;
                    var temp = item.WishLists.SingleOrDefault(x => x.ProductID == wish.ProductID && x.MemberID == wish.MemberID);

                    item.WishLists.Remove(temp);
                    try
                    {
                        item.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            //return Response.Redirect(Request.FilePath);
            return RedirectToAction("MemberPageWishlist");
        }

        public ActionResult LoginPage ()
        {

            return View();
        }

        public ActionResult ForgotPassword ()
        {
            return View();
        }
        public ActionResult MemberCart ()
        {
            return View();
        }
        public ActionResult Logout ()
        {

            FormsAuthentication.SignOut();
            Session.RemoveAll();
            HttpCookie cookie1 = new HttpCookie("myaccount", "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            TempData ["username"] = null;
            TempData ["roles"] = null;
            return Redirect("Index");
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddCart ([Bind(Include = "CartID,MemberID,ProductID,Quantity")] CartDetail cart)
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
        public ActionResult FrontCreate (Member Member)
        {
            MemberServices services = new MemberServices();
            Member.MemberPassword = Encryption.EncryptionMethod(Member.MemberPassword, Member.MemberName);
            services.CreateMember(Member);
            return Redirect("/FrontEnd/Index");
        }
        public ActionResult LineLogin ()
        {
            var code = Request.QueryString ["code"];
            if (string.IsNullOrEmpty(code))
                return Content("沒有收到 Code");

            var token = isRock.LineLoginV21.Utility.GetTokenFromCode(code,
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
        public ActionResult GoogleLogin ()
        {
            var code = Request.QueryString ["code"];
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
        public string Login (string uname, string psw)
        {
            //get code from queryString

            //var initdata = initMemberData();
            var temp = item.Members.Any(x => x.MemberEmail == uname);
            if (temp)
            {
                var membership = (from m in item.Members where m.MemberEmail == uname select m).ToList();
                var password = Encryption.EncryptionMethod(psw, membership [0].MemberName);
                if (membership [0].MemberEmail == uname && password == membership [0].MemberPassword)
                {
                    LoginProcess("Client", membership [0].MemberName, true, membership [0]);

                    return "1";
                }
                return "3";

            }
            return "2";
        }
        //會員登入功能
        private void LoginProcess (string level, string Name, bool isRemeber, object user)
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
        private void LoginProcessmdfity (string level, string Name, bool isRemeber, object user)
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
            TempData ["roles"] = roles;
        }
        //會員資料修改功能
        public ActionResult MemberPageSetting ()
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
        public ActionResult MemberPageSetting (Member Member)
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

        [HttpPost]
        public void CheckOrder (OrderDetail cart)
        {
            if (ModelState.IsValid)
            {
                using (ItemContext item = new ItemContext())
                {
                    //取得cookie中的會員資料
                    HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
                    var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
                    var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
                    cart.MemberID = memberData.MemberID;
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
                        bool allOrdersAreCheckout = orders.All(x => x.DeliverName != null);
                        //都已經結帳 產生一筆新訂單
                        if (allOrdersAreCheckout)
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
        public Order NewOrder (ItemContext item, OrderDetail cart)
        {
            Order newOrder = new Order();
            newOrder.MemberID = cart.MemberID;
            //datetime格式最小的日期
            newOrder.OrderDate = new DateTime(1753, 01, 01, 00, 00, 00);
            item.Orders.Add(newOrder);
            item.SaveChanges();
            return newOrder;
        }

        public void AddCart (Order order, OrderDetail cart, ItemContext item)
        {
            cart.OrderID = order.OrderID;
            item.OrderDetails.Add(cart);
            item.SaveChanges();
        }

        OrderDetailRepository orderDetail = new OrderDetailRepository();
        public ActionResult Cart ()
        {
            //取得cookie中的會員資料
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
            var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
            IEnumerable<OrderDetailViewModel> cartVM = orderDetail.GetAllCart(memberData.MemberID);

            return View("Cart", cartVM);
        }

        public void DeleteCart (int cartId)
        {
            orderDetail.DeleteCart(cartId);
        }

        [HttpPost]
        public ActionResult GoBackToCart ()
        {
            return Json(Url.Action("Cart"));
        }

        //到結帳頁
        public ActionResult Checkout ()
        {
            // 取得cookie中的會員資料
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
            var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
            IEnumerable<OrderDetailViewModel> cartVM = orderDetail.GetAllCart(memberData.MemberID);
            return View("Checkout", cartVM);
        }

        public CheckoutRepository CheckoutRepository;
        public void AddCheckout (OrderDetailViewModel orderVM)
        {
            if (CheckoutRepository == null)
            {
                CheckoutRepository = new CheckoutRepository();
            }

            Order order = new Order()
            {
                OrderID = orderVM.OrderID,
                OrderDate = orderVM.OrderDate,
                DeliverAddress = orderVM.DeliverAddress,
                DeliverPhone = orderVM.DeliverPhone,
                DeliverName = orderVM.DeliverName
            };
            CheckoutRepository.Update(order);
        }

        public void UpdateCart (int cartId, int quantity)
        {
            orderDetail.UpdateCart(cartId, quantity);
        }
    }
}