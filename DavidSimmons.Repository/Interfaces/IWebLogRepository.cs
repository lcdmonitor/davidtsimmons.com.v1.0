using DavidSimmons.Contracts;

namespace DavidSimmons.Repository.Interfaces
{
    public interface IWebLogRepository
    {
        void LogPageVisited(PageVisited pageVisitedEvent);

        void UpdatePageVisitsByURL(PageVisited pageVisitedEvent);
        void UpdatePageVisitsByDay(PageVisited message);
    }
}
