
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Azure;
//using Microsoft.Azure;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DavidSimmons.Hubs;

namespace BlogPostProcessorRole
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                // Setup the cors middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                    EnableJavaScriptProxies = false
                };

                //setup service bus for SignalRScaleout
                string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
                GlobalHost.DependencyResolver.UseServiceBus(connectionString, "BlogPostProcessorRole");
                GlobalHost.DependencyResolver.Register(typeof(IAssemblyLocator), () => new HubAssemblyLocator());

                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch is already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });


            
            app.UseStaticFiles(
                    new StaticFileOptions()
                    {
                        RequestPath = new PathString(""),
                        FileSystem = new PhysicalFileSystem(@".\Web"),
                    }
                );
        }
    }
}
