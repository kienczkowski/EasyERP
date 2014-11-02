using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace EasyERP.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/js/Chart.js",
                        "~/Scripts/js/kalendarz.js",
                        "~/Scripts/mainJS.js",
                        "~/Scripts/CommonAction.js",
                        "~/Scripts/jquery.datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/Common").Include(
                "~/Scripts/CommonAction.js",
                "~/Scripts/jquery.validate*"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/reset.css",
                      "~/Content/style.css",
                      "~/Content/PagedList.css",
                      "~/Content/themes/base/all.css",
                      "~/Content/themes/base/jquery.datetimepicker.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}