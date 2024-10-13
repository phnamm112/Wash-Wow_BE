using AutoMapper;
using Castle.Core.Resource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Models;
using Wash_Wow.Application.Common.Pagination;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Application.Booking;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WashAndWow.Application.Notification.GetAllNotificationByReceiverId
{
    public class GetAllNotificationByReceiverIdQueryHandler
        : IRequestHandler<GetAllNotificationByReceiverIdQuery, PagedResult<NotificationDto>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IMapper _mapper;

        public GetAllNotificationByReceiverIdQueryHandler(
            INotificationRepository notificationRepository,
            IUserRepository userRepository,
            ILaundryShopRepository laundryShopRepository,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _laundryShopRepository = laundryShopRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<NotificationDto>> Handle(
            GetAllNotificationByReceiverIdQuery request,
            CancellationToken cancellationToken)
        {
            IPagedResult<NotificationEntity> notification;
            if (request.IsRead.HasValue)
            {
                notification = await _notificationRepository.FindAllAsync(x => x.ReceiverID == request.ReceiverId && x.IsRead == request.IsRead,
                    request.PageNumber, request.PageSize, cancellationToken);
            }
            else
            {
                notification = await _notificationRepository.FindAllAsync(x => x.ReceiverID == request.ReceiverId,
                    request.PageNumber, request.PageSize, cancellationToken);
            }
            var user = await _userRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.FullName, cancellationToken);
            var laundryShops = await _laundryShopRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.Name, cancellationToken);
            return PagedResult<NotificationDto>.Create(
           totalCount: notification.TotalCount,
           pageCount: notification.PageCount,
            pageSize: notification.PageSize,
            pageNumber: notification.PageNo,
               data: notification.MapToNotificationDtoList(_mapper, user, laundryShops));
        }
    }
}
