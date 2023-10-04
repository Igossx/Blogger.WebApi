using MediatR;

namespace Blogger.Application.Post.Commands.CreateUser
{
    public class CreatePostCommand : CreatePostDto, IRequest<int>
    {
    }
}
