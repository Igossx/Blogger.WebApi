using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BloggerDbContext _dbContext;

        public AccountRepository(BloggerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.Include(u => u.Role).AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email) ??
                    throw new NotFoundException("User not found.");
        }

        public bool IsEmailUnique(string email)
        {
            return !(_dbContext.Users.Any(u => u.Email == email));
        }

        public async Task Register(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
