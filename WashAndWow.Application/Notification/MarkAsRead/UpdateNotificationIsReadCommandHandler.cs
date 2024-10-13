using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Common.Exceptions;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Notification.MarkAsRead
{
    public class UpdateNotificationIsReadCommandHandler : IRequestHandler<UpdateNotificationIsReadCommand, bool>
    {
        private readonly INotificationRepository _notificationRepository;

        public UpdateNotificationIsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> Handle(UpdateNotificationIsReadCommand request, CancellationToken cancellationToken)
        {
            if (!request.NotificationIds.Any())
                throw new NotFoundException("No notifications provided.");

            // Fetch notifications by their IDs
            var notifications = await _notificationRepository.FindAllAsync(
                n => request.NotificationIds.Contains(n.ID) && n.DeletedAt == null, cancellationToken);

            if (!notifications.Any())
                throw new NotFoundException("No matching notifications found.");

            // Update IsRead status and ReadAt timestamp for all notifications
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                _notificationRepository.Update(notification);
            }

            // Save changes to the repository
            var result = await _notificationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
