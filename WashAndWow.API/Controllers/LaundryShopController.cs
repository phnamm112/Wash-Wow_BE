using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.LaundryShops;
using WashAndWow.Application.LaundryShops.Create;
using WashAndWow.Application.LaundryShops.Delete;
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

            // return CreatedAtAction(nameof(GetLaundryShopById), new { id = result }, new JsonResponse<string>(result));
            return Ok(new JsonResponse<string>(result));
        }

        [HttpGet]
        [Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<LaundryShopDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JsonResponse<LaundryShopDto>>> GetLaundryShopById(string id, CancellationToken cancellationToken = default)
        {
            var query = new GetLaundryShopByIdQuery(id); 
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound(new JsonResponse<string>($"LaundryShop with ID {id} not found"));
            }

            return Ok(new JsonResponse<LaundryShopDto>(result));
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<IPagedResult<LaundryShopDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<IPagedResult<LaundryShopDto>>>> GetLaundryShops(
            [FromQuery] GetAllLaundryShopsQuery query,
            CancellationToken cancellationToken = default) 
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<IPagedResult<LaundryShopDto>>(result));
        }

        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JsonResponse<string>>> UpdateLaundryShop(
            [FromBody] UpdateLaundryShopCommand command,
            CancellationToken cancellationToken = default) {
            
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
        public async Task<IActionResult> DeleteLaundryShop(string id, CancellationToken cancellationToken = default)
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
