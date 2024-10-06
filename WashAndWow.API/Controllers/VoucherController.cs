using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Application.Common.Pagination;
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

        /// <summary>
        /// Retrieve all voucher by pagination
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<VoucherDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<VoucherDto>>>> GetAllVoucher(
            [FromQuery] GetAllVoucherQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<PagedResult<VoucherDto>>(result));
        }

        /// <summary>
        /// Retrieve single voucher by voucher's ID
        /// </summary>
        /// <param name="id">ID of voucher</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(VoucherDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VoucherDto>> GetVoucherByID([FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetVoucherByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Method allow for Admin and ShopOwner to create an voucher
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> CreateVoucher(
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
