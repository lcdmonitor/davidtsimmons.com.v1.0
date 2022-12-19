using DavidSimmons.Contracts;
using Rebus;
using System;
using System.Diagnostics;
using DavidSimmons.Repository.Interfaces;
using Microsoft.AspNet.SignalR;
using DavidSimmons.Hubs;

namespace DavidSimmons.Processing.Handlers
{
    public class HandleBlogEntry : IHandleMessages<BlogEntryPosted>
    {
        private IBlogEntryRepository _blogRepository { get; set; }

        public HandleBlogEntry(IBlogEntryRepository blogRepository)
        {
            this._blogRepository = blogRepository;
        }

        public void Handle(BlogEntryPosted message)
        {

            _blogRepository.PostEntry(message);
            
            //SignalR Sample Hub Context Call

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AlertHub>();
            hubContext.Clients.All.Notify(new { Message = "Blog Entry Posted", ID = Guid.NewGuid().ToString() });

            Trace.WriteLine("Blog Entry Posted: " + message.Title + " Processed by: " + this.GetType().FullName);
        }
    }
}
