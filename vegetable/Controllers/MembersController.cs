using System;
using System.Collections.Generic;
using System.Linq;
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
            services.CreateMember(Member);
            return RedirectToAction("Index");
        }







    }
}

