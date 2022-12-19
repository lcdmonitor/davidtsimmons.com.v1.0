using Microsoft.WindowsAzure.Storage.Table;
using DavidSimmons.Contracts;
using DavidSimmons.Core.Extensions;

namespace DavidSimmons.Repository.Entities
{
    public class BlogEntriesByMonthEntity : TableEntity
    {
        public BlogEntriesByMonthEntity()
        {

        }

        public BlogEntriesByMonthEntity(BlogEntryPosted entryPostedEvent)
        {
            this.PartitionKey = entryPostedEvent.PostDate.ToMonthAndYearStorageKey();
            this.RowKey = entryPostedEvent.PostDate.ToMonthAndYearStorageKey();

            this.Month = entryPostedEvent.PostDate.Month;
            this.Year = entryPostedEvent.PostDate.Year;

            this.Label = entryPostedEvent.PostDate.ToString("MMMM yyyy");

            this.PostCount = 1;
        }

        public int Month { get; set; }
        public int Year { get; set; }
        public string Label { get; set; }
        public int PostCount { get; set; }
    }
}
