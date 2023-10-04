using MediatR;

namespace Blogger.Application.Comment.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        public int Id { get; set; } = default!;
    }
}
