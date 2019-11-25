using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using vegetable.Controllers;

namespace vegetable.Helper
{
    public static class MyHtmlHelper 
    {
       
        public static MvcHtmlString Dropdownlist(this HtmlHelper htmlHelper,string name,string ActionName) {
            ItemContext _context = new ItemContext();
            var Dropdown = new TagBuilder("select");
            Dropdown.Attributes.Add("name", "CategoryName");
            Dropdown.Attributes.Add("id", "selectitem");
            Dropdown.Attributes.Add("class", "form-control");
           
            var data = from c in _context.Categories select c;
            StringBuilder option = new StringBuilder();
            if (data != null)
            {
                foreach (var v in data)
                {

                    option = option.Append("<option asp-for=CategoryName class=form-control value=" + v.CategoryName + " id=CategoryName name=CategoryName>" + v.CategoryName + "</option>");

                }
            }
            option = option.Append("<option class=btn-primary value=" + ActionName + ">新增類別</option>");
            Dropdown.InnerHtml = option.ToString();
            return MvcHtmlString.Create(Dropdown.ToString(TagRenderMode.Normal));
        }
    }
}