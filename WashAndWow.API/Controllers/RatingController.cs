using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.Rating;
using WashAndWow.Application.Rating.Create;
using WashAndWow.Application.Rating.Delete;
using WashAndWow.Application.Rating.Read;
using WashAndWow.Application.Rating.Update;

namespace WashAndWow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RatingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get all rating for all shop
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<IPagedResult<RatingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<IPagedResult<RatingDto>>>> GetAllRating(
            [FromQuery] GetAllRatingQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<IPagedResult<RatingDto>>(result));
        }
        /// <summary>
        /// Get rating of a shop by it id
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<RatingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRatingById(string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetRatingByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Rating with ID {id} not found"));
            }

            return Ok(new JsonResponse<RatingDto>(result));
        }

        /// <summary>
        /// Create a new rating with noti
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<RatingDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRating(
            [FromBody] CreateRatingCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<RatingDto>(result));
        }

        // Update an existing rating
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRating(
            [FromBody] UpdateRatingCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Service with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("Service updated successfully"));
        }

        // Delete a rating
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteService(string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteRatingCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Rating with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("Rating deleted successfully"));
        }
    }
}
