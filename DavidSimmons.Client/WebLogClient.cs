using DavidSimmons.Contracts;
using DavidSimmons.Core.Cloud.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Web;

namespace DavidSimmons.Client
{
    public class WebLogClient : IWebLogClient
    {
        private IBusFactory _busFactory;

        public WebLogClient(IBusFactory busFactory)
        {
            _busFactory = busFactory;
        }

        public void LogPageVisit(string action, string controller, string parameters)
        {
            //TODO: the way unity is being passed around is pretty sketchy, MAYBE CREATE A CONTAINER PROVIDER AS A constuctory,DEDPENDENCY TO BusFactory
            //TODO: SPLIT CREATE BUS AND CREATE BUS FOR SEND ONLY INTO 2 DIFFERENT METHODS, or make client/ server bus factories.
            using (var bus = this._busFactory.CreateBus(new UnityContainer(), true))
            {
                bus.Send(new PageVisited()
                {
                    UrlReferrer = HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.ToString() : null,
                    RawUrl = HttpContext.Current.Request.RawUrl,
                    Controller = controller,
                    Action = action,
                    Parameters = parameters,
                    UserAgent = HttpContext.Current.Request.UserAgent,
                    VistorIP = HttpContext.Current.Request.UserHostAddress,
                    Browser = HttpContext.Current.Request.Browser != null ? HttpContext.Current.Request.Browser.Browser : null,
                    VisitDate = DateTime.Now
                });
            }
        }
    }
}
