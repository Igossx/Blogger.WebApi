using MediatR;

namespace Blogger.Application.Post.Queries.GetPostDetailsById
{
    public class GetPostDetailsByIdQuery : IRequest<DetailsPostDto>
    {
        public int Id { get; set; } = default!;
    }
}
