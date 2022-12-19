namespace DavidSimmons.Client
{
    public interface IWebLogClient
    {
        void LogPageVisit(string action, string controller, string parameters);
    }
}
