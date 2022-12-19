using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DavidSimmons.Extensions
{    public static class Helpers
    {
        public static IHtmlString CDNImage(this HtmlHelper helper, string imageFileName)
        {
            string rootDomain = "az814479.vo.msecnd.net";
            string rootContainer = "images";

            string imageTag = "<img src=\"{0}://{1}/{2}/{3}\">";

            //TODO: MAKE THIS HTTP/HTTPS DYNAMIC
            return new HtmlString(string.Format(imageTag, "https", rootDomain, rootContainer, imageFileName));
        }
    }
}