using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.ViewModels
{
    public class CommentViewModel
    {
        public int CommentID { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public string UserID { get; set; }
        public string User { get; set; }
        public int BlogPostID { get; set; }
    }
}