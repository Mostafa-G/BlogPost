namespace SP_ASPNET_1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SP_ASPNET_1.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.Owin;
    using Owin;

    internal sealed class Configuration : DbMigrationsConfiguration<SP_ASPNET_1.DbFiles.Contexts.IceCreamBlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SP_ASPNET_1.DbFiles.Contexts.IceCreamBlogContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.ProductLines.AddOrUpdate(x => x.Title, new ProductLine { 
                Title = "All Time Classic", 
                Description = "One morning, when Gregor Samsa woke from troubled dreams, he found himself transformed in his bed into a horrible vermin. He lay.",
                ProductItems = new List<ProductItem>()
                {
                    new ProductItem()
                    {
                        Title = "kiwi",
                        ImageAlt = "kiwi icecream",
                        ImageURL = "kiwi.jpg",
                        
                    },
                    new ProductItem()
                    {
                        Title = "mango",
                        ImageAlt = "mango icecream",
                        ImageURL = "mango.jpg",

                    },
                    new ProductItem()
                    {
                        Title = "cantaloupe",
                        ImageAlt = "cantaloupe icecream",
                        ImageURL = "cantaloupe.jpg",

                    }
                }
            });
            context.ProductLines.AddOrUpdate(x => x.Title, new ProductLine
            {
                Title = "Berry Special",
                Description = "On his armour-like back, and if he lifted his head a little he could see his brown belly, slightly domed and divided by arches.",
                ProductItems = new List<ProductItem>()
                {
                    new ProductItem()
                    {
                        Title = "blackberry",
                        ImageAlt = "blackberry icecream",
                        ImageURL = "blackberry.jpg",

                    },
                    new ProductItem()
                    {
                        Title = "strawberry",
                        ImageAlt = "strawberry icecream",
                        ImageURL = "strawberry.jpg",

                    },
                    new ProductItem()
                    {
                        Title = "blueberry",
                        ImageAlt = "blueberry icecream",
                        ImageURL = "blueberry.jpg",

                    }
                }
            });
            context.ProductLines.AddOrUpdate(x => x.Title, new ProductLine
            {
                Title = "Fruit Blast",
                Description = "Into stiff sections. The bedding was hardly able to cover it and seemed ready to slide off any moment. His many legs, pitifully.",
                ProductItems = new List<ProductItem>()
                {
                    new ProductItem()
                    {
                        Title = "grape",
                        ImageAlt = "grape icecream",
                        ImageURL = "grapes.jpg",

                    },
                    new ProductItem()
                    {
                        Title = "apple",
                        ImageAlt = "apple icecream",
                        ImageURL = "green-apple.jpg",

                    },
                    new ProductItem()
                    {
                        Title = "pineapple",
                        ImageAlt = "pineapple icecream",
                        ImageURL = "pineapple.jpg",

                    }
                }
            });

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole() { Name = "Admin" });
            }

            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole() { Name = "User" });
            }

            if (!roleManager.RoleExists("Author"))
            {
                roleManager.Create(new IdentityRole() { Name = "Author" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user1 = new ApplicationUser() { UserName = "Admin@a.com", Email = "Admin@a.com", Name = "Alejandra", Surname = "Biagi" };
            var check = userManager.Create(user1, "aA@123");
            if (check.Succeeded)
            {
                userManager.AddToRole(user1.Id, "Admin");
            }

            var user2 = new ApplicationUser() { UserName = "User@a.com", Email = "User@a.com", Name = "Biagi", Surname = "Alejandra" };
            check = userManager.Create(user2, "aA@123");
            if (check.Succeeded)
            {
                userManager.AddToRole(user2.Id, "User");
            }

            var user3 = new ApplicationUser() { UserName = "Author@a.com", Email = "Author@a.com", Name = "Yezafovich", Surname = "Alejandra" };
            check = userManager.Create(user3, "aA@123");
            if (check.Succeeded)
            {
                userManager.AddToRole(user3.Id, "Author");
            }

            context.SaveChanges();

            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user1.Id, DateTime = DateTime.Now.AddSeconds(-56461870), ImageUrl = "post-2.jpg", Content = "18aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.18 18" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user2.Id, DateTime = DateTime.Now.AddSeconds(-35264963), ImageUrl = "post-1.jpg", Content = "26aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.26 26" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user3.Id, DateTime = DateTime.Now.AddSeconds(-14096753), ImageUrl = "post-2.jpg", Content = "39aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.39 39" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user1.Id, DateTime = DateTime.Now.AddSeconds(-5275829), ImageUrl = "post-2.jpg", Content = "19aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.19 19" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user2.Id, DateTime = DateTime.Now.AddSeconds(-17646789), ImageUrl = "post-2.jpg", Content = "29aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.29 29" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user3.Id, DateTime = DateTime.Now.AddSeconds(-38240999), ImageUrl = "post-1.jpg", Content = "28aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.28 28" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user1.Id, DateTime = DateTime.Now.AddSeconds(-39669202), ImageUrl = "post-1.jpg", Content = "29aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.29 29" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user2.Id, DateTime = DateTime.Now.AddSeconds(-8937357), ImageUrl = "post-3.jpg", Content = "19aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.19 19" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user2.Id, DateTime = DateTime.Now.AddSeconds(-35701712), ImageUrl = "post-3.jpg", Content = "34aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.34 34" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user3.Id, DateTime = DateTime.Now.AddSeconds(-46595340), ImageUrl = "post-3.jpg", Content = "29aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.29 29" });
            context.BlogPosts.AddOrUpdate(new BlogPost() { AuthorID = user3.Id, DateTime = DateTime.Now.AddSeconds(-1767433), ImageUrl = "post-1.jpg", Content = "17aTakes much more effort than doing your own business at home, and on top of that there's the curse of travelling, worries about making train connections, bad and irregular food, contact with different people all the time so that you can never get to know anyone or become friendly. \n\n With them. It can all go to Hell!\" He felt a slight itch up on his belly; pushed himself slowly up on his back towards the headboard so that he could lift his head better; found where the itch was, and saw that it was covered with lots of little white spots which he didn't know what to make of; and when he.17 17" });

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
