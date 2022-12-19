using DavidSimmons.Contracts;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using DavidSimmons.Core.Extensions;
using Newtonsoft.Json;

namespace DavidSimmons.Repository.Entities
{
    public class PageVisitEntity : TableEntity
    {
        public string PageVisitedData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageVistedEvent">event to store as an entity</param>
        /// <param name="partitionKey">data will be partitioned for scalability</param>
        public PageVisitEntity(PageVisited pageVistedEvent, string partitionKey, string sourceProcessor)
        {
            this.PageVisitedData = JsonConvert.SerializeObject(pageVistedEvent);

            this.RowKey = string.Concat(sourceProcessor, ":", Guid.NewGuid());
            this.PartitionKey = partitionKey;
        }
    }
}
