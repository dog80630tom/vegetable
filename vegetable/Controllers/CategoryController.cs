using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vegetable.Models;
using vegetable.Services;

namespace vegetable.Controllers
{
    public class CategoryController : Controller
    {
        ItemContext item = new ItemContext();
        // GET: Category

        public List<Category> initCategoryData()
        {
            List<Category> categoryData = new List<Category>();
            try
            {
                categoryData = (from c in item.Categories select c).ToList();

            }
            catch (Exception)
            {

                throw;
            }
            return (categoryData);
        }




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CatForm()
        {
            var initdata = initCategoryData();
            if (initdata == null)
            {
                return View(new List<Category>());
            }
            ViewBag.ISuccess = "false";
            return View(initdata);
        }

        //是否要做post的form
        public ActionResult Edit(int? Id)
        {
            TempData["CategoryID"] = Id;
            return View(initCategoryData().Find(x => x.CategoryID == Id));
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoryServices services = new CategoryServices();
            category.CategoryID= (int)TempData["CategoryID"];
            services.EditProduct(category);
            return RedirectToAction("CatForm");

        }





    }
}