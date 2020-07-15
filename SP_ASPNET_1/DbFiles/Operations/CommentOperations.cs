using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.DbFiles.Operations
{
    public class CommentOperations
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly List<Comment> _comments;

        public CommentOperations()
        {
            _comments = _unitOfWork.CommentSchoolRepository.Get(null, b => b.OrderByDescending(d => d.DateTime), "").ToList();
        }

        public Comment GetCommentByIdD(int id)
        {
            return _comments.SingleOrDefault(x => x.CommentID == id);
        }

        internal void Create(Comment comment)
        {
            try
            {
                this._unitOfWork.CommentSchoolRepository.Insert(comment);
                this._unitOfWork.Save();
                comment.User = _unitOfWork.UserSchoolRepository.GetByID(comment.UserID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        internal void Update(Comment comment)
        {
            try
            {
                this._unitOfWork.CommentSchoolRepository.Update(comment);
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
                Comment comment = this.GetCommentByIdD(id);
                this._unitOfWork.CommentSchoolRepository.Remove(comment);
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