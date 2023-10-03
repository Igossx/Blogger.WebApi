using MediatR;

namespace Blogger.Application.Comment.Queries.GetAllCommentsByDate
{
    public class GetAllCommentsByDateQuery : IRequest<IEnumerable<CommentDto>>
    {
        public string EncodedTitle{ get; set; } = default!;
        public string SortOrder { get; set; } = default!;
    }
}
