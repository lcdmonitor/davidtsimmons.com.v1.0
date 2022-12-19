using DavidSimmons.Client;
using DavidSimmons.Contracts;
using DavidSimmons.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DavidSimmons.Controllers
{
    public class BlogController : Controller
    {
        private IBlogClient _blogClient;

        public BlogController(IBlogClient blogClient)
        {
            this._blogClient = blogClient;
        }

        [Route("Read/{monthOfPost}/{entryKey}")]
        public ActionResult Read(string monthOfPost, string entryKey)
        {
            //todo: seperate read activity into separater client, it could be abused here
            var entry = _blogClient.GetEntry(monthOfPost, entryKey);

            //TODO:This is a little sketchy
            entry.PartitionKey = monthOfPost;
            entry.Key = entryKey;

            return View(new BlogEntryModel() { Entry = entry, MetaDescription = entry.Summary });
        }

        [Route("Month/{monthKey}")]
        public ActionResult Month(string monthKey)
        {
            //TODO: IMPLEMENT BY MONTH VIEW
            return View(new BlogEntryListModel() { Entries = _blogClient.GetEntriesByMonth(monthKey) });
        }
    }
}