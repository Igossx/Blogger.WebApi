namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = default!;
        public string UserEmail { get; set; } = default!;

        // Relacja 1:n
        public int? CreatedCommentById { get; set; }
        public User? CreatedCommentBy { get; set; } = default!;

        // Relacja 1:n
        public int PostId { get; set; }
        public Post Post { get; set; } = default!;
    }
}
