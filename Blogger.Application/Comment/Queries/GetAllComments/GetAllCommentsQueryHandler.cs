using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Comment.Queries.GetAllComments
{
    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetAllCommentsQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAll(request.EnocdedTitle);

            // Mapping: Comment to CommentDto
            var commentsDtos = comments.Select(c => new CommentDto
            {
                UserName = c.UserName,
                Message = c.Message,
                Email = c.UserEmail
            });

            return commentsDtos;
        }
    }
}
