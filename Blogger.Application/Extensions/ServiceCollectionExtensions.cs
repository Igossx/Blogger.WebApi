using Microsoft.Extensions.DependencyInjection;
using Application.Mappings;
using Application.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Blogger.Application.ApplicationUser;
using MediatR;
using Blogger.Application.Account.Queries.GetAllUsers;
using Blogger.Application.Account.Commands.LoginUser;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(PostMappingProfile));

            services.AddMediatR(typeof(GetAllUsersQuery));

            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddValidatorsFromAssemblyContaining(typeof(LoginUserCommandValidator))
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            var authenticationSettings = new AuthenticationSettings();

            configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });

            services.AddSingleton(authenticationSettings);

            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
