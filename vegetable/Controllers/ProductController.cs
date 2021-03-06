using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vegetable.Models;
using vegetable.Models.ViewModels;
using vegetable.Respository;

namespace vegetable.Controllers
{
    public class ProductController : Controller
    {
        ItemContext item = new ItemContext();
        // GET: Backstage
        public List<ProducetDetail> initdetil() {
            List<ProducetDetail> data =new List<ProducetDetail>();
            try
            {
                string sql = @"select *from Products p
left join Categories c on p.CategoryID= c.CategoryID
left join PicDetails pic on pic.ProductID=p.ProductID";
                ConnRespository<ProducetDetail> Conn = new ConnRespository<ProducetDetail>(item);
             data =  Conn.GetAll( sql).ToList();
                //有join有viewmodel才要用隱含轉換
                //data = (from d in item.Products
                //            join c in item.Categories on d.CategoryID equals c.CategoryID
                //            join pic in item.PicDetails on d.ProductID equals pic.ProductID
                            
                //    select new
                //    {
                //        ProductID = d.ProductID,
                //        CategoryDescription = c.CategoryDescription,
                //        CategoryName = c.CategoryName,
                //        CategoryPic = c.CategoryPic,
                //        PicUrl = pic.PicUrl,
                //        ProductDescription = d.ProductDescription,
                //        ProductName = d.ProductName,
                //        UnitsInStock = d.UnitsInStock,
                //        ProductPrice=d.ProductPrice
                //    }).ToList().Select(x=>new ProducetDetail
                //    {
                //        ProductID = x.ProductID,
                //        CategoryDescription = x.CategoryDescription,
                //        CategoryName = x.CategoryName,
                //        CategoryPic = x.CategoryPic,
                //        PicUrl = x.PicUrl,
                //        ProductDescription = x.ProductDescription,
                //        ProductName = x.ProductName,
                //        UnitsInStock = x.UnitsInStock,
                //        ProductPrice=x.ProductPrice
                //    }).ToList();
            }
            catch (Exception ex) {
                return null;

            }
            return data;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductList()
        {
            var initdata = initdetil();
            if (initdata == null)
            {

                return View(new List<ProducetDetail>());
            }
            ViewBag.ISuccess = "false";
            return View(initdata);
        }
        public ActionResult AdminIndex()
        {
            return View();
        }


        //商品的初始資料
        [HttpPost]
        public ActionResult Getinitdata()
        {
            var initdata = initdetil();

            ProductDetilListViewMdoel listViewMdoel = new ProductDetilListViewMdoel();
            listViewMdoel.models = initdata;
            var jsondata = JsonConvert.SerializeObject(listViewMdoel);
            ViewBag.ISuccess = "false";
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult Form(string CategoryID, string ProductName, string CategoryName, string ProductDescription, int UnitsInStock, string PicUrl, string CategoryPic, string CategoryDescription,int ProductPrice)
        //{
        //    PrductServices services = new PrductServices();
        //    Category category = services.GetCategory(CategoryName, CategoryPic, CategoryDescription);
        //    Product product = services.GetProduct(ProductName, CategoryName, ProductDescription, UnitsInStock, ProductPrice);
        //    PicDetail picDetail = services.GetPicDetail(PicUrl);
        //    services.addProduct(product, category, picDetail);

        //    return View(initdetil());
        //}

        //local圖片上傳方法
        [HttpPost]
        public ActionResult UploadFile()
        {
            var path = "";
            if (Request.Files.AllKeys.Any())
            {
                //## 讀取指定的上傳檔案ID
                var httpPostedFile = Request.Files["userfile"];

                //## 真實有檔案，進行上傳
                if (httpPostedFile != null && httpPostedFile.ContentLength != 0)
                {
                    string _FileName = Path.GetFileName(httpPostedFile.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Assets/Image"), _FileName);
                    httpPostedFile.SaveAs(_path);
                    path = _path;
                }
            }
            return Json(path, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ProductList(Product product,Category category,PicDetail picDetail, HttpPostedFileBase file)
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(Product product, PicDetail pic)
        {
            return View();
        }

       /* public ActionResult Edit(int? id)
        {

            TempData["ProductID"] = id;
            //TempData是在專案啟動時把資料存到Session，不過在結束專案時會把此資料存到Session，詳細請參考https://dotblogs.com.tw/wadehuang36/2010/10/02/tempdata
            return View(initdetil().Find(x => x.ProductID == id));
        }
        [HttpPost]
        public ActionResult Edit(Product product, Category category, PicDetail pic)
        {
            PrductServices services = new PrductServices();
            product.ProductID = (int)TempData["ProductID"];
            category.CategoryID = (from d in item.Products where d.ProductID == product.ProductID select d).FirstOrDefault().CategoryID;
            product.CategoryID = (from d in item.Products where d.ProductID == product.ProductID select d).FirstOrDefault().CategoryID;
            pic.ProductID = product.ProductID;
            services.EditProduct(product, category, pic);

            TempData["ProductID"] = null;
            //此程式碼是為了把資料不要被攻擊的預防行為
            return RedirectToAction("Form", initdetil());
        }*/

      /*  public ActionResult Delete(int? id)
        {
                var delItem =from d in item.Products
                             where d.ProductID==id
                             select d;
            var data2= (from d in item.Products where d.ProductID == id select d).FirstOrDefault().CategoryID;
            var co = from d in item.Categories
                     where d.CategoryID == data2
                     select d;
            var itempic = from d in item.PicDetails
                          where d.ProductID == id
                          select d ;
               
                PrductServices services = new PrductServices();
                services.DeleteProduct(delItem, itempic, co);
            return RedirectToAction("Index");
        }*/
       

      
    }
}