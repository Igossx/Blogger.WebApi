using Blogger.Application.Comment.Queries;
using Domain.Enums;

namespace Blogger.Application.Post.Queries
{
    public class DetailsPostDto
    {
        public string Title { get; set; } = default!;
        public DateTime PublicationDate { get; set; }
        public PostCategory Category { get; set; }
        public string EncodedTitle { get; set; } = default!;
        public string Content { get; set; } = default!;
        public IEnumerable<CommentDto> Comments { get; set; } = default!;
    }
}
