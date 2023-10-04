using MediatR;

namespace Blogger.Application.Comment.Commands.UpdateComment
{
    public class UpdateCommentCommand : CreateCommentDto, IRequest
    {
        public int Id { get; set; } = default!;
    }
}
