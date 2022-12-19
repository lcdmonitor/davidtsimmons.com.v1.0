using Microsoft.AspNet.SignalR;
using System;

namespace DavidSimmons.Hubs
{
    public class AlertHub : Hub
    {
        public void AlertAllUsers(string message)
        {
            //TODO: Cleanup
            Clients.All.Notify(new { Message = "Update: Dave Was Here at: " + DateTime.Now.ToString(), ID = Guid.NewGuid().ToString() });
        }
    }
}
