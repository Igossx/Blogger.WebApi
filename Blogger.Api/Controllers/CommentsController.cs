using Blogger.Application.Comment.Commands;
using Blogger.Application.Comment.Commands.CreateComment;
using Blogger.Application.Comment.Commands.DeleteComment;
using Blogger.Application.Comment.Commands.UpdateComment;
using Blogger.Application.Comment.Queries;
using Blogger.Application.Comment.Queries.GetAllComments;
using Blogger.Application.Comment.Queries.GetAllCommentsByDate;
using Blogger.Application.Comment.Queries.GetCommentById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blogger.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{encodeTitle}/getAllByEncodeTitle")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CommentDto>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Receives all comments by post encodeTitle")]
        public async Task<IActionResult> GetAll([FromRoute] string encodeTitle)
        {
            var comments = await _mediator.Send(new GetAllCommentsQuery() { EnocdedTitle = encodeTitle });

            return Ok(comments);

        }

        [HttpGet("{encodeTitle}/getAllByDate")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CommentDto>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Receives all comments by post encodeTitle sorted (asc/desc) by date")]
        public async Task<IActionResult> GetAllByDate([FromRoute] string encodeTitle, [FromQuery] string sortOrder)
        {
            var comments = await _mediator.Send(new GetAllCommentsByDateQuery()
            {
                EncodedTitle = encodeTitle,
                SortOrder = sortOrder
            });

            return Ok(comments);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CommentDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Retrieve a specific comment by id")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _mediator.Send(new GetCommentByIdQuery() { Id = id });

            return Ok(comment);
        }

        [HttpPost("{encodeTitle}")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Crete a new comment for a specific post")]
        public async Task<IActionResult> CreateForPost([FromBody] CreateCommentDto commentDto, [FromRoute] string encodeTitle)
        {
            var command = new CreateCommentCommand
            {
                UserName = commentDto.UserName,
                Message = commentDto.Message,
                EncodedTitle = encodeTitle
            };

            int id = await _mediator.Send(command);

            return Created($"api/comments/{id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 403)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Delete a specific comment")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteCommentCommand() { Id = id });

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        [SwaggerOperation(Summary = "Update a existing comment")]
        public async Task<IActionResult> Update([FromBody] CreateCommentDto commentDto, [FromRoute] int id)
        {
            var command = new UpdateCommentCommand
            {
                UserName = commentDto.UserName,
                Message = commentDto.Message,
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
