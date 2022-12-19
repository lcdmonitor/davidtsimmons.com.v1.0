using DavidSimmons.Contracts;
using DavidSimmons.Hubs;
using DavidSimmons.Repository.Interfaces;
using Microsoft.AspNet.SignalR;
using Rebus;
using System;
using System.Diagnostics;

namespace DavidSimmons.Processing.Handlers
{
    public class UpdatePageVisitsByDay : IHandleMessages<PageVisited>
    {
        private IWebLogRepository _webLogRepository { get; set; }

        public UpdatePageVisitsByDay(IWebLogRepository webLogRepository)
        {
            this._webLogRepository = webLogRepository;
        }
        public void Handle(PageVisited message)
        {
            this._webLogRepository.UpdatePageVisitsByDay(message);
        }
    }
}
