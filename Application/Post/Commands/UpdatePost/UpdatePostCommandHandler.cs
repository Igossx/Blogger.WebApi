using Domain.Interfaces;
using MediatR;
namespace Blogger.Application.Post.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IPostRepository _postRepository;

        public UpdatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var existingPost = await _postRepository.GetById(request.Id);

            // Mapping: UpdatePostDto to Post
            existingPost.Title = request.Title;
            existingPost.Content = request.Content;

            existingPost.EncodeTitle();

            await _postRepository.Update(existingPost);

            return Unit.Value;
        }
    }
}
