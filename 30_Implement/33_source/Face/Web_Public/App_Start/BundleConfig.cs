using System.Web;
using System.Web.Optimization;

namespace Web_Public
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));
            // Contents
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Themes/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Themes/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/Themes/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Themes/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/Themes/assets/global/plugins/select2/css/select2.min.css",
                      "~/Themes/assets/global/plugins/select2/css/select2-bootstrap.min.css",
                      "~/Themes/assets/global/css/components-md.min.css",
                      "~/Themes/assets/global/css/plugins-md.min.css"
                      ));
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Themes/assets/global/plugins/jquery.min.js",
                "~/Themes/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Themes/assets/global/plugins/js.cookie.min.js",
                "~/Themes/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Themes/assets/global/plugins/jquery.blockui.min.js"
                ));
            bundles.Add(new ScriptBundle("~/Content/js2").Include(

                // valudate
                "~/Themes/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
              "~/Themes/assets/global/plugins/jquery-validation/js/additional-methods.min.js",
              "~/Themes/assets/global/plugins/select2/js/select2.full.min.js",
              "~/Themes/assets/global/plugins/backstretch/jquery.backstretch.min.js",
              // global
              "~/Themes/assets/global/scripts/app.min.js"
              ));

            // Login
            bundles.Add(new StyleBundle("~/css/login").Include(
                     "~/Themes/assets/pages/css/login-5.min.css" ));

            bundles.Add(new ScriptBundle("~/Sctipt/Login").Include(
                   "~/Themes/assets/pages/scripts/login-5.min.js"));
        }
    }
}
