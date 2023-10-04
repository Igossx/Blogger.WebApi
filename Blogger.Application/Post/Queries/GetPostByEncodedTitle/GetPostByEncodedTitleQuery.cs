using MediatR;

namespace Blogger.Application.Post.Queries.GetPostByEncodedTitle
{
    public class GetPostByEncodedTitleQuery : IRequest<PostDto>
    {
        public string EncodedTitle { get; set; } = default!;
    }
}
