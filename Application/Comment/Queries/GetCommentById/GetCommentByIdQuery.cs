using MediatR;

namespace Blogger.Application.Comment.Queries.GetCommentById
{
    public class GetCommentByIdQuery : IRequest<CommentDto>
    {
        public int Id { get; set; } = default!;
    }
}
