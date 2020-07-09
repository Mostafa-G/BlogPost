using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services.Description;
using SP_ASPNET_1.Models;

namespace SP_ASPNET_1.ViewModels
{
    public class BlogSinglePostViewModel : IBlogViewModel
    {
        public ApplicationUser Author { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}