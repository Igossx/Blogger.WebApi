namespace Blogger.Application.Comment.Commands
{
    public class CreateCommentDto
    {
        public string UserName { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
