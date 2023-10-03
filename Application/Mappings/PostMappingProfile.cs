using AutoMapper;
using Blogger.Application.Post.Queries;
using Domain.Entities;

namespace Application.Mappings
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}
