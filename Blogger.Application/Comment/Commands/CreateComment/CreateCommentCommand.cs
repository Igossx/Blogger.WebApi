using MediatR;

namespace Blogger.Application.Comment.Commands.CreateComment
{
    public class CreateCommentCommand : CreateCommentDto, IRequest<int>
    {
        public string EncodedTitle { get; set; } = default!;
    }
}
