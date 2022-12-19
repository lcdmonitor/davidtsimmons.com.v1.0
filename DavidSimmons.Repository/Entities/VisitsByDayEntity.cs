using DavidSimmons.Contracts;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using DavidSimmons.Core.Extensions;

namespace DavidSimmons.Repository.Entities
{
    public class VisitsByDayEntity : TableEntity
    {
        public DateTime VisitDate { get; set; }
        public int VisitCount { get; set; }

        public VisitsByDayEntity()
        {

        }

        public VisitsByDayEntity(PageVisited pageVisitedEvent, string partitionKey, string rowKey)
        {
            VisitDate = pageVisitedEvent.VisitDate;
            VisitCount = 1;

            this.RowKey = rowKey;
            this.PartitionKey = partitionKey;
        }
    }
}
