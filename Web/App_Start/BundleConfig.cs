using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/vendor/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/vendor/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                        "~/Scripts/admin/*.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/vendor/bootstrap.js",
                      "~/Scripts/vendor/respond.js"));

            bundles.Add(new StyleBundle("~/Content/admin/css").Include(
                      "~/Content/style/bootstrap.css",
                      "~/Content/style/shared.css",
                      "~/Content/solid-state/css/font-awesome.min.css",
                      "~/Content/style/admin.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/style/bootstrap.css",
                      "~/Content/solid-state/css/font-awesome.min.css",
                      "~/Content/solid-state/css/main.css",
                      "~/Content/style/shared.css",
                      "~/Content/style/site.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
