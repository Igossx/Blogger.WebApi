using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BloggerDbContext _dbContext;

        public CommentRepository(BloggerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Comment comment, string encodedTitle)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.EncodedTitle == encodedTitle) ??
                throw new NotFoundException("Post not found.");

            comment.Post = post;
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAll(string encodedTitle)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.EncodedTitle == encodedTitle) ??
               throw new NotFoundException("Post not found.");

            var comments = _dbContext.Comments.Where(c => c.Post.EncodedTitle == encodedTitle);

            return await comments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByDate(string encodedTitle, string sortOrder)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.EncodedTitle == encodedTitle) ??
               throw new NotFoundException("Post not found.");

            var comments = _dbContext.Comments.Where(c => c.Post.EncodedTitle == encodedTitle);

            switch (sortOrder.ToLower())
            {
                case "asc":
                    comments = comments.OrderBy(c => c.PublicationDate);
                    break;
                case "desc":
                    comments = comments.OrderByDescending(p => p.PublicationDate);
                    break;
                default:
                    throw new BadRequestException("Incorrect query. Expected 'asc' or 'desc'.");
            }
            return await comments.AsNoTracking().ToListAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id) ??
                throw new NotFoundException("Comment not found");
        }

        public async Task Update(Comment comment)
        {
            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
