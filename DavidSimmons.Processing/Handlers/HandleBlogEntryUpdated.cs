using DavidSimmons.Contracts;
using DavidSimmons.Hubs;
using DavidSimmons.Repository.Interfaces;
using Microsoft.AspNet.SignalR;
using Rebus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSimmons.Processing.Handlers
{
    public class HandleBlogEntryUpdated : IHandleMessages<BlogEntryUpdated>
    {
        private IBlogEntryRepository _blogRepository { get; set; }

        public HandleBlogEntryUpdated(IBlogEntryRepository blogRepository)
        {
            this._blogRepository = blogRepository;
        }

        public void Handle(BlogEntryUpdated message)
        {
            this._blogRepository.UpdateEntry(message);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AlertHub>();
            hubContext.Clients.All.Notify(new { Message = "Blog Entry Updated", ID = Guid.NewGuid().ToString() });

            Trace.WriteLine("Blog Entry Updated: " + message.Title + " Processed by: " + this.GetType().FullName);
        }
    }
}
