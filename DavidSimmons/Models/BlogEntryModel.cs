using DavidSimmons.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DavidSimmons.Models
{
    public class BlogEntryModel : ViewModelBase
    {
        //TODO: CREATE NEW VIEW MODEL HERE, SHOULDN'T LET CONTRACTS BLEED IN
        public BlogEntry Entry;
    }
}