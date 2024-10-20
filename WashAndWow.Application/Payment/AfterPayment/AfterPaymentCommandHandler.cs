using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Payment.AfterPayment
{
    public class AfterPaymentCommandHandler : IRequestHandler<AfterPaymentCommand, string>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IBookingItemRepository _bookingItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly INotificationRepository _notificationRepository;

        public AfterPaymentCommandHandler(IBookingRepository bookingRepository
            , IMapper mapper
            , IBookingItemRepository bookingItemRepository
            , ICurrentUserService currentUserService
            , ILaundryShopRepository laundryShopRepository
            , IShopServiceRepository shopServiceRepository
            , IVoucherRepository voucherRepository
            , IUserRepository userRepository
            , IPaymentRepository paymentRepository
            , INotificationRepository notificationRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _bookingItemRepository = bookingItemRepository;
            _currentUserService = currentUserService;
            _laundryShopRepository = laundryShopRepository;
            _shopServiceRepository = shopServiceRepository;
            _voucherRepository = voucherRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<string> Handle(AfterPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.FindAsync(x => x.ID == request.PaymentID && x.DeletedAt == null, cancellationToken);
            if (payment == null)
            {
                throw new NotFoundException("Payment is not existed");
            }
            var booking = await _bookingRepository.FindAsync(x => x.ID == request.OrderID && x.DeletedAt == null, cancellationToken);
            if (booking == null)
            {
                throw new NotFoundException("Booking is not existed");
            }
            var voucher = booking.Voucher;
            var customer = booking.Customer;
            if (request.Status.Equals(PaymentStatus.CANCEL) || request.Status.Equals(PaymentStatus.FAILURE))
            {
                booking.Status = BookingStatus.CANCELLED;
                payment.Status = request.Status;
                if (voucher != null)
                {
                    voucher.Users.Remove(customer);
                    voucher.Bookings.Remove(booking);
                    customer.UsedVouchers.Remove(voucher);
                }
                return await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failure";
            }
            booking.Status = BookingStatus.COMPLETED;
            payment.Status = request.Status;
            var laundryShop = booking.LaundryShop;
            //TODO: DEFINE CÔNG THỨC Ở FUNCTION TRONG DOMAIN
            //Handle voucher của shop tạo
            if (voucher != null && voucher.Creator.LaundryShops.Contains(laundryShop))
            {
                laundryShop.Wallet += booking.TotalPrice * (decimal)0.88;
            }
            else
            //Handle voucher của admin tạo
            if (voucher != null && voucher.Creator.Role.Equals(Role.Admin))
            {
                decimal baseTotalPrice = booking.TotalPrice + booking.VoucherDiscounted ?? 0;
                laundryShop.Wallet += baseTotalPrice * (decimal)0.88;
            }
            // Create notifications for both customer and shop
            var customerNotification = new NotificationEntity
            {
                ReceiverID = customer.ID,
                Content = $"Your payment for Booking {booking.ID} at {laundryShop.Name} has been processed successfully.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                CreatorID = _currentUserService.UserId,
                Type = NotificationType.BookingCreated
            };

            var shopOwnerNotification = new NotificationEntity
            {
                ReceiverID = laundryShop.ID,
                Content = $"A payment has been made for Booking {booking.ID} by {customer.FullName}.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                CreatorID = _currentUserService.UserId,
                Type = NotificationType.BookingCreated
            };

            _notificationRepository.Add(customerNotification);
            _notificationRepository.Add(shopOwnerNotification);
            return await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failure";
        }
    }
}
