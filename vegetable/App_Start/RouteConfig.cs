using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace vegetable
{
    public class RouteConfig
    {
        public static void RegisterRoutes (RouteCollection routes)
        {
            //show product 
            routes.MapRoute(
              name: "ShowProducts",
              url: "Products/{SearchCondition}",
              defaults: new
              {
                  controller = "FrontEnd",
                  action = "ShowProducts",
                  SearchCondition = UrlParameter.Optional
              }
          );

            //耳環
            routes.MapRoute(
               name: "Earrings",
               url: "Products/Earrings/{id}",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "ProductIndex",
                   id = UrlParameter.Optional
               }
           );

            //購物車
            routes.MapRoute(
               name: "Cart",
               url: "Cart",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "Cart"
               }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "HomePage",
               url: "{controller}/{action}/{id}",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "Index",
                   id = UrlParameter.Optional
               }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
