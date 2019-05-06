using System.Web;
using System.Web.Optimization;

namespace Libar_Hotel
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"
                     ));
            bundles.Add(new StyleBundle("~/Content/css-theme").Include(

                      "~/Content/cssTheme/open-iconic-bootstrap.min.css",
                      "~/Content/cssTheme/animate.css",
                      "~/Content/cssTheme/aos.css",
                      "~/Content/cssTheme/bootstrap-theme.css",
                      "~/Content/cssTheme/owl.carousel.min.css",
                      "~/Content/cssTheme/owl.theme.default.min.css",
                      "~/Content/cssTheme/owl.theme.default.min.css",
                      "~/Content/cssTheme/owl.theme.default.min.css",
                      "~/Content/cssTheme/magnific-popup.css",
                      "~/Content/cssTheme/ionicons.min.css",
                      "~/Content/cssTheme/bootstrap-datepicker.css",
                      "~/Content/cssTheme/jquery.timepicker.css",
                      "~/Content/cssTheme/flaticon.css",
                      "~/Content/cssTheme/ionicons.min.css",
                      "~/Content/cssTheme/icomoon.css",
                      "~/Content/cssTheme/style.css"



                ));
        }
    }
}
