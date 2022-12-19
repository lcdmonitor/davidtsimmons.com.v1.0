using System;

namespace DavidSimmons.Repository.Interfaces
{
    public interface IRawLoggingRepository
    {
        void CreateLogEntryForException(Exception ex, string instanceID);
    }
}
