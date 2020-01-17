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
              name: "SearchProducts",
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
               url: "Products/{cat}/{id}",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "ProductIndex",
                   cat = UrlParameter.Optional,
                   id = UrlParameter.Optional
               }
           );

            //手環
            // routes.MapRoute(
            //    name: "Bracelets",
            //    url: "Products/Bracelets/{id}",
            //    defaults: new
            //    {
            //        controller = "FrontEnd",
            //        action = "ProductIndex",
            //        id = UrlParameter.Optional
            //    }
            //);

            //項鍊
            // routes.MapRoute(
            //    name: "Necklaces",
            //    url: "Products/Necklaces/{id}",
            //    defaults: new
            //    {
            //        controller = "FrontEnd",
            //        action = "ProductIndex",
            //        id = UrlParameter.Optional
            //    }
            //);

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

            //登入頁
            routes.MapRoute(
               name: "Login",
               url: "Login",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "LoginPage"
               }
            );

            //忘記密碼頁
            routes.MapRoute(
               name: "ForgetPassword",
               url: "Login/ForgotPassword",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "ForgetPassword"
               }
            );

            //訂單頁
            routes.MapRoute(
               name: "Order",
               url: "Order",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "MemberPageOrder"
               }
            );

            //訂單明細頁
            routes.MapRoute(
               name: "OrderDetail",
               url: "Order/{id}",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "MemberPageOrderDetail"
               }
            );

            //會員設定頁
            routes.MapRoute(
               name: "MemberSetting",
               url: "Member",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "MemberPageSetting"
               }
            );

            //會員註冊頁
            routes.MapRoute(
               name: "MemberRegist",
               url: "Regist",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "MemberRegist"
               }
            );

            //會員願望清單頁
            routes.MapRoute(
               name: "MemberWishList",
               url: "WishList",
               defaults: new
               {
                   controller = "FrontEnd",
                   action = "MemberPageWishlist"
               }
            );

            //後台
            //首頁
            //routes.MapRoute(
            //   name: "MemberWishList",
            //   url: "Admin",
            //   defaults: new
            //   {
            //       controller = "FrontEnd",
            //       action = "MemberPageWishlist"
            //   }
            //);

            //產品管理頁
            routes.MapRoute(
               name: "ProductManagement",
               url: "AdminProduct",
               defaults: new
               {
                   controller = "NewProduct",
                   action = "Index"
               }
            );

            //會員管理頁
            routes.MapRoute(
               name: "MemberManagement",
               url: "AdminMember",
               defaults: new
               {
                   controller = "Members",
                   action = "Index"
               }
            );

            //類別管理頁
            routes.MapRoute(
               name: "CategoryManagement",
               url: "AdminCategory",
               defaults: new
               {
                   controller = "Category",
                   action = "Index"
               }
            );

            //績效管理頁
            routes.MapRoute(
               name: "ChartManagement",
               url: "AdminChart",
               defaults: new
               {
                   controller = "Report",
                   action = "Index"
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
