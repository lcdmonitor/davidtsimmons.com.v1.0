using System.Web.Mvc;
using System.Web.Routing;

namespace DavidSimmons
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "EditEntry",
                url: "Admin/Edit/{monthOfPost}/{entryKey}",
                defaults: new { controller = "Admin", action = "Edit" }
            );

            routes.MapRoute(
                name: "ReadEntry",
                url: "Blog/Read/{monthOfPost}/{entryKey}",
                defaults: new { controller = "Blog", action = "Read" }
            );

            routes.MapRoute(
                name: "EntriesByMonth",
                url: "Blog/Month/{monthKey}",
                defaults: new { controller = "Blog", action = "Month"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
