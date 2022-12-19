using DavidSimmons.Client;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Text;
using System.Web.Mvc;

namespace DavidSimmons.CustomAttributes
{
    public class WebLoggingAttribute : ActionFilterAttribute
    {
        private bool IsWebLoggingEnabled
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["WebLoggingEnabled"]);
            }
        }

        [Dependency]
        public IWebLogClient LoggingClient { get; set; }

        public WebLoggingAttribute()
        {

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
 
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var parameters = GetParameters(filterContext);
            var controllerName = filterContext.Controller.GetType().Name;
            var actionName = filterContext.ActionDescriptor.ActionName;

            if (IsWebLoggingEnabled)
            {
                LoggingClient.LogPageVisit(actionName, controllerName, parameters);
            }
        }

        private string GetParameters(ActionExecutingContext filterContext)
        {
            var parameters = filterContext.ActionDescriptor.GetParameters();

            if(parameters == null)
            {
                return null;
            }
            else
            {
                StringBuilder parameterBuilder = new StringBuilder();

                foreach(var p in parameters)
                {
                    parameterBuilder.Append(p.ParameterName);
                    parameterBuilder.Append('=');
                    parameterBuilder.Append(p.DefaultValue);
                    parameterBuilder.Append(';');
                }

                return parameterBuilder.ToString();
            }
        }
    }
}