using DavidSimmons.Client;
using DavidSimmons.Contracts;
using DavidSimmons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DavidSimmons.Controllers
{
    public class AdminController : Controller
    {
        private IBlogClient _blogClient;

        public AdminController(IBlogClient blogClient)
        {
            this._blogClient = blogClient;
        }

        //TODO Add Authorization back to all methods
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }


        //[Authorize]
        [Route("Edit/{monthOfPost}/{entryKey}")]
        public ActionResult Edit(string monthOfPost, string entryKey)
        {
            var postToEdit = _blogClient.GetEntry(monthOfPost, entryKey);

            postToEdit.Key = entryKey;
            postToEdit.PartitionKey = monthOfPost;

            var updateBlogEntryModel = new UpdateBlogEntryModel()
            {
                Title = postToEdit.Title,
                Summary = postToEdit.Summary,
                PostContent = postToEdit.Text,
                PostDate = postToEdit.PostDate,
                PostedByUser = postToEdit.PostedByUser,
                Key = postToEdit.Key,
                PartitionKey = postToEdit.PartitionKey
            };

            return View(updateBlogEntryModel);
        }

        //[Authorize]
        [HttpPost]
        public JsonResult Edit(UpdateBlogEntryModel updatedEntry)
        {
            if (ModelState.IsValid)
            {
                _blogClient.UpdateBlogEntry(
                        new BlogEntryUpdated()
                        {
                            Title = updatedEntry.Title,
                            Text = updatedEntry.PostContent,
                            Summary = updatedEntry.Summary,
                            PartitionKey = updatedEntry.PartitionKey,
                            Key = updatedEntry.Key
                        });

                //TODO: THIS IS KIND OF A DUMMY SETUP FOR NOW;
                return Json(new
                {
                    Key = Guid.NewGuid().ToString(),
                    //TODO: Fix Me after fixing Authentication
                    Status = string.Concat("Accepted:", updatedEntry.Title, " from ", "FixMeDavid")
                    //Status = string.Concat("Accepted:", updatedEntry.Title, " from ", ClaimHelper.GetCurrentUserName())
                }) ;
            }
            else
            {
                //TODO: THIS IS KIND OF A DUMMY SETUP FOR NOW;
                return Json(new
                {
                    Key = Guid.NewGuid().ToString(),
                    Status = "rejected, Post is invalid"
                });
            }
        }


        //[Authorize]
        public ActionResult Post()
        {
            var model = new PostBlogEntryModel()
            {
                PostDate = DateTime.Now,
                PostedByUser = "FixMeDavid",
            };
            
            return View(model);
        }

        //[Authorize]
        [HttpPost]
        public JsonResult Post(PostBlogEntryModel entryToPost)
        {
            if (ModelState.IsValid)
            {
                _blogClient.PostBlogEntry(
                        new BlogEntryPosted()
                        {
                            Title = entryToPost.Title,
                            PostDate = DateTime.Now,
                            Text = entryToPost.PostContent,
                            Summary = entryToPost.Summary,
                            PostedByUser = "FixMeDavid"
                        });

                //TODO: THIS IS KIND OF A DUMMY SETUP FOR NOW;
                return Json(new
                {
                    Key = Guid.NewGuid().ToString(),
                    Status = string.Concat("Accepted:", entryToPost.Title, " from ", "FixMeDavid")
                });
            }
            else
            {
                //TODO: THIS IS KIND OF A DUMMY SETUP FOR NOW;
                return Json(new
                {
                    Key = Guid.NewGuid().ToString(),
                    Status = "rejected, Post is invalid"
                });
            }
        }

        //[Authorize]
        public ActionResult Upload()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> selectedFiles)
        {
            foreach (var file in selectedFiles)
            {
                _blogClient.UploadBlob(file.InputStream, file.FileName, file.ContentType);
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}