using System;
using System.Collections.Generic;

namespace DavidSimmons.Contracts
{
    public class BlogEntryUpdated
    {
        public Image PostImage { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
        public List<Attachment> Attachments { get; set; }
        public string PartitionKey { get; set; }
        public string Key { get; set; }
    }
}
