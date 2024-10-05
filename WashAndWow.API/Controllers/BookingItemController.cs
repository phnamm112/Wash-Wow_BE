using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.BookingItem;
using WashAndWow.Application.BookingItem.Create;
using WashAndWow.Application.BookingItem.Delete;
using WashAndWow.Application.BookingItem.Read;
using WashAndWow.Application.BookingItem.Update;

namespace WashAndWow.API.Controllers
{
    [ApiController]
    [Route("bookings/{bookingId}/[controller]")]
    public class BookingItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all booking items for a specific booking
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<List<BookingItemDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<IPagedResult<BookingItemDto>>>> GetBookingItems(
            [FromQuery] GetAllBookingItemByBookingIdQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<IPagedResult<BookingItemDto>>(result));
        }

        // Get a booking item by ID
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<BookingItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingItemById(string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetBookingItemByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"BookingItem with ID {id} not found"));
            }

            return Ok(new JsonResponse<BookingItemDto>(result));
        }

        // Create a new booking item
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBookingItem(
            string bookingId, [FromBody] CreateBookingItemCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        // Update an existing booking item
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBookingItem(
            [FromBody] UpdateBookingItemCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"BookingItem with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("BookingItem updated successfully"));
        }

        // Delete a booking item
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBookingItem(string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteBookingItemCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"BookingItem with ID {id} not found"));
            }

            return Ok(new JsonResponse<string>("BookingItem deleted successfully"));
        }
    }
}
