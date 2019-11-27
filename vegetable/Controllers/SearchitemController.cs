using Newtonsoft.Json;
using PagedList;
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
        private List<SearchViewModel> getSearch()
        {
            var product = from p in Item.Products select p;
            var Category = from c in Item.Categories select c;
            var PicDetail = from pic in Item.PicDetails select pic;
            var search = (from se in product

                          join pi in PicDetail on se.ProductID equals pi.ProductID
                          select new SearchViewModel
                          {
                              ProductID = se.ProductID,
                              CategoryID = se.CategoryID,
                              PicUrl = pi.PicUrl,
                              ProductDescription = se.ProductDescription,
                              ProductName = se.ProductName,
                              ProductPrice = se.ProductPrice,
                              UnitsInStock = se.UnitsInStock


                          }).ToList();
            /*
             上方為linq組成SearchViewModel
             join 變數名稱 in 資料表 on 變數.資料欄位 equals 變數.資料欄位這為sql的join語法
             */
            foreach (var data in search)
            {

                data.CategoryName = Category.FirstOrDefault(x => x.CategoryID == data.CategoryID).CategoryName;
                data.CategoryPic = Category.FirstOrDefault(x => x.CategoryID == data.CategoryID).CategoryPic;


            }
            /*
             上方為先找到categoryid的id然後把其他欄位塞進去
             */

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
            ViewBag.Data = JsonConvert.SerializeObject(search.ToList());
            return View(search.ToList());
        }
        
        public ActionResult Search(string Seachstring, int page = 1)
        {

            var search = getSearch();//把搜尋全部的資料放入變數裡
            int index;
            if (int.TryParse(Seachstring, out index))
            {
                index = int.Parse(Seachstring);
            }
            int pagesize = 1;//顯示主要幾個
            int currectpage = page < 1 ? 1 :page;//控制頁數
            ViewBag.Searchstr = Seachstring;//暫存搜尋字串
          var data=  search.Where(x => x.CategoryName.Contains(Seachstring) || x.ProductName.Contains(Seachstring) || x.ProductPrice == index || x.ProductDescription.Contains(Seachstring)).ToList();
               //搜尋語法Contains可以讓字串有部分相符的就會傳回符合的資料
            var list = data.ToPagedList(currectpage, pagesize );//currectpage參數為目前頁數，pagesize顯示多少筆
            return View("Search", list);
        }

        public ActionResult Detail(int? Id)
        {
            var search = getSearch();
            var data = search.Find(x => x.ProductID == Id);//find找到條件符合的項目
            return View("Detail", data);
        }
    }
}