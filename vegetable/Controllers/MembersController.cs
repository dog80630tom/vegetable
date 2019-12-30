
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using vegetable.Models;
using vegetable.Respository.MemberResp;
using vegetable.Services;

namespace vegetable.Controllers
{

    public class MembersController : Controller

    {
        Encryption Encryption = new Encryption();
        ItemContext item = new ItemContext();
        initMember init = new initMember();

        // GET: Member
        public ActionResult Index()
        {
            var initdata = init.initMemberData();
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
            return View(init.initMemberData().Find(x => x.MemberID == Id));
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

            return View(init.initMemberData().Find(x => x.MemberID == Id));

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
            var code = Request.QueryString["code"];
            
            //Response.Write("<br/> code: " + code);

            //get token from code
           

            MemberServices services = new MemberServices();
            Member.MemberPassword = Encryption.EncryptionMethod(Member.MemberPassword, Member.MemberName);
            services.CreateMember(Member);
            return RedirectToAction("Index");
        }


    }
}

