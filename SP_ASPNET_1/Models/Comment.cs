using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        // ForeignKeys
        public string UserID { get; set; }

        [Required]
        public int BlogPostID { get; set; }

        // Relations
        [ForeignKey("BlogPostID")]
        public BlogPost BlogPost { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
    }
}