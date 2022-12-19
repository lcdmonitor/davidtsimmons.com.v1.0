using DavidSimmons.Contracts;
using DavidSimmons.Hubs;
using DavidSimmons.Repository.Interfaces;
using Microsoft.AspNet.SignalR;
using Rebus;
using System;
using System.Diagnostics;

namespace DavidSimmons.Processing.Handlers
{
    public class UpdateBlogEntriesByMonth : IHandleMessages<BlogEntryPosted>
    {
        private IBlogEntryRepository _blogRepository { get; set; }

        public UpdateBlogEntriesByMonth(IBlogEntryRepository blogRepository)
        {
            this._blogRepository = blogRepository;
        }

        public void Handle(BlogEntryPosted message)
        {
            this._blogRepository.UpdateBlogEntriesByMonth(message);

            //SignalR Sample Hub Context Call

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AlertHub>();
            hubContext.Clients.All.Notify(new { Message = "Update Blog Entry By Month", ID = Guid.NewGuid().ToString() });


            Trace.WriteLine("Updating Entries By Month: " + message.Title + " by: " + this.GetType().FullName);
        }
    }
}
