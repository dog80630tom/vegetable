using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vegetable.Models.ViewModels;

namespace vegetable.Controllers
{
    public class SearchitemController : Controller
    {
        ItemContext Item = new ItemContext();
        private List<SearchViewModel> getSearch() {
            var product = from p in Item.Products select p;
            var Category = from c in Item.Categories select c;
            var PicDetail = from pic in Item.PicDetails select pic;
            var search = (from se in product

                         join pi in PicDetail on se.ProductID equals pi.ProductID
                         select new SearchViewModel
                         {
                              ProductID=se.ProductID, 
                               CategoryID=se.CategoryID,
                             PicUrl = pi.PicUrl,
                             ProductDescription = se.ProductDescription,
                             ProductName = se.ProductName,
                             ProductPrice = se.ProductPrice,
                             UnitsInStock = se.UnitsInStock

                             
                         }).ToList();
            foreach (var data in search)
            {

                data.CategoryName = Category.FirstOrDefault(x => x.CategoryID == data.CategoryID).CategoryName;
                data.CategoryPic= Category.FirstOrDefault(x => x.CategoryID == data.CategoryID).CategoryPic;


            }

            return search;
        }
        // GET: Searchitem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult item(string Seachstring)

        {
            var search = getSearch();
            int index;
            if (int.TryParse(Seachstring, out index))
            {
                index = int.Parse(Seachstring);
            }
            var data = search.Where(x => x.CategoryName == Seachstring || x.ProductName == Seachstring || x.ProductPrice == index || x.ProductDescription == Seachstring).ToList();
            ViewBag.Data = data.ToList();
            return View(search.ToList());
        }
        public ActionResult Search(string Seachstring)

        {
            var search = getSearch();
            int index;
            if (int.TryParse(Seachstring, out index))
            {
                index = int.Parse(Seachstring);
            }
            var data = search.Where(x => x.CategoryName == Seachstring || x.ProductName == Seachstring || x.ProductPrice == index || x.ProductDescription == Seachstring).ToList();
          
            return View("Search",data.ToList());
        }

    }
}