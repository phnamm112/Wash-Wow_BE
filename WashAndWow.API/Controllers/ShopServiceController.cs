using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Application.Common.Pagination;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.ShopService;
using WashAndWow.Application.ShopService.Create;
using WashAndWow.Application.ShopService.Delete;
using WashAndWow.Application.ShopService.GetByFilter;
using WashAndWow.Application.ShopService.GetByOwnerId;
using WashAndWow.Application.ShopService.Read;
using WashAndWow.Application.ShopService.Update;

namespace WashAndWow.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve all services by laundry shop ID with pagination
        /// </summary>
        /// <param name="shopID">Laundry shop ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{shopID}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<IPagedResult<ShopServiceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<ShopServiceDto>>>> GetAllByShopID(
            [FromRoute] string shopID,
            [FromQuery] int pageNo,
            [FromQuery] int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllShopServiceQuery(pageNo, pageSize);
            query.ShopId = shopID;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<IPagedResult<ShopServiceDto>>(result));
        }

        /// <summary>
        /// Retrieve single service by service ID
        /// </summary>
        /// <param name="id">Service ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<ShopServiceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JsonResponse<ShopServiceDto>>> GetServiceById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetShopServiceByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Service with ID {id} not found"));
            }

            return Ok(new JsonResponse<ShopServiceDto>(result));
        }
        /// <summary>
        /// Retrieve all service by Owner ID
        /// </summary>
        /// <param name="ownerId">Service ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{ownerId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<ShopServiceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JsonResponse<PagedResult<ShopServiceDto>>>> GetServiceByOwnerId(
            [FromRoute] string ownerId,
            CancellationToken cancellationToken = default)
        {
            var query = new GetShopServiceByOwnerIdQuery();
            query.OwnerID = ownerId;
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Service for owner {ownerId} not found"));
            }

            return Ok(new JsonResponse<PagedResult<ShopServiceDto>>(result));
        }

        /// <summary>
        /// Shop owner creates a new service
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> CreateService(
            [FromBody] CreateShopServiceCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        /// <summary>
        /// Update an existing service
        /// </summary>
        /// <param name="id">Service ID</param>
        /// <param name="command">Update service command</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/service/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateService(
            [FromRoute] string id,
            [FromBody] UpdateShopServiceCommand command,
            CancellationToken cancellationToken = default)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Service with ID {id} not found"));
            }

            return Ok(new JsonResponse<string>("Service updated successfully"));
        }

        /// <summary>
        /// Mark a service as deleted (soft delete)
        /// </summary>
        /// <param name="id">Service ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteService(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteShopServiceCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"Service with ID {id} not found"));
            }

            return Ok(new JsonResponse<string>("Service marked as deleted successfully"));
        }
        /// <summary>
        /// Filter shop services
        /// </summary>
        /// <param name="query">Filter parameters</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("filter")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<ShopServiceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<ShopServiceDto>>>> FilterShopServices(
            [FromQuery] FilterShopServiceQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<PagedResult<ShopServiceDto>>(result));
        }
    }
}
