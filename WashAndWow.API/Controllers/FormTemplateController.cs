using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WashAndWow.Application.Form.GetFormByID;
using WashAndWow.Application.Form;
using WashAndWow.Application.FormTemplate;
using WashAndWow.Application.FormTemplate.GetAll;

namespace WashAndWow.API.Controllers
{
    [ApiController]
    //[Authorize]
    public class FormTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FormTemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/formTemplates")]
        [ProducesResponseType(typeof(JsonResponse<List<FormTemplateDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FormTemplateDto>> GetFormTemplates(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllFormTemplateQuery(), cancellationToken);
            return result != null ? Ok(new JsonResponse<List<FormTemplateDto>>(result)) : NotFound();
        }
    }
}
