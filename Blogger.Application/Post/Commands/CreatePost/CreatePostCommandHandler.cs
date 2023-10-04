using Blogger.Application.ApplicationUser;
using Blogger.Application.Post.Commands.CreateUser;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Post.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IUserContext _userContext;
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IUserContext userContext, IPostRepository postRepository)
        {
            _userContext = userContext;
            _postRepository = postRepository;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser() ??
                throw new BadRequestException("You need to log in.");

            // Mapping: CreatePostDto to Post
            var post = new Domain.Entities.Post
            {
                Title = request.Title,
                Category = request.Category,
                Content = request.Content,
                CreatedPostById = currentUser.Id
            };

            post.EncodeTitle();

            await _postRepository.Create(post);

            return post.Id;
        }
    }
}
