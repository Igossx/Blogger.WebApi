using Domain.Enums;

namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public PostCategory Category { get; set; }
        public string Content { get; set; } = default!;
        public int Likes { get; set; } = default;
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        public string EncodedTitle { get; set; } = default!;

        // Relacja 1:n
        public int? CreatedPostById { get; set; }
        public User? CreatedPostBy { get; set; } = default!;

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public void EncodeTitle() =>
            EncodedTitle = Title.ToLower().Replace(" ", "-");


    }
}
