﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP_ASPNET_1.DbFiles.Contexts;
using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.DbFiles.Repositories;
using SP_ASPNET_1.Models;

namespace SP_ASPNET_1.DbFiles.UnitsOfWork
{
    public interface IUnitOfWork
    {
        IRepository<BlogPost> BlogPostSchoolRepository { get; }
        IRepository<Comment> CommentSchoolRepository { get; }
        IRepository<ApplicationUser> UserSchoolRepository { get; }
        IRepository<ProductLine> ProductLineSchoolRepository { get; }
        IRepository<ProductItem> ProductItemSchoolRepository { get; }
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IceCreamBlogContext _context = new IceCreamBlogContext();

        private IRepository<ApplicationUser> _userSchoolRepository;
        private IRepository<BlogPost> _blogPostSchoolRepository;
        private IRepository<Comment> _commentSchoolRepository;
        private IRepository<ProductLine> _productLineSchoolRepository;
        private IRepository<ProductItem> _productItemSchoolRepository;

        public IRepository<BlogPost> BlogPostSchoolRepository
        {
            get
            {
                if (this._blogPostSchoolRepository == null)
                {
                    this._blogPostSchoolRepository = new BlogPostRepository(this._context);
                }
                return _blogPostSchoolRepository;
            }
        }

        public IRepository<Comment> CommentSchoolRepository
        {
            get
            {
                if (this._commentSchoolRepository == null)
                {
                    this._commentSchoolRepository = new CommentRepository(this._context);
                }
                return _commentSchoolRepository;
            }
        }

        public IRepository<ApplicationUser> UserSchoolRepository
        {
            get
            {
                if (this._userSchoolRepository == null)
                {
                    this._userSchoolRepository = new BaseRepository<ApplicationUser>(this._context);
                }
                return _userSchoolRepository;
            }
        }

        public IRepository<ProductLine> ProductLineSchoolRepository
        {
            get
            {
                if (this._productLineSchoolRepository == null)
                {
                    this._productLineSchoolRepository = new BaseRepository<ProductLine>(this._context);
                }
                return _productLineSchoolRepository;
            }
        }

        public IRepository<ProductItem> ProductItemSchoolRepository
        {
            get
            {
                if (this._productItemSchoolRepository == null)
                {
                    this._productItemSchoolRepository = new BaseRepository<ProductItem>(this._context);
                }
                return _productItemSchoolRepository;
            }
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}