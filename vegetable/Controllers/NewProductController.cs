using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
        public List<NewProductDetail> initdetail()
        {
            List<NewProductDetail> data = new List<NewProductDetail>();
            try
            {
                string sql = @"select *from Products p
                                left join Categories c on p.CategoryID= c.CategoryID
                                left join PicDetails pic on pic.ProductID=p.ProductID";
                ConnRespository<NewProductDetail> Conn = new ConnRespository<NewProductDetail>(item);
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
            var initdata = initdetail();
            if (initdata == null)
            {
                return View(new List<NewProductDetail>());
            }
            ViewBag.ISuccess = "false";
            return View(initdata);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(JsonProductDetail product)
        {
            PrductServices services = new PrductServices();

            Product p = ChangeIt(product);
            p.ProductID = product.ProductID;

            JsonURL u = new JsonURL();
            u.Url1 = product.PicUrl1;
            u.Url2 = product.PicUrl2;
            u.Url3 = product.PicUrl3;

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonData = js.Serialize(u);//序列化
            PicDetail pd = new PicDetail();
            pd.PicUrl = jsonData;

            services.addProduct(p, pd);

            //services.addProduct();
            return RedirectToAction("Index");
        }



        public ActionResult Edit(int? id)
        {
            TempData ["ProductID"] = id;
            var product = initdetail().Find(x => x.ProductID == id);
            JsonProductDetail p=new JsonProductDetail ();
            p.ProductID = product.ProductID; 
            p.ProductName = product.ProductName;
            p.ProductPrice = product.ProductPrice;
            p.ProductDescription = product.ProductDescription;
            p.UnitsInStock = product.UnitsInStock;
            p.CategoryId= product.CategoryId;
            JavaScriptSerializer js = new JavaScriptSerializer();
            JsonURL u = js.Deserialize<JsonURL>(product.PicUrl);// //反序列化
            p.PicUrl1 = u.Url1;
            p.PicUrl2 = u.Url2;
            p.PicUrl3 = u.Url3;
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(JsonProductDetail product)
        {
            PrductServices services = new PrductServices();
            Product p = ChangeIt(product);

            p.ProductID = product.ProductID;

            JsonURL u = new JsonURL();
            u.Url1 = product.PicUrl1;
            u.Url2 = product.PicUrl2;
            u.Url3 = product.PicUrl3;

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonData = js.Serialize(u);//序列化
            PicDetail pd = new PicDetail();
            pd.PicUrl = jsonData;
            pd.ProductID = (int)TempData["ProductID"];

            services.EditProduct(p,pd);

            //services.addProduct();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            var delItem = from d in item.Products
                          where d.ProductID == id
                          select d;
 
            var itempic = from d in item.PicDetails
                          where d.ProductID == id
                          select d;

            PrductServices services = new PrductServices();
            services.DeleteProduct(delItem, itempic);
            return RedirectToAction("Index");
        }

        public Product ChangeIt(JsonProductDetail product)
        {
            Product p = new Product();
            p.ProductName = product.ProductName;
            p.ProductPrice = product.ProductPrice;
            p.ProductDescription = product.ProductDescription;
            p.UnitsInStock = product.UnitsInStock;
            p.CategoryID = product.CategoryId;

            p.ProductID = 0;

            return p;

        }

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
        public ActionResult UploadFile2Cloud(string updatedata, string form)
        {
            var client = new ImgurClient("f4698b7dc49d5f0", "109e94774eab1e47496e875b4e55bc6b6e59140f");
            var endpoint = new ImageEndpoint(client);
            IImage image;
            //取得圖片檔案FileStream
            using (var fs = new FileStream(form, FileMode.Open))
            {
                image = endpoint.UploadImageStreamAsync(fs).GetAwaiter().GetResult();
            }
            var link = JsonConvert.SerializeObject(image.Link);
            link = link.Replace("\"", "");

            return Json(link, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getupdate()
        {

            return View();
        }


    }
}






