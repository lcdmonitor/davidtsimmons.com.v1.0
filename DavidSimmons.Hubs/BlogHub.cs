using Microsoft.AspNet.SignalR;
using System;

namespace DavidSimmons.Hubs
{
    public class BlogHub : Hub
    {
        public void NotifyOfBlogPostStatus(string message)
        {
            //TODO: Cleanup
            Clients.All.NotifyOfBlogPostStatus(new { Message = "Update: Dave Was Here at: " + DateTime.Now.ToString(), ID = Guid.NewGuid().ToString() });
        }
    }
}