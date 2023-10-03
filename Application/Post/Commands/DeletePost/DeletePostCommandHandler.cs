using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Post.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var postToDelete = await _postRepository.GetById(request.Id);

            await _postRepository.Delete(postToDelete);

            return Unit.Value;
        }
    }
}
