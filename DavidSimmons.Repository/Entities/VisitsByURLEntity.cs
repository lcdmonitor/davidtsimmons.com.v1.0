using Microsoft.WindowsAzure.Storage.Table;
using DavidSimmons.Contracts;
using DavidSimmons.Core.Extensions;

namespace DavidSimmons.Repository.Entities
{
    public class VisitsByURLEntity : TableEntity
    {
        public VisitsByURLEntity()
        {

        }

        public VisitsByURLEntity(PageVisited pageVisitedEvent, string partitionKey,string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;

            this.URL = pageVisitedEvent.RawUrl;
            this.VisitCount = 1;
        }

        public string URL { get; set; }
        public int VisitCount { get; set; }
    }
}
