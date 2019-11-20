using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vegetable.Models;
using vegetable.Models.ViewModels;
using vegetable.Services;

namespace vegetable.Controllers
{
    public class BackstageController : Controller
    {
        ItemContext item = new ItemContext();
        // GET: Backstage
        public List<ProducetDetil> initdetil() {
            List<ProducetDetil> data =new List<ProducetDetil>();
            try
            {
                data = (from d in item.Products
                            join c in item.Categories on d.CategoryID equals c.CategoryID
                            join pic in item.PicDetails on d.ProductID equals pic.ProductID
                            
                    select new
                    {
                        ProductID = d.ProductID,
                        CategoryDescription = c.CategoryDescription,
                        CategoryName = c.CategoryName,
                        CategoryPic = c.CategoryPic,
                        PicUrl = pic.PicUrl,
                        ProductDescription = d.ProductDescription,
                        ProductName = d.ProductName,
                        UnitsInStock = d.UnitsInStock,
                        ProductPrice=d.ProductPrice
                    }).ToList().Select(x=>new ProducetDetil
                    {
                        ProductID = x.ProductID,
                        CategoryDescription = x.CategoryDescription,
                        CategoryName = x.CategoryName,
                        CategoryPic = x.CategoryPic,
                        PicUrl = x.PicUrl,
                        ProductDescription = x.ProductDescription,
                        ProductName = x.ProductName,
                        UnitsInStock = x.UnitsInStock,
                        ProductPrice=x.ProductPrice
                    }).ToList();
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
        public ActionResult Form()
        {
            var initdata = initdetil();
            if (initdata == null)
            {

                return View(new List<ProducetDetil>());
            }

            return View(initdata);
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
        [HttpPost]
        public ActionResult Form(Product product,Category category,PicDetail picDetail)
        {
            PrductServices services = new PrductServices();
         
            services.addProduct(product, category, picDetail);

            return View(initdetil());
        }
        //cinnaㄉ
        public ActionResult Edit(int? id)
        {
           


            return View(initdetil().Find(x=>x.ProductID==id));
        }
        [HttpPost]
        public ActionResult Edit(Product product, Category category, PicDetail pic) {
            PrductServices services = new PrductServices();

            services.EditProduct(product, category, pic);


            return RedirectToAction("Form", initdetil());
        }

    }
}