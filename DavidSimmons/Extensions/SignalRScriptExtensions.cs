using System.Web;
using System.Web.Mvc;

namespace DavidSimmons.Extensions
{
    public static class SignalRScriptExtensions
    {
        private static string RootSignalRPath
        {
            get
            {
                //TODO: Remove Hardcoded URL
                string rootPath = "http://dsimmons.cloudapp.net/";
                
                #if DEBUG
                rootPath = "http://localhost:8080/";
                #endif

                return rootPath;
            }
        }

        public static IHtmlString RenderHubInitializationScript(this HtmlHelper helper)
        {
            string signalHubTag =
                "<script type=\"text/javascript\">var hubConnectionUrl=\"{0}\";</script>";

            string hubsUrl = RootSignalRPath + "SignalR/Hubs";

            return new HtmlString(string.Format(signalHubTag, hubsUrl));
        }

        public static IHtmlString RenderHubScriptTag(this HtmlHelper helper)
        {
            string signalHubTag =
                "<script type=\"text/javascript\" src=\"{0}\"></script>";

            string hubsUrl = RootSignalRPath + "Hubs.js";

            return new HtmlString(string.Format(signalHubTag, hubsUrl));
        }
    }
}