using System.Web.Optimization;

namespace DavidSimmons
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.jgrowl.js",
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/dsimmons").Include(
                      "~/Scripts/ClientNotification.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery.jgrowl.css",
                      "~/Content/blog.css"
                      ));

            bundles.Add(new StyleBundle("~/editor/css").Include(
                      "~/Content/default.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/SignalR").Include(
                        "~/Scripts/jquery.SignalR-*"));

            bundles.Add(new ScriptBundle("~/bundles/editor").Include(
                        "~/Scripts/jquery.sceditor.bbcode.js"));
        }
    }
}
