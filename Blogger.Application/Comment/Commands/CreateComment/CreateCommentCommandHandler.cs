using Blogger.Application.ApplicationUser;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Comment.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IUserContext _userContext;
        private readonly ICommentRepository _commentRepository;

        public CreateCommentCommandHandler(IUserContext userContext, ICommentRepository commentRepository)
        {
            _userContext = userContext;
            _commentRepository = commentRepository;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser() ??
                throw new BadRequestException("You need to log in.");

            // Mapping: CommentDto to Comment
            var comment = new Domain.Entities.Comment
            {
                UserName = request.UserName,
                Message = request.Message,
                UserEmail = currentUser.Email,
                CreatedCommentById = currentUser.Id
            };

            await _commentRepository.Create(comment, request.EncodedTitle);

            return comment.Id;
        }
    }
}
