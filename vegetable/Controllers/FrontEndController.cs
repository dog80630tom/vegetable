using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            return View();
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
        public ActionResult MemberPageSetting()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");

            if (rqstCookie.Value.Length > 0)
            {
                var bbb = FormsAuthentication.Decrypt(rqstCookie.Value);
                var memberData=JsonConvert.DeserializeObject<Member>(bbb.UserData);
                return View();
            }
            return RedirectToAction("LoginPage");
        }

        public ActionResult ProductIndex ()
        {
            return View();
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


    }
}