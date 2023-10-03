using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
    public class BloggerSeeder
    {
        private readonly BloggerDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEnumerable<Role> _roles;

        public BloggerSeeder(BloggerDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _roles = GetRoles();
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync() && _dbContext.Database.IsRelational())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();

                if (pendingMigrations is not null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.Posts.Any())
                {
                    _dbContext.Posts.AddRange(GetPosts());
                    await _dbContext.SaveChangesAsync();
                }
                if (!_dbContext.Roles.Any())
                {
                    _dbContext.Roles.AddRange(_roles);
                    await _dbContext.SaveChangesAsync();
                }
                if (!_dbContext.Users.Any())
                {
                    _dbContext.Add(GetAdmin());
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Post> GetPosts()
        {
            var posts = new List<Post>()
            {
                new Post()
                {
                    Title = "Post number 1",
                    Category = Domain.Enums.PostCategory.Travel,
                    Content = "Lorem ipsum 3. It is a long established fact that a reader will be " +
                    "distracted by the readable content of a page when looking at its layout. " +
                    "The point of using Lorem Ipsum is that it has a more-or-less normal distribution " +
                    "of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                    CreatedPostBy = _dbContext.Users.FirstOrDefault()!
                },
                new Post()
                {
                    Title = "Post number 2",
                    Category = Domain.Enums.PostCategory.HealthWellness,
                    Content = "Lorem ipsum 2. It is a long established fact that a reader will be " +
                    "distracted by the readable content of a page when looking at its layout. " +
                    "The point of using Lorem Ipsum is that it has a more-or-less normal distribution " +
                    "of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                    CreatedPostBy = _dbContext.Users.FirstOrDefault()!
                },
                new Post()
                {
                    Title = "Post number 3",
                    Category = Domain.Enums.PostCategory.Technology,
                    Content = "Lorem ipsum 3. It is a long established fact that a reader will be " +
                    "distracted by the readable content of a page when looking at its layout. " +
                    "The point of using Lorem Ipsum is that it has a more-or-less normal distribution " +
                    "of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                    CreatedPostBy = _dbContext.Users.FirstOrDefault()!
                }
            };

            foreach (var post in posts)
            {
                post.EncodeTitle();
            }
            return posts;
        }

        private static IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

        private User GetAdmin()
        {
            DateTime dateOfBirth = DateTime.Now.AddYears(-24);

            var admin = new User()
            {
                Email = "admin@gmail.com",
                FirstName = "Jack",
                LastName = "Strong",
                DateOfBirth = dateOfBirth,
                Role = _dbContext.Roles.FirstOrDefault(r => r.Name == "Admin")!
            };

            var passwordHash = _passwordHasher.HashPassword(admin, "AdminPassword");

            admin.PasswordHash = passwordHash;

            return admin;
        }
    }
}
