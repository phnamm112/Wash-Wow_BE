using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.ShopService;
using WashAndWow.Application.ShopService.Create;
using WashAndWow.Application.ShopService.Delete;
using WashAndWow.Application.ShopService.Read;
using WashAndWow.Application.ShopService.Update;

namespace WashAndWow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all services
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<IPagedResult<ShopServiceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<IPagedResult<ShopServiceDto>>>> GetAllServices(
            [FromQuery] GetAllShopServiceQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<IPagedResult<ShopServiceDto>>(result));
        }

        // Get a specific service by ID
        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<ShopServiceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServiceById(string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetShopServiceByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"LaundryShop with ID {id} not found"));
            }

            return Ok(new JsonResponse<ShopServiceDto>(result));
        }

        // Create a new service
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateService(
            [FromBody] CreateRatingCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        // Update an existing service
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateService(
            [FromBody] UpdateShopServiceCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Service with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("Service updated successfully"));
        }

        // Delete a service
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteService(string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteShopServiceCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Service with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("Service deleted successfully"));
        }
    }
}
