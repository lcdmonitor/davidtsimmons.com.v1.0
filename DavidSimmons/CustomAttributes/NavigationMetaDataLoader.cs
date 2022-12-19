using DavidSimmons.Client;
using DavidSimmons.Contracts;
using DavidSimmons.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DavidSimmons.CustomAttributes
{
    public class NavigationMetaDataLoader : ActionFilterAttribute
    {
        [Dependency]
        public IBlogClient BlogClient { get; set; }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            if(model == null)
            {
                return;
            }

            var modelBase = model as ViewModelBase;

            if (modelBase != null)
            {
                modelBase.ArchiveList = new List<ArchiveModel>();
                List<Archive> entriesByMonth = BlogClient.GetArchive();
                foreach (var e in entriesByMonth)
                {
                    modelBase.ArchiveList.Add(new ArchiveModel { Label = e.Label, Key = e.Key });
                }
            }
        }
    }
}