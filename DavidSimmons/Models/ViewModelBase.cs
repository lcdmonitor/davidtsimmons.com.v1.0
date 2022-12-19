using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DavidSimmons.Models
{
    public class ViewModelBase
    {
        public String PageTitle { get; set; }
        public String MetaKeywords { get; set; }
        public String MetaDescription { get; set; }

        public List<ArchiveModel> ArchiveList { get; set; }
    }
}