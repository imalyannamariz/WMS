using System.Web;
using System.Web.Optimization;

namespace WMS
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
                      "~/Content/site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/layout.css"));

            //Datatables JS
            bundles.Add(new ScriptBundle("~/bundles/DataTables-js").Include(
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/responsive.bootstrap4.js"));

            //Datatables css
            bundles.Add(new StyleBundle("~/bundles/DataTables-css").Include(
                    "~/Content/DataTables/css/jquery.dataTables.css",
                     "~/Content/DataTables/css/responsive.bootstrap4.css"));

            //Inventory
            bundles.Add(new ScriptBundle("~/bundles/Inventory-js").Include(
                      "~/Scripts/Inventory/Inventory.js"));

            //Inventory
            bundles.Add(new ScriptBundle("~/bundles/Warehouse-js").Include(
                      "~/Scripts/Warehouse/Warehouse.js"));

            //Inventory
            bundles.Add(new ScriptBundle("~/bundles/Category-js").Include(
                      "~/Scripts/Category/Category.js"));
        }
    }
}
