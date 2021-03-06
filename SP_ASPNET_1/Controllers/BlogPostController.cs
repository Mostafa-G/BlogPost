﻿using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using System.Web.Mvc;
using System.Web.Routing;
using SP_ASPNET_1.BusinessLogic;
using System;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

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
        public ActionResult LikeBlogPost(int id)
        {
            BlogPost blogPost = this._blogPostOperations.GetBlogPostByIdD(id);
            if (blogPost == null)
            {
                return Json(new { PostLikes = -1, ResultState = "Fail", JsonRequestBehavior.AllowGet });
            }

            blogPost = this._blogPostOperations.LikeBlogPost(id, User.Identity.GetUserId());
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

            var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (!_userManager.IsInRole(User.Identity.GetUserId(), "Admin") 
                && blogPost.AuthorID != User.Identity.GetUserId())
            {
                return Content(HttpStatusCode.Unauthorized.ToString(), "Unauthorized");
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
                var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                if (!_userManager.IsInRole(User.Identity.GetUserId(), "Admin")
                    && postToEdit.AuthorID != User.Identity.GetUserId())
                {
                    return Json(new { ResultState = "Unauthorized", JsonRequestBehavior.AllowGet });
                }

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
                var postToDelete = this._blogPostOperations.GetBlogPostByIdD(id);
                var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                if (!_userManager.IsInRole(User.Identity.GetUserId(), "Admin")
                    && postToDelete.AuthorID != User.Identity.GetUserId())
                {
                    return Content(HttpStatusCode.Unauthorized.ToString(), "Unauthorized");
                }

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
