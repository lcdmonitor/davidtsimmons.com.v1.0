//using Microsoft.Practices.Unity.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DavidSimmons.App_Start.UnityWebApiActivator), "Start")]

namespace DavidSimmons.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET</summary>
    public static class UnityWebApiActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            // todo: fix me missing Unity.WebApi nuget package
            // Use UnityHierarchicalDependencyResolver if you want to use a new child container for each IHttpController resolution.
            // var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetConfiguredContainer());
            //var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

            //GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}
