using MediatR;

namespace Blogger.Application.Comment.Queries.GetAllComments
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<CommentDto>>
    {
        public string EnocdedTitle { get; set; } = default!;
    }
}
