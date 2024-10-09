using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Application.Common.Pagination;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.LaundryShops;
using WashAndWow.Application.LaundryShops.Create;
using WashAndWow.Application.LaundryShops.Delete;
using WashAndWow.Application.LaundryShops.Get_by_filter;
using WashAndWow.Application.LaundryShops.GetByOwnerId;
using WashAndWow.Application.LaundryShops.Read;
using WashAndWow.Application.LaundryShops.Update;

namespace WashAndWow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaundryShopController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaundryShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve all laudry shop data with filter 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("laundryShops/filter-laundry-shop")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<List<LaundryShopDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<List<LaundryShopDto>>>> FilterOrder(
            [FromQuery] FilterLaundryShopQuery query
            , CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> CreateLaundryShop(
            [FromBody] CreateLaundryShopCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }


        /// <summary>
        /// Retrieve laundry shop with ID
        /// </summary>
        /// <param name="id">ID of laundry shop</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(LaundryShopDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LaundryShopDto>> GetLaundryShopById([FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var query = new GetLaundryShopByIdQuery();
            query.Id = id;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        /// <summary>
        /// API BUGGING
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<LaundryShopDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<IPagedResult<LaundryShopDto>>>> GetLaundryShops(
            [FromQuery] GetAllLaundryShopsQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<PagedResult<LaundryShopDto>>(result));
        }
        /// <summary>
        /// Retrieve laundry shops with OwnerId
        /// </summary>
        /// <param name="ownerId">ID of laundry shop</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("by-owner/{ownerId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<LaundryShopDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LaundryShopDto>> GetLaundryShopByOwnerId([FromRoute] string ownerId,
            CancellationToken cancellationToken = default)
        {
            var query = new GetLaundryShopByOwnerIdQuery();
            query.OwnerID = ownerId;
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<PagedResult<LaundryShopDto>>(result));
        }
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JsonResponse<string>>> UpdateLaundryShop(
            [FromBody] UpdateLaundryShopCommand command,
            CancellationToken cancellationToken = default)
        {

            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"LaundryShop with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("LaundryShop updated successfully"));
        }


        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLaundryShop(string id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteLaundryShopCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"LaundryShop with ID {command.Id} not found"));
            }

            return Ok(new JsonResponse<string>("LaundryShop deleted successfully"));
        }
    }
}
