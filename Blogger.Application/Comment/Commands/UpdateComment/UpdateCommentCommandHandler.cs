using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Comment.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var existingComment = await _commentRepository.GetById(request.Id);

            // Mapping: CommentDto to Comment
            existingComment.UserName = request.UserName;
            existingComment.Message = request.Message;

            await _commentRepository.Update(existingComment);

            return Unit.Value;
        }
    }
}
