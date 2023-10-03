using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Comment.Queries.GetCommentById
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentByIdQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetById(request.Id);

            // Mapping: Comment to CommentDto
            var commentDto = new CommentDto
            {
                UserName = comment.UserName,
                Message = comment.Message,
                Email = comment.UserEmail
            };

            return commentDto;
        }
    }
}
