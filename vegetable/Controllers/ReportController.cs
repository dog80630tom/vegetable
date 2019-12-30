
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vegetable.Models.ViewModels;

using vegetable.Respository;

namespace vegetable.Controllers
{
    public class ReportController : Controller
    {
        ItemContext context = new ItemContext();
        public IEnumerable<ReportViewModel> initreportdata() {
            string sql = @"select * from OrderDetails od
left join Products p on od.ProductID = p.ProductID
 ";
            ConnRespository<ReportViewModel> Respository = new ConnRespository<ReportViewModel>(context);
        var data   = Respository.GetAll(sql);

            return data;
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetReportData() {
            var Lsitdata = initreportdata().ToList();
            var jsondata = JsonConvert.SerializeObject(Lsitdata);
            return Json(jsondata,JsonRequestBehavior.AllowGet);
        }




    }
}