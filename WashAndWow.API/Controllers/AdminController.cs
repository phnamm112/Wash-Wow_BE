using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WashAndWow.Application.Users.AssignRole;

namespace WashAndWow.API.Controllers
{
    [ApiController]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Assign a role to a user.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <param name="command">The role assignment command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result indicating whether the role was successfully assigned.</returns>
        [HttpPut]
        [Route("/users/{userID}/role")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> AssignRole([FromRoute] string userID
            , [FromBody] AssignRoleCommand command
            , CancellationToken cancellationToken = default)
        {
            command.UserID = userID;
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Contains("banned"))
            {
                return BadRequest(new JsonResponse<string>(result));
            }
            return Ok(new JsonResponse<string>(result));
        }
    }
}
