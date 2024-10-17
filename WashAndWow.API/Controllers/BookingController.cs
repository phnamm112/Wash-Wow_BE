using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Application.Common.Pagination;
using WashAndWow.Application.Booking;
using WashAndWow.Application.Booking.Create;
using WashAndWow.Application.Booking.Delete;
using WashAndWow.Application.Booking.GetAllByShopID;
using WashAndWow.Application.Booking.GetByID;
using WashAndWow.Application.Booking.Update;
using WashAndWow.Application.Booking.UpdateStatus;

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

        /// <summary>
        /// Retrieve all bookings by laundry shop ID with pagination
        /// </summary>
        /// <param name="shopID">Laundry shop ID</param>
        /// <param name="query">Request body</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("bookings/{shopID}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<BookingDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<BookingDto>>>> GetAllByShopID(
            [FromRoute] string shopID,
            [FromQuery] GetAllBookingbyShopIdQuery query,
            CancellationToken cancellationToken = default)
        {
            query.ShopId = shopID;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<PagedResult<BookingDto>>(result));
        }

        /// <summary>
        /// Retrieve single booking by booking ID
        /// </summary>
        /// <param name="id">Booking ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<BookingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDto>> GetBookingById([FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetBookingByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Customer create new booking
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> CreateBooking(
            [FromBody] CreateBookingCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        // Update an existing booking
        [HttpPut]
        [Route("bookings/booking/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBooking([FromRoute] string id,
            [FromBody] UpdateBookingCommand command,
            CancellationToken cancellationToken = default)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }
        /// <summary>
        /// Update status of a booking
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("bookings/{id}/status")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBookingStatus([FromRoute] string id,
           [FromBody] UpdateBookingStatusCommand command,
           CancellationToken cancellationToken = default)
        {
            command.BookingId = id;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
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
