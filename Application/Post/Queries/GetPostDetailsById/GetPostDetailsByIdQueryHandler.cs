using Blogger.Application.Comment.Queries;
using Domain.Interfaces;
using MediatR;

namespace Blogger.Application.Post.Queries.GetPostDetailsById
{
    public class GetPostDetailsByIdQueryHandler : IRequestHandler<GetPostDetailsByIdQuery, DetailsPostDto>
    {
        private readonly IPostRepository _postRepository;

        public GetPostDetailsByIdQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<DetailsPostDto> Handle(GetPostDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetDetailsById(request.Id);

            post.EncodeTitle();

            var commentsDtos = post.Comments.Select(c => new CommentDto
            {
                UserName = c.UserName,
                Message = c.Message,
                Email = c.UserEmail
            });

            var detailsPostDto = new DetailsPostDto
            {
                Title = post.Title,
                Category = post.Category,
                Content = post.Content,
                Comments = commentsDtos,
                EncodedTitle = post.EncodedTitle,
                PublicationDate = post.PublicationDate
            };

            return detailsPostDto;
        }
    }
}
