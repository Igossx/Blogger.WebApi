namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string PasswordHash { get; set; } = default!;

        // Relacja 1:n
        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;

        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
