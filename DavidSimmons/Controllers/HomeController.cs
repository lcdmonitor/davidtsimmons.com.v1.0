using DavidSimmons.Client;
using DavidSimmons.Models;
using System.Web.Mvc;

namespace DavidSimmons.Controllers
{
    public class HomeController : Controller
    {
        private IBlogClient _blogClient;

        public HomeController(IBlogClient blogClient)
        {
            this._blogClient = blogClient;
        }

        public ActionResult Index()
        {
            return View(new BlogEntryListModel() { Entries = this._blogClient.GetFrontPageEntries() });
        }

        public ActionResult FrontPage(int id)
        {
            ViewBag.Message = "You requested page number: " + id;
            return View(new BlogEntryListModel() { Entries = this._blogClient.GetFrontPageEntries() });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}