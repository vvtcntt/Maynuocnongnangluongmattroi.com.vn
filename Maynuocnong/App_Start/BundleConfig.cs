using System.Web;
using System.Web.Optimization;

namespace Maynuocnong
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/Display/Style/css").Include(
                      "~/Content/Display/Style/Default.css",
                      "~/Content/Display/Style/Default_Rs.css",
                      "~/Content/Display/Style/news.css",
                      "~/Content/Display/Style/News_Rs.css",
                      "~/Content/Display/Style/product.css",
                      "~/Content/Display/Style/Popup.css",
                        "~/Content/Display/Style/Order.css",
                          "~/Content/Display/Style/Contact.css",
                            "~/Content/Display/Style/Contact_Rs.css", "~/Content/Display/Style/baogia.css",
                            "~/Content/Display/Style/baogia_Rs.css",
                        "~/Content/Display/Style/Order_Rs.css", "~/Content/bootstrap.css",

                      "~/Content/Display/Style/product_Rs.css" ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
