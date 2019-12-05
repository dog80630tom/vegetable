using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using vegetable.Models;
using vegetable.Services;

namespace vegetable.Controllers
{
    public class MembersController : Controller
    {
        ItemContext item = new ItemContext();
        // GET: Member

        public List<Member> initMemberData()
        {
            List<Member> MemberData = new List<Member>();
            try
            {
                MemberData = (from m in item.Members select m).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return (MemberData);
        }

        public ActionResult Index()
        {
            var initdata = initMemberData();
            if (initdata == null)
            {
                return View(new List<Member>());
            }
            ViewBag.ISuccess = "false";
            return View(initdata);
        }


        public ActionResult Edit(int? Id)
        {
            TempData["MemberID"] = Id;
            return View(initMemberData().Find(x => x.MemberID == Id));
        }

        [HttpPost]
        public ActionResult Edit(Member Member)
        {
            MemberServices services = new MemberServices();
            Member.MemberID = (int)TempData["MemberID"];
            services.EditMember(Member);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? Id)
        {

            TempData["MemberID"] = Id;

            return View(initMemberData().Find(x => x.MemberID == Id));

        }

        [HttpPost]
        public ActionResult Delete(Member Member)
        {
            Member.MemberID = (int)TempData["MemberID"];
            MemberServices services = new MemberServices();
            services.DeleteMember(Member);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new List<Member>());
        }

        [HttpPost]
        public ActionResult Create(Member Member)
        {
            MemberServices services = new MemberServices();
            Member.MemberPassword = Encryption(Member.MemberPassword, Member.MemberName);
            services.CreateMember(Member);
            return RedirectToAction("Index");
        }



        //以下為前台的會員功能
        //會員新增功能
        [HttpPost]
        public ActionResult FrontCreate(Member Member)
        {
            MemberServices services = new MemberServices();
            Member.MemberPassword = Encryption(Member.MemberPassword, Member.MemberName);
            services.CreateMember(Member);
            return Redirect("/FrontEnd/MemberLogInModel");
        }

        public string Encryption(string password, string name)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            encrypt = md5.ComputeHash(encode.GetBytes(password + name));
            return Convert.ToBase64String(encrypt);
        }

        //會員登入功能
        [HttpPost]
        public string Login(string uname, string psw)
        {
            //var initdata = initMemberData();
            var temp = item.Members.Any(x => x.MemberEmail == uname);
            if (temp)
            {
                var membership = (from m in item.Members where m.MemberEmail == uname select m).ToList();
                var password = Encryption(psw, membership[0].MemberName);
                if (membership[0].MemberEmail == uname && password == membership[0].MemberPassword)
                {
                    //Session["userName"] = membership[0].MemberName;
                    //Session["isLogIn"] = "Y";
                    LoginProcess("Client", membership[0].MemberName, true, membership[0]);
 

                    return "1";
                }
                return "3";
            
            }
            return "2";
        }

        private void LoginProcess(string level, string Name, bool isRemeber,object user)
        {
            var now = DateTime.Now;
            string roles = level;
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: Name, //這邊看個人，你想放使用者名稱也可以，自行更改
                issueDate: now,//現在時間
                expiration: DateTime.Now.AddDays(1),//Cookie有效時間=現在時間往後+30分鐘
                isPersistent: isRemeber,//記住我 true or false
                userData: JsonConvert.SerializeObject(user), //放會員資料
                cookiePath: "/");

            var encryptedTicket = FormsAuthentication.Encrypt(ticket); //把驗證的表單加密
            var cookie = new HttpCookie("myaccount", encryptedTicket);
            HttpContext.Response.Cookies.Add(cookie);

        }

        public ActionResult MemberPageSetting()
        {
            HttpCookie rqstCookie = HttpContext.Request.Cookies.Get("myaccount");
            var memberDataObj = FormsAuthentication.Decrypt(rqstCookie.Value);
            var memberData = JsonConvert.DeserializeObject<Member>(memberDataObj.UserData);
            return View(initMemberData().Find(x => x.MemberID == memberData.MemberID));
        }

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

