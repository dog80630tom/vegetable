
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
left join Orders o on od.OrderID=o.OrderID
where o.DeliverName is not null and o.DeliverPhone is not null and o.DeliverAddress is not null
 ";
            ConnRespository<ReportViewModel> Respository = new ConnRespository<ReportViewModel>(context);
        var data   = Respository.GetAll(sql);
            foreach (var date in data)
            {

                date.Month = date.OrderDate.Month;

            
            }




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