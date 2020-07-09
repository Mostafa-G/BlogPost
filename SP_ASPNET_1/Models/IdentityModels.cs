using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace SP_ASPNET_1.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.LikedBlogPosts = new HashSet<BlogPost>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Likes { get { return BlogPosts.Sum(x => x.Likes); } }
        public override string ToString()
        {
            return $"{Name} {Surname} ({Likes}♥)";
        }

        
        public virtual ICollection<BlogPost> LikedBlogPosts { get; set; }
        [ForeignKey("UserID")]
        public ICollection<BlogPost> BlogPosts { get; set; }
        [ForeignKey("UserID")]
        public ICollection<Comment> Comments { get; set; }
    }
}