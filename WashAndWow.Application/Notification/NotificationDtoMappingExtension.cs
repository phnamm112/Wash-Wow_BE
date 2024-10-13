using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Notification
{
    public static class NotificationDtoMappingExtension
    {
        public static NotificationDto MapToNotificationDto(this NotificationEntity notification, IMapper mapper)
            => mapper.Map<NotificationDto>(notification);

        public static List<NotificationDto> MapToNotificationDtoList(this IEnumerable<NotificationEntity> notifications, IMapper mapper)
            => notifications.Select(n => n.MapToNotificationDto(mapper)).ToList();

        public static NotificationDto MapToNotificationDto(this NotificationEntity notification, IMapper mapper, string receiverName)
        {
            var dto = mapper.Map<NotificationDto>(notification);
            dto.ReceiverName = receiverName;
            dto.Status = notification.IsRead ? "Read" : "Unread";
            return dto;
        }

        public static List<NotificationDto> MapToNotificationDtoList(this IEnumerable<NotificationEntity> notifications, IMapper mapper,
        Dictionary<string, string> userNames, Dictionary<string, string> shopNames)
        {
            return notifications.Select(n =>
            {
                // Determine if the receiver is a user or shop, then get the correct name.
                var receiverName = userNames.ContainsKey(n.ReceiverID)
                    ? userNames[n.ReceiverID]
                    : shopNames.ContainsKey(n.ReceiverID)
                        ? shopNames[n.ReceiverID]
                        : "Unknown";

                return n.MapToNotificationDto(mapper, receiverName);
            }).ToList();
        }
    }
}
