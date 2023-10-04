namespace Blogger.Application.Account.Queries
{
    public class UserDto
    {
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string RoleName { get; set; } = default!;
    }
}
