using Domain.Enums;

namespace Blogger.Application.Post.Queries
{
    public class PostDto
    {
        public string Title { get; set; } = default!;
        public PostCategory Category { get; set; }
        public string Content { get; set; } = default!;
        public string EncodedTitle { get; set; } = default!;
        public int Likes { get; set; }
    }
}
