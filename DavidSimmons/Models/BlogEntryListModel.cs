using DavidSimmons.Contracts;
using System.Collections.Generic;

namespace DavidSimmons.Models
{
    public class BlogEntryListModel : ViewModelBase
    {
        //TODO: PROBABLY SHOULDN'T BE USING CONTRACT CLASSES IN MY MODELS
        public List<BlogEntry> Entries { get; set; }
    }
}