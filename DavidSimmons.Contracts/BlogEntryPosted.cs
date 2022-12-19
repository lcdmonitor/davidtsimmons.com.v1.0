using System;
using System.Collections.Generic;

namespace DavidSimmons.Contracts
{
    public class BlogEntryPosted
    {
        public Image PostImage { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
        public List<Attachment> Attachments { get; set; }

        public string PostedByUser { get; set; }

    }
}
