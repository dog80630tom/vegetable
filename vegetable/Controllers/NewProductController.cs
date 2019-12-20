using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vegetable.Models;
using vegetable.Models.ViewModels;
using vegetable.Respository;
using vegetable.Services;

namespace vegetable.Controllers
{
    public class NewProductController : Controller
    {
        // GET: NewProduct


        ItemContext item = new ItemContext();

        // 用daper抓出初始資料
        public List<ProducetDetail> initdetil()
        {
            List<ProducetDetail> data = new List<ProducetDetail>();
            try
            {
                string sql = @"select *from Products p
                                left join Categories c on p.CategoryID= c.CategoryID
                                left join PicDetails pic on pic.ProductID=p.ProductID";
                ConnRespository<ProducetDetail> Conn = new ConnRespository<ProducetDetail>(item);
                data = Conn.GetAll(sql).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return data;
        }

        public ActionResult Index()
        {
            var initdata = initdetil();
            if (initdata == null)
            {

                return View(new List<ProducetDetail>());
            }
            ViewBag.ISuccess = "false";
            return View(initdata);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, PicDetail pic)
        {
            PrductServices services = new PrductServices();
            services.addProduct();
            return RedirectToAction("Index");
        }


    }
}