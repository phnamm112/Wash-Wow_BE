using EXE2_Wash_Wow.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Wash_Wow.Application.Common.Pagination;
using WashAndWow.Application.Notification.GetAllNotificationByReceiverId;
using WashAndWow.Application.Notification.MarkAsRead;
using WashAndWow.Application.Notification;

namespace WashAndWow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve all notifications by receiver ID (User or Shop) with pagination
        /// </summary>
        /// <param name="receiverId">ID of the receiver</param>
        /// <param name="pageNo">Pagination parameters</param>
        /// <param name="pageSize">Pagination parameters</param>
        /// <param name="isRead">Status of Notification</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Paginated list of notifications</returns>
        [HttpGet]
        [Route("{receiverId}/notifications")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<NotificationDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotificationsByReceiverId(
            [FromRoute]string receiverId,
            [FromQuery] int pageNo, 
            [FromQuery] int pageSize,
            [FromQuery] bool isRead,
            CancellationToken cancellationToken)
        {
            var getAllQuery = new GetAllNotificationByReceiverIdQuery(receiverId, pageNo, pageSize, isRead);
            var result = await _mediator.Send(getAllQuery, cancellationToken);

            if (result.TotalCount == 0)
                return NotFound("No notifications found.");

            return Ok(new JsonResponse<PagedResult<NotificationDto>>(result));
        }

        /// <summary>
        /// Mark multiple notifications as read
        /// </summary>
        /// <param name="command">List of notification IDs</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Success or failure response</returns>
        [HttpPut]
        [Route("notifications/read")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkNotificationsAsRead(
            [FromBody] UpdateNotificationIsReadCommand command,
            CancellationToken cancellationToken)
        {
            if (command.NotificationIds == null || command.NotificationIds.Count == 0)
                return BadRequest("Notification IDs cannot be empty.");

            var result = await _mediator.Send(command, cancellationToken);

            return result
                ? Ok(new JsonResponse<string>("Notifications marked as read."))
                : BadRequest(new JsonResponse<string>("Failed to update notifications."));
    }}
}
