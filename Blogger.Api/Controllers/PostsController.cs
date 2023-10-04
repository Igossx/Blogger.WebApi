using Blogger.Application.Pagination;
using Blogger.Application.Post.Commands;
using Blogger.Application.Post.Commands.CreateUser;
using Blogger.Application.Post.Commands.DeletePost;
using Blogger.Application.Post.Commands.UpdatePost;
using Blogger.Application.Post.Queries;
using Blogger.Application.Post.Queries.GetAllPosts;
using Blogger.Application.Post.Queries.GetPostByEncodedTitle;
using Blogger.Application.Post.Queries.GetPostDetailsById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blogger.Controllers
{
    public class PostsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResult<PostDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Retrieves all posts")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPostsQuery query)
        {
            var posts = await _mediator.Send(query);

            return Ok(posts);
        }

        [HttpGet("{encodedTitle}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PostDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Retrieve a specific post by encodedTitle")]
        public async Task<IActionResult> GetByEncodedTitle([FromRoute] string encodedTitle)
        {
            var post = await _mediator.Send(new GetPostByEncodedTitleQuery() { EncodedTitle = encodedTitle });

            return Ok(post);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
        [SwaggerOperation(Summary = "Crete a new post")]
        public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
        {
            var id = await _mediator.Send(command);

            return Created($"api/posts/{id}", null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Update a existing post")]

        public async Task<IActionResult> Update([FromBody] UpdatePostDto dto, [FromRoute] int id)
        {
            var command = new UpdatePostCommand
            {
                Id = id,
                Title = dto.Title,
                Content = dto.Content
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 403)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Delete a specific post")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeletePostCommand() { Id = id });

            return NoContent();
        }

        [HttpGet("{id}/details")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DetailsPostDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Retrieve a specific post details by id")]
        public async Task<IActionResult> GetDetailsById([FromRoute] int id)
        {
            var post = await _mediator.Send(new GetPostDetailsByIdQuery() { Id = id });

            return Ok(post);
        }
    }
}
