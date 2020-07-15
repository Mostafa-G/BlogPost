using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.Models
{
    public class BlogPost
    {
        public BlogPost()
        {
            this.Likers = new HashSet<ApplicationUser>();
        }

        public int BlogPostID { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int Likes { get { return Likers.Count; } }


        // ForeignKeys
        public string AuthorID { get; set; }

        // Relations
        [ForeignKey("AuthorID")]
        public ApplicationUser Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ApplicationUser> Likers { get; set; }
    }
}