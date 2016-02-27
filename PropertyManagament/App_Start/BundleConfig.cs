using System.Web;
using System.Web.Optimization;

namespace PropertyManagament
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                         "~/Content/bootstrap.css",
                         "~/Content/bootstrap-theme.css",                  
                         "~/Content/bootstrap.min.css"                 
             ));

            bundles.Add(new StyleBundle("~/Content/css").Include(                     
                      "~/Content/site.css"));      

            bundles.Add(new StyleBundle("~/Content/ng-grid").Include(
                    "~/Content/ng-grid.css",
                    "~/Content/ng-grid.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
           "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));  

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/ng-grid").Include(
                    "~/Scripts/ng-grid.js",
                    "~/Scripts/ng-grid.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app-controller").Include(
                    "~/Scripts/app-controller.js"));


            bundles.Add(new ScriptBundle("~/bundles/data-controller").Include(
                    "~/Scripts/data-controller.js"));
        }

    }
}
