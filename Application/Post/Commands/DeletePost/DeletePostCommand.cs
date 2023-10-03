using MediatR;

namespace Blogger.Application.Post.Commands.DeletePost
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; } = default!;
    }
}
