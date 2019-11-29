using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
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
            Member.MemberPassword = encryption(Member.MemberPassword, Member.MemberName);
            services.CreateMember(Member);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult FrontCreate(Member Member)
        {
            MemberServices services = new MemberServices();
            Member.MemberPassword = encryption(Member.MemberPassword, Member.MemberName);
            services.CreateMember(Member);
            return Redirect("/FrontEnd/MemberLogInModel");
        }

        public string encryption(string password, string name)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            encrypt = md5.ComputeHash(encode.GetBytes(password + name));
            return Convert.ToBase64String(encrypt);

        }

        [HttpPost]
        public string Login(string uname, string psw)
        {
            //var initdata = initMemberData();
            var temp = item.Members.Any(x => x.MemberEmail == uname);
            if (temp)
            {
                var membership = (from m in item.Members where m.MemberEmail == uname select m).ToList();
                var password = encryption(psw, membership[0].MemberName);
                if (membership[0].MemberEmail == uname && password == membership[0].MemberPassword)
                {
                    Session["userName"] = membership[0].MemberName;
                    Session["isLogIn"] = "Y";
                    return "1";
                }
                return "3";
            
            }
            return "2";
        }









    }
}

