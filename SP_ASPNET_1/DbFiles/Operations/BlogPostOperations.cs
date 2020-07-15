using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using SP_ASPNET_1.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.ModelBinding;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class BlogPostOperations
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly List<BlogPost> _blogPosts;

        public BlogPostOperations()
        {
            _blogPosts = _unitOfWork.BlogPostSchoolRepository.Get(null, b => b.OrderByDescending(d => d.DateTime), "Author,Comments").ToList();
            foreach(var blogPost in _blogPosts)
            {
                blogPost.Comments = blogPost.Comments.OrderBy(c => c.DateTime).ToList();
            }
        }

        public async Task<BlogIndexViewModel> GetBlogIndexViewModelAsync()
        {
            List<BlogPost> blogPosts = (await _unitOfWork.BlogPostSchoolRepository.GetAsync(null, b => b.OrderByDescending(d => d.DateTime), "Author")).ToList();

            return new BlogIndexViewModel()
            {
                BlogPosts = blogPosts.GetRange(1, blogPosts.Count - 1),
                BlogPost = blogPosts.Take(1).FirstOrDefault()
            };
        }

        public BlogPost LikeBlogPost(int postID, string userID)
        {
            try
            {
                ApplicationUser user = this._unitOfWork.UserSchoolRepository.GetByID(userID);
                BlogPost blogPost = this._unitOfWork.BlogPostSchoolRepository.GetByID(postID);

                if (user.LikedBlogPosts.Any(x => x.BlogPostID == postID)) 
                {
                    user.LikedBlogPosts.Remove(blogPost);
                }
                else
                {
                    user.LikedBlogPosts.Add(blogPost);
                }

                this._unitOfWork.BlogPostSchoolRepository.Update(blogPost);
                this._unitOfWork.UserSchoolRepository.Update(user);
                this._unitOfWork.Save();

                return blogPost;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public BlogIndexViewModel GetBlogIndexViewModel()
        {
            if (!_blogPosts.Any())
            {
                return new BlogIndexViewModel();
            }
            return new BlogIndexViewModel()
            {
                BlogPosts = _blogPosts.GetRange(0, _blogPosts.Count),
                BlogPost = _blogPosts.Take(1).FirstOrDefault()
            };
        }

        public BlogPost GetBlogPostByIdD(int id)
        {
            return _blogPosts.SingleOrDefault(x => x.BlogPostID == id);
        }

        public BlogSinglePostViewModel GetBlogPostByIdFull(int id)
        {
            return _blogPosts.FirstOrDefault(x => x.BlogPostID == id).ToBlogSinglePostViewModel();
        }

        public BlogSinglePostViewModel GetLatestBlogPost()
        {
            return _blogPosts.Select(StaticHelpers.ToBlogSinglePostViewModel)
                .FirstOrDefault();
        }


        public BlogSinglePostViewModel GetRandomBlogPost()
        {
            //TODO: Investigate
            if (_blogPosts.Count is 0)
            {
                return null;
            }

            Random rnd = new Random();

            var randomPost = _blogPosts[rnd.Next(_blogPosts.Count)];
            return randomPost.ToBlogSinglePostViewModel();
        }

        internal void Create(BlogPost blogPost)
        {
            try
            {
                this._unitOfWork.BlogPostSchoolRepository.Insert(blogPost);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        internal void Update(BlogPost blogPost)
        {
            try
            {
                this._unitOfWork.BlogPostSchoolRepository.Update(blogPost);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                BlogPost post = this.GetBlogPostByIdD(id);
                this._unitOfWork.BlogPostSchoolRepository.Remove(post);
                this._unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}