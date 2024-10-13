using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Notification.MarkAsRead
{
    public class UpdateNotificationIsReadCommand : IRequest<bool>, ICommand
    {
        public List<string> NotificationIds { get; set; }
        public UpdateNotificationIsReadCommand()
        {
            
        }
        public UpdateNotificationIsReadCommand(List<string> notificationIds)
        {
            NotificationIds = notificationIds;
        }
    }
}
