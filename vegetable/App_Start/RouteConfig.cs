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
              url: "Products/{query}",
              defaults: new
              {
                  controller = "FrontEnd",
                  action = "ShowProducts",
                  query = UrlParameter.Optional
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

            //手環
            routes.MapRoute(
               name: "Bracelets",
               url: "Products/Bracelets/{id}",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "ProductIndex",
                   id = UrlParameter.Optional
               }
           );

            //項鍊
            routes.MapRoute(
               name: "Necklaces",
               url: "Products/Necklaces/{id}",
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

            //結帳頁
            routes.MapRoute(
               name: "Checkout",
               url: "Checkout",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "Checkout"
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
