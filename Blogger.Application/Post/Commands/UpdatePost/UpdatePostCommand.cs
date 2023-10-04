using MediatR;

namespace Blogger.Application.Post.Commands.UpdatePost
{
    public class UpdatePostCommand : UpdatePostDto, IRequest
    {
        public int Id { get; set; } = default!;
    }
}
