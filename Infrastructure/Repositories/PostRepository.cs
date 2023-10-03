using Blogger.Application.Pagination.Enums;
using Blogger.Application.Pagination.QueryParameters;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BloggerDbContext _dbContext;

        public PostRepository(BloggerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();

        }

        public async Task Delete(Post post)
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAll(QueryModel query)
        {
            IQueryable<Post> postsQuery = _dbContext.Posts;

            if (!string.IsNullOrEmpty(query.SearchPhrase))
            {
                postsQuery = postsQuery.Where(p => p.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                || p.Content.ToLower().Contains(query.SearchPhrase.ToLower()));
            }

            postsQuery = query.SortDirection == SortDirection.Ascending
                    ? postsQuery.OrderBy(p => p.Title)
                    : postsQuery.OrderByDescending(p => p.Title);

            if (!await postsQuery.AnyAsync())
            {
                throw new NotFoundException("No posts matching search request");
            }

            return await postsQuery.AsNoTracking().ToListAsync();
        }

        public async Task<Post> GetByEncodedTitle(string encodedTitle)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(p => p.EncodedTitle == encodedTitle) ??
                throw new NotFoundException("Post not found.");
        }

        public async Task<Post> GetById(int id)
        {
            return await _dbContext.Posts.FindAsync(id) ??
                throw new NotFoundException("Post not found.");
        }

        public async Task Update(Post post)
        {
            //_dbContext.Entry(post).State = EntityState.Modified;
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsTitleUnique(string encodedTitle)
        {
            return !(_dbContext.Posts.Any(p => p.EncodedTitle == encodedTitle));
        }

        public async Task<Post> GetDetailsById(int id)
        {
            return await _dbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id) ??
                throw new NotFoundException("Post not found.");
        }
    }
}
