using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Application.Common.Interfaces;
using WashAndWow.Application.Form;
using WashAndWow.Application.Form.ApprovalForm;
using WashAndWow.Application.Form.GetAll;
using WashAndWow.Application.Form.GetFormByID;
using WashAndWow.Application.Form.SendForm;
using WashAndWow.Application.Users.AssignRole;

namespace WashAndWow.API.Controllers
{
    [ApiController]
    //[Authorize]
    public class FormController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("form")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> SendForm([FromBody] SendFormCommand command
            , CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);            
            return Ok(new JsonResponse<string>(result));
        }

        /// <summary>
        /// 
        /// Update form status APPROVED/REJECTED
        /// 
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("/forms/{formID}/status")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> UpdateForm([FromRoute] string formID
            , [FromBody] ApprovalFormCommand command
            , CancellationToken cancellationToken = default)
        {
            command.FormID = formID;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpGet]
        [Route("/form/{id}")]
        [ProducesResponseType(typeof(JsonResponse<FormDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FormDto>> GetFormByID(
                 [FromRoute] string id,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetFormByIDQuery(id: id), cancellationToken);
            return result != null ? Ok(new JsonResponse<FormDto>(result)) : NotFound();
        }

        [HttpGet]
        [Route("/forms")]
        [ProducesResponseType(typeof(JsonResponse<List<FormDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FormDto>> GetAllForms(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllFormQuery(), cancellationToken);
            return result != null ? Ok(new JsonResponse<List<FormDto>>(result)) : NotFound();
        }
    }
}
