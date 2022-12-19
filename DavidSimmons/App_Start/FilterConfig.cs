using System.Web.Mvc;

namespace DavidSimmons
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //TODO: FIND A BETTER WAY OF CONTROLLING THIS PROGRAMATICALLY.
            #if !DEBUG
            filters.Add(new RequireHttpsAttribute());
            #endif
        }
    }
}
