using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RealShop.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include
                ("~/Content/SiteStyleSheet.css",
                 "~/Content/bootstrap.css",
                 "~/Content/bootstrap-theme.css",
                 "~/Content/jquery-ui-1.11.4.custom smootheness/jquery-ui-1.11.4.custom/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/Scripts/CommonJavascriptLibs").Include
                ("~/Scripts/jquery-2.1.4.js",
                 "~/Scripts/bootstrap.js",
                 "~/Scripts/jquery-ui-1.11.4.js",
                 "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle ("~/Scripts/LayoutSitePage").Include
                ("~/Scripts/CustomScript/LayoutSitePage.js"));

            bundles.Add(new ScriptBundle("~/Scripts/OneProductInfoPage").Include
               ( "~/Scripts/CustomScript/OneProductInfoPage.js"));
        }
    }
}