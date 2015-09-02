using System.Web.Optimization;

namespace Memento.App_Start
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
						"~/Scripts/jquery.unobtrusive-ajax.js"));

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					    "~/Scripts/bootstrap.js",
					    "~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
					    "~/Scripts/fileinput.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
						"~/Scripts/angular.min.js",
						"~/Scripts/angular-loader.min.js",
						"~/Scripts/angular-route.min.js",
						"~/Scripts/app.js",
						"~/Scripts/AngularControllers/*.js",
						"~/Scripts/AngularDirectives/*.js",
						"~/Scripts/AngularServices/*.js"));

			bundles.Add(new ScriptBundle("~/bundles/custom-scripts").Include(
					    "~/Scripts/CustomScripts/*.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					    "~/Content/CSS/*.css",
						"~/Content/bootstrap-fileinput/css/fileinput.min.css"));
		}
	}
}
