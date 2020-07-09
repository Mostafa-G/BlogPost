using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using System.Web.Mvc;
using System.Web.Routing;
using SP_ASPNET_1.BusinessLogic;
using System;
using Microsoft.AspNet.Identity;

namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("Blog")]
    public class BlogPostController : Controller
    {

        private readonly BlogPostOperations _blogPostOperations = new BlogPostOperations();

        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            BlogIndexViewModel result = this._blogPostOperations.GetBlogIndexViewModel();
            ViewBag.Title = "Blog";
            return View(result);
        }


        [Route("Detail/{id:int?}")]
        [HttpGet]
        public ActionResult SinglePost(int? id)
        {
            ViewBag.Title = "single post";

            
            BlogSinglePostViewModel modelView;

            if (id == null)
            {
                modelView = this._blogPostOperations.GetLatestBlogPost();
            }
            else
            {
                modelView = this._blogPostOperations.GetBlogPostByIdFull(id.Value);
            }

            return View(modelView);
        }

        [Authorize]
        [Route("Detail/Random")]
        [HttpGet]
        public ActionResult RandomPost()
        {
            ViewBag.Title = "Random post";

            var viewModel = this._blogPostOperations.GetRandomBlogPost();

            return View("SinglePost", viewModel);
        }

        [Route("LatestPost")]
        [HttpGet]
        public ActionResult LatestPost()
        {
            var viewModel = this._blogPostOperations.GetLatestBlogPost();

            return this.PartialView("~/Views/BlogPost/_BlogPostRecentPartialView.cshtml", viewModel);
        }

        [Authorize(Roles = "Admin, Author, User")]
        [Route("LikePost/{id:int}")]
        [HttpPost]
        public ActionResult LikePost(int id)
        {
            BlogPost blogPost = this._blogPostOperations.LikePost(id, User.Identity.GetUserId());
            if(blogPost == null)
            {
                return Json(new { PostLikes = -1, ResultState = "Fail", JsonRequestBehavior.AllowGet });
            }
            return Json(new { PostLikes = blogPost.Likes, ResultState = "Success", JsonRequestBehavior.AllowGet });
        }

        [Authorize(Roles = "Admin, Author")]
        [Route("Create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Author")]
        [Route("Create")]
        [HttpPost]
        public ActionResult Create(BlogPost blogPost)
        {
            try
            {
                blogPost.DateTime = DateTime.Now;
                blogPost.AuthorID = User.Identity.GetUserId();

                if (ModelState.IsValid)
                {
                    this._blogPostOperations.Create(blogPost);

                    return RedirectToAction("SinglePost", blogPost.BlogPostID);
                }

                return View(blogPost);
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin, Author")]
        [Route("Edit/{id:int?}")]
        [HttpGet]
        public ActionResult EditBlogPost(int id)
        {
            BlogPost blogPost = this._blogPostOperations.GetBlogPostByIdD(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        [Authorize(Roles = "Admin, Author")]
        [Route("Edit/{id:int}")]
        [HttpPost]
        public ActionResult Edit(int id, BlogPost blogPost)
        {
            try
            {
                var postToEdit = this._blogPostOperations.GetBlogPostByIdD(id);
                postToEdit.Content = blogPost.Content;
                postToEdit.DateTime = blogPost.DateTime;
                postToEdit.Title = blogPost.Title;
                postToEdit.ImageUrl = blogPost.ImageUrl;
                postToEdit.BlogPostID = id;


                this._blogPostOperations.Update(postToEdit);

                return Json(new { ResultState = "Success", JsonRequestBehavior.AllowGet});
            }
            catch
            {
                return Json(new { ResultState = "Fail", JsonRequestBehavior.AllowGet });
            }
        }

        [Authorize(Roles = "Admin, Author")]
        [Route("Delete/{id:int}")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                this._blogPostOperations.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return this.HttpNotFound();
            }
        }
    }
}
