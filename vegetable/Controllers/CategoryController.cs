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
        MyDBContext item = new MyDBContext();
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
            var initdata = initCategoryData();
            if (initdata == null)
            {
                return View(new List<Category>());
            }
            ViewBag.ISuccess = "false";
            return View(initdata);
        }


        public ActionResult Edit(int? Id)
        {
            TempData["CategoryID"] = Id;
            return View(initCategoryData().Find(x => x.CategoryID == Id));
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoryServices services = new CategoryServices();
            category.CategoryID = (int)TempData["CategoryID"];
            services.EditCategory(category);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? Id)
        {
            TempData["CategoryID"] = Id;
            var temp = item.Products.Any(x => x.CategoryID == Id);

            if (temp)
            {
                //需要做一個view跟他說有產品屬於這個類別所以不能刪除?
                TempData["CanNotDelete"] = true;
        
                return RedirectToAction("Index");
            }
            else
            {
                TempData["CanNotDelete"] = false;
                return View(initCategoryData().Find(x => x.CategoryID == Id)); 
            }               
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            category.CategoryID = (int)TempData["CategoryID"];
            CategoryServices services = new CategoryServices();
            services.DeleteCategory(category);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new List<Category>());
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            CategoryServices services = new CategoryServices();
            services.CreateCategory(category);
            return RedirectToAction("Index");
        }







    }
}

