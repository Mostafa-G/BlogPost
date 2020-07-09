﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using SP_ASPNET_1.Models;

namespace SP_ASPNET_1.DbFiles.Contexts
{
    public class IceCreamBlogContext: IdentityDbContext<ApplicationUser>
    {
        public IceCreamBlogContext() : base("name=IceCreamBlogDB")
        {

        }

        public static IceCreamBlogContext Create()
        {
            return new IceCreamBlogContext();
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ProductLine> ProductLines { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<BlogPost>()
                .HasMany(s => s.Likers)
                .WithMany(c => c.LikedBlogPosts)
                .Map(cs =>
                {
                    cs.MapRightKey("UserId");
                    cs.MapLeftKey("BlogPostID");
                    cs.ToTable("PostsLikes");
                });
        }
    }

}