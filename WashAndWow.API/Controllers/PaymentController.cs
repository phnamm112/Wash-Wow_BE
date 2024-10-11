using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WashAndWow.Application.Booking.Update;
using WashAndWow.Application.Payment.AfterPayment;
using WashAndWow.Application.PaymentPayOS.ConfirmWebhook;
using WashAndWow.Application.PaymentPayOS.CreatePaymentUrl;
using WashAndWow.Application.PaymentPayOS.GetPaymentInformation;
using WashAndWow.Application.PaymentPayOS.VerifyPaymentWebhookData;

namespace WashAndWow.API.Controllers
{
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("payOS")]
        public async Task<IActionResult> AddAsync([FromBody] CreatePaymentUrlCommand reqObj
            , CancellationToken cancellationToken = default)
        {

            var paymentReponse = await _mediator.Send(reqObj, cancellationToken);
            return Ok(paymentReponse);
        }

        [HttpGet("payOS/{id}")]
        public async Task<IActionResult> GetPayment([FromRoute] string id
            , CancellationToken cancellationToken = default)
        {
            GetPaymentInformationQuery query = new GetPaymentInformationQuery();
            query.PaymentID = int.Parse(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("payOS/confirm-webhook")]
        public async Task<IActionResult> ConfirmWebhook(ConfirmWebhookCommand command
            , CancellationToken cancellationToken = default)
        {

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("payOS/verify-wehook")]
        public async Task<IActionResult> VerifyWebhook([FromBody] VerifyPaymentWebhookDataQuery query
            , CancellationToken cancellationToken = default)
        {

            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        /// <summary>
        /// API to call update synchorous payment-booking after payOS payment process
        /// </summary>
        /// <param name="paymentID">ID of payment</param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("payments/payment/after-payment/{paymentID}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JsonResponse<string>>> UpdatePayment([FromRoute] int paymentID,
            [FromBody] AfterPaymentCommand command,
            CancellationToken cancellationToken = default)
        {
            command.PaymentID = paymentID;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }
    }
}
