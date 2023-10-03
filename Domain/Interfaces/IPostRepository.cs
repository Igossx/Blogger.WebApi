using Blogger.Application.Pagination.QueryParameters;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll(QueryModel query);
        Task<Post> GetById(int id);
        Task<Post> GetDetailsById(int id);
        Task<Post> GetByEncodedTitle(string title);
        Task Create(Post post);
        Task Update(Post post);
        Task Delete(Post post);
        bool IsTitleUnique(string encodedTitle);
    }
}
