using Microsoft.WindowsAzure.Storage.Table;
using System;
using DavidSimmons.Core.Extensions;
using DavidSimmons.Contracts;
using Newtonsoft.Json;

namespace DavidSimmons.Repository.Entities
{
    public class BlogEntryEntity : TableEntity
    {
        public string BlogEntryData { get; set; }

        public BlogEntryEntity()
        {
        }

        public BlogEntryEntity(BlogEntryPosted entry)
        {
            //TODO: SKETCHY, entry data should be different type than event
            this.BlogEntryData = JsonConvert.SerializeObject(entry);
            
            this.RowKey = entry.Title.ToStorageKey();

            //TODO: fIND BETTER PARTIONING STRATEGY
            this.PartitionKey = entry.PostDate.Date.ToMonthAndYearStorageKey();
        }
    }
}
