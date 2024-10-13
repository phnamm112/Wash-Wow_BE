using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Application.Common.Pagination;

namespace WashAndWow.Application.Notification.GetAllNotificationByReceiverId
{
    public class GetAllNotificationByReceiverIdQuery : IRequest<PagedResult<NotificationDto>>, IQuery 
    {
        public string ReceiverId { get; set; }
        public bool? IsRead { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllNotificationByReceiverIdQuery()
        {
            
        }
        public GetAllNotificationByReceiverIdQuery(string receiverId,int pageNo, int pageSize, bool? isRead = null)
        {
            ReceiverId = receiverId;
            IsRead = isRead;
            PageNumber = pageNo;
            PageSize = pageSize;
        }
    }
}
