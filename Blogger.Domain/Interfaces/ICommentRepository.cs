using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAll(string encodedTitle);
        Task<IEnumerable<Comment>> GetAllByDate(string encodedTitle, string sortOrder);
        Task<Comment> GetById(int id);
        Task Create(Comment comment, string encodedTitle);
        Task Update(Comment comment);
        Task Delete(Comment comment);
    }
}
