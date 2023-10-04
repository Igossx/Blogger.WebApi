using AutoMapper;
using Blogger.Application.Extensions;
using Blogger.Application.Pagination;
using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Post.Queries.GetAllPosts
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PagedResult<PostDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAll(request);

            var totalItemsCount = posts.Count();

            var pagedResult = posts.GetItemsForPage(request);

            // Mapping: Post to PostDto
            var result = _mapper.Map<IEnumerable<PostDto>>(pagedResult).ToList();

            return new PagedResult<PostDto>(result, totalItemsCount, request.PageSize, request.PageNumber);
        }
    }
}
