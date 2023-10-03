using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Post.Queries.GetPostById
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetById(request.Id);

            // Mapping: Post to PostDto
            return _mapper.Map<PostDto>(post);
        }
    }
}
