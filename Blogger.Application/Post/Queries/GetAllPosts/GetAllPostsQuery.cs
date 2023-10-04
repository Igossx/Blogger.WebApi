using Blogger.Application.Pagination;
using Blogger.Application.Pagination.QueryParameters;
using MediatR;

namespace Blogger.Application.Post.Queries.GetAllPosts
{
    public class GetAllPostsQuery : QueryModel, IRequest<PagedResult<PostDto>>
    {
    }
}
