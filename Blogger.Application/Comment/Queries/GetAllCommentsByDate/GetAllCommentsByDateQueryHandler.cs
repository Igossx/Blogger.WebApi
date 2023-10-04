using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Comment.Queries.GetAllCommentsByDate
{
    public class GetAllCommentsByDateQueryHandler : IRequestHandler<GetAllCommentsByDateQuery, IEnumerable<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetAllCommentsByDateQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsByDateQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAllByDate(request.EncodedTitle, request.SortOrder);

            // Mapping: Comment to CommentDto
            var commentsDto = comments.Select(c => new CommentDto
            {
                UserName = c.UserName,
                Message = c.Message,
                Email = c.UserEmail
            });

            return commentsDto;
        }
    }
}
