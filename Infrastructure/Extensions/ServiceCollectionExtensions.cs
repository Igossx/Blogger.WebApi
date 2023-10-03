using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BloggerDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("BloggerDb")));

            services.AddScoped<BloggerSeeder>();

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
