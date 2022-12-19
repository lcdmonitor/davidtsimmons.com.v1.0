using DavidSimmons.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DavidSimmons.Models
{
    public class UpdateBlogEntryModel : ViewModelBase
    {
        //TODO: CREATE NEW VIEW MODEL HERE, SHOULDN'T LET CONTRACTS BLEED IN
        [DisplayName("Post Image")]
        public Image PostImage { get; set; }

        [DisplayName("Post Title")]
        [Required(AllowEmptyStrings = false)]
        [Editable(false)]
        public string Title { get; set; }

        [DisplayName("Post Date")]
        [Editable(false)]
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
        [Editable(false)]
        public string PostedByUser { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PartitionKey { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Key { get; set; }
    }
}