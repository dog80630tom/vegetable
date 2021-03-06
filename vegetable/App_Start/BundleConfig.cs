﻿using System.Web;
using System.Web.Optimization;

namespace vegetable
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles (BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.dataTables.min.js", "~/Scripts/dataTables.select.min.js", "~/Scripts/Chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js", "~/Scripts/jquery.nicescroll.js", "~/Scripts/jquery.scrollTo.min.js", "~/Scripts/common-scripts.js"));

            bundles.Add(new StyleBundle("~/Content/vegetable").Include(
                    "~/Content/fontawesome-all.css",
                    "~/Content/bootstrap.css",
                    "~/Content/Site.css"
                   ));

            bundles.Add(new StyleBundle("~/Content/adminCss").Include(
                    "~/content/jquery.dataTables.min.css",
                    "~/Content/Css/style-responsive.css",
                    "~/Content/Css/style.css",
                    "~/Content/Css/table-responsive.css",
                    "~/Content/Css/to-do.css",
                    "~/Content/Css/zabuto_calendar.css",
                    "~/Content/Css/datatables.css"));

            bundles.Add(new StyleBundle("~/Content/loadingCSS").Include(
                "~/Assets/node_modules/rocket-loader/css/loader.min.css",
                "~/Assets/node_modules/loading.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/loadingJS").Include(
                       "~/Assets/node_modules/rocket-tools/js/tools.min.js",
                       "~/Assets/node_modules/rocket-loader/js/loader.min.js",
                       "~/Assets/node_modules/loading.js"
                       ));

        }
    }
}
