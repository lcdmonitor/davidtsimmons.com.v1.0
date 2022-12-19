using DavidSimmons.CustomAttributes;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace DavidSimmons
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterContainer();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //register Logging Filter
            //TODO: REWORK USING iOC FOR ATTRIBUTES: http://hwyfwk.com/blog/2013/10/06/mvc-filters-with-dependency-injection/
            //TODO: Replace the bogus serviceLocation here
            GlobalFilters.Filters.Add(UnityConfig.GetConfiguredContainer().BuildUp(typeof(WebLoggingAttribute), new WebLoggingAttribute()));
            GlobalFilters.Filters.Add(UnityConfig.GetConfiguredContainer().BuildUp(typeof(NavigationMetaDataLoader), new NavigationMetaDataLoader()));

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
