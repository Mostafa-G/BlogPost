using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("BlogPostComment")]
    public class CommentController : Controller
    {
        private readonly BlogPostOperations _blogPostOperations = new BlogPostOperations();

        private readonly CommentOperations _commentOperations = new CommentOperations();

        [Authorize(Roles = "Admin, Author, User")]
        [Route("Create")]
        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            BlogPost blogPost = this._blogPostOperations.GetBlogPostByIdD(comment.BlogPostID);
            if (blogPost == null)
            {
                return Json(new { commentID = -1, ResultState = "Fail", JsonRequestBehavior.AllowGet });
            }


            comment.UserID = User.Identity.GetUserId();
            comment.DateTime = DateTime.Now;
           

            this._commentOperations.Create(comment);

            CommentViewModel commentViewModel = new CommentViewModel()
            {
                UserID = comment.UserID,
                User = comment.User.ToString(),
                Content = comment.Content,
                BlogPostID = comment.BlogPostID,
                CommentID = comment.CommentID,
                DateTime = comment.DateTime
            };

            return Json(new { Comment = JsonConvert.SerializeObject(commentViewModel), ResultState = "Success", JsonRequestBehavior.AllowGet });
        }

        [Authorize(Roles = "Admin, Author, User")]
        [Route("Delete/{id:int}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var commentToDelete = this._commentOperations.GetCommentByIdD(id);
                var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                if (!_userManager.IsInRole(User.Identity.GetUserId(), "Admin")
                    && commentToDelete.UserID != User.Identity.GetUserId())
                {
                    return Content(HttpStatusCode.Unauthorized.ToString(), "Unauthorized");
                }

                this._commentOperations.Delete(id);

                return Json(new { ResultState = "Success", JsonRequestBehavior.AllowGet });
            }
            catch
            {
                return Json(new { ResultState = "NotFound", JsonRequestBehavior.AllowGet });
            }
        }
    }
}