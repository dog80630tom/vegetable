
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
            string sql = @"select year(o.OrderDate) as year, MONTH(o.OrderDate) as mouth from OrderDetails od
left join Products p on od.ProductID = p.ProductID
left join Orders o on od.OrderID=o.OrderID
where o.DeliverName is not null and o.DeliverPhone is not null and o.DeliverAddress is not null
GROUP BY year(o.OrderDate), MONTH(o.OrderDate)
 ";
            ConnRespository<ReportViewModel> Respository = new ConnRespository<ReportViewModel>(context);
        var data   = Respository.GetAll(sql);
          



            return data;
        }
        public IEnumerable<ReportViewModel> initreportdata2()
        {
            string sql = @"select  c.CategoryName,Month(od.OrderDate)as mouth,year(od.OrderDate) as year,SUM(o.Quantity*p.ProductPrice) as total from OrderDetails o
left join Products p on o.ProductID=p.ProductID
left join Orders od on o.OrderID=od.OrderID
left join Categories c on p.CategoryID=c.CategoryID
where od.DeliverName is not null and od.DeliverPhone is not null and od.DeliverAddress is not null
 GRoup by c.CategoryName,Month(od.OrderDate),year(od.OrderDate)
 ";
            ConnRespository<ReportViewModel> Respository = new ConnRespository<ReportViewModel>(context);
            var data = Respository.GetAll(sql);




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
        [HttpPost]
        public ActionResult GetReportMouthData(string mouth,string year)
        {
            int termouth = int.Parse(mouth);
            int teryear = int.Parse(year);
            var Lsitdata = initreportdata2();
          
            Lsitdata = Lsitdata.Where(x => x.mouth == termouth && x.year == teryear);
            var jsondata = JsonConvert.SerializeObject(Lsitdata);
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }



    }
}