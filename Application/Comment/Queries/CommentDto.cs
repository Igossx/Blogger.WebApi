namespace Blogger.Application.Comment.Queries
{
    public class CommentDto
    {
        public string UserName { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
