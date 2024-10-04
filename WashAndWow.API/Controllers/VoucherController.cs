using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.Voucher;
using WashAndWow.Application.Voucher.Create;
using WashAndWow.Application.Voucher.Delete;
using WashAndWow.Application.Voucher.Read;
using WashAndWow.Application.Voucher.Update;

namespace WashAndWow.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoucherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all voucher
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<IPagedResult<VoucherDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<IPagedResult<VoucherDto>>>> GetAllRating(
            [FromQuery] GetAllVoucherQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<IPagedResult<VoucherDto>>(result));
        }

        // Get a specific voucher by ID
        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<VoucherDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRatingById(string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetVoucherByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Rating with ID {id} not found"));
            }

            return Ok(new JsonResponse<VoucherDto>(result));
        }

        // Create a new voucher
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRating(
            [FromBody] CreateVoucherCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        // Update an existing voucher
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRating(
            [FromBody] UpdateVoucherCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Service with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("Service updated successfully"));
        }

        // Delete a voucher
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteService(string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteVoucherCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Rating with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("Rating deleted successfully"));
        }
    }
}
