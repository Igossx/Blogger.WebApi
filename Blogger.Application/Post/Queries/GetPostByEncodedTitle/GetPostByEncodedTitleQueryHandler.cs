using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Post.Queries.GetPostByEncodedTitle
{
    public class GetPostByEncodedTitleQueryHandler : IRequestHandler<GetPostByEncodedTitleQuery, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostByEncodedTitleQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetPostByEncodedTitleQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByEncodedTitle(request.EncodedTitle);

            // Mapping: Post to PostDto
            return _mapper.Map<PostDto>(post);
        }
    }
}
