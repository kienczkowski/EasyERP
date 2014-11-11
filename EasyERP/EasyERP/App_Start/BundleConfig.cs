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
                        "~/Scripts/jquery.datetimepicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Common").Include(
                "~/Scripts/CommonAction.js",
                "~/Scripts/jquery.validate*"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/reset.css",
                      "~/Content/style.css",
                      "~/Content/PagedList.css",
                      "~/Content/themes/base/all.css",
                      "~/Content/themes/base/base.css",
                      "~/Content/themes/base/core.css",
                      "~/Content/themes/base/resizable.css",
                      "~/Content/themes/base/selectable.css",
                      "~/Content/themes/base/accordion.css",
                      "~/Content/themes/base/autocomplete.css",
                      "~/Content/themes/base/button.css",
                      "~/Content/themes/base/dialog.css",
                      "~/Content/themes/base/slider.css",
                      "~/Content/themes/base/tabs.css",
                      "~/Content/themes/base/datepicker.css",
                      "~/Content/themes/base/progressbar.css",
                      "~/Content/themes/base/theme.css",
                      "~/Content/datetimepicker.css"
                      ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}