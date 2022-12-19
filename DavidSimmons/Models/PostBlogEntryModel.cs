using DavidSimmons.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DavidSimmons.Models
{
    public class PostBlogEntryModel : ViewModelBase
    {
        //TODO: CREATE NEW VIEW MODEL HERE, SHOULDN'T LET CONTRACTS BLEED IN
        [DisplayName("Post Image")]
        public Image PostImage { get; set; }

        [DisplayName("Post Title")]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [DisplayName("Post Date")]
        public DateTime PostDate { get; set; }

        [DisplayName("Summary")]
        [Required(AllowEmptyStrings = false)]
        public string Summary { get; set; }

        [DisplayName("Post Content")]
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.MultilineText)]
        public string PostContent { get; set; }

        //TODO: CREATE NEW VIEW MODEL HERE, SHOULDN'T LET CONTRACTS BLEED IN
        public List<Attachment> Attachments { get; set; }

        [DisplayName("Posted by")]
        public string PostedByUser { get; set; }

        public string Key { get; set; }
    }
}