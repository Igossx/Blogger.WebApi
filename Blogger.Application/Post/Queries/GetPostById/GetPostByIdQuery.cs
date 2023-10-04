using MediatR;

namespace Blogger.Application.Post.Queries.GetPostById
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public int Id { get; set; } = default!;
    }
}
