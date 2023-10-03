namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<User> Users { get; set; } = new List<User>();
    }
}
