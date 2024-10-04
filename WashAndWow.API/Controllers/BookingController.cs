using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.Booking;
using WashAndWow.Application.Booking.Create;
using WashAndWow.Application.Booking.Delete;
using WashAndWow.Application.Booking.Read;
using WashAndWow.Application.Booking.Update;

namespace WashAndWow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all bookings
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<IPagedResult<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<List<BookingDto>>>> GetAllBookings(
            [FromQuery] GetAllBookingbyShopIdQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<IPagedResult<BookingDto>>(result));
        }

        // Get booking by ID
        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<BookingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingById(string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetBookingByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Booking with ID {id} not found"));
            }

            return Ok(new JsonResponse<BookingDto>(result));
        }

        // Create a new booking
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBooking(
            [FromBody] CreateBookingCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        // Update an existing booking
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBooking(
            [FromBody] UpdateBookingCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Booking with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("Booking updated successfully"));
        }

        // Delete a booking
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBooking(string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteBookingCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Booking with ID {id} not found"));
            }

            return Ok(new JsonResponse<string>("Booking deleted successfully"));
        }
    }
}
