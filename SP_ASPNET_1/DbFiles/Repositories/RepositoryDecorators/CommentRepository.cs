using SP_ASPNET_1.BusinessLogic;
using SP_ASPNET_1.DbFiles.Contexts;
using SP_ASPNET_1.DbFiles.Repositories;
using SP_ASPNET_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(IceCreamBlogContext context) : base(context)
        {

        }

        public new Comment GetByID(object ID)
        {
            return (new Comment[] { this.GetByID(ID) }) .FirstOrDefault();
        }

        public new Task<IEnumerable<Comment>> GetAsync(Expression<Func<Comment, bool>> filter = null, Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null, string includeProperties = "")
        {
            return new Task<IEnumerable<Comment>>(() =>
            {
                return this.GetAsync(filter, orderBy, includeProperties).Result;
            });
        }

        public new IEnumerable<Comment> Get(Expression<Func<Comment, bool>> filter = null, Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null, string includeProperties = "")
        {
            return base.Get(filter, orderBy, includeProperties);
        }
    }
}