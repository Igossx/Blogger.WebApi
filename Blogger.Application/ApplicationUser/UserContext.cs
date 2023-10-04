using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blogger.Application.ApplicationUser
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser? GetCurrentUser()
        {
            var user = (_httpContextAccessor?.HttpContext?.User) ??
                throw new InvalidOperationException("Content user is not present");

            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var id = int.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;

            return new CurrentUser(id, email);
        }
    }
}
