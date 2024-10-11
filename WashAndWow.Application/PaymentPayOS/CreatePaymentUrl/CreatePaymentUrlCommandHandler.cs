using MediatR;
using Microsoft.Extensions.Options;
using Net.payOS.Types;
using Net.payOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities.Third_Party_define;
using Microsoft.Extensions.Configuration;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;
using Wash_Wow.Domain.Common.Exceptions;
using WashAndWow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;
using FluentValidation;

namespace WashAndWow.Application.PaymentPayOS.CreatePaymentUrl
{
    public class CreatePaymentUrlCommandHandler : IRequestHandler<CreatePaymentUrlCommand, CreatePaymentResult>
    {
        private readonly IConfiguration _config;
        private readonly PayOSKey _payOSKey;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingItemRepository _bookingItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentUrlCommandHandler(IConfiguration config
            , IOptions<PayOSKey> payOSKey
            , IBookingRepository bookingRepository
            , IBookingItemRepository bookingItemRepository
            , IUserRepository userRepository
            , IPaymentRepository paymentRepository)
        {
            _config = config;
            _payOSKey = payOSKey.Value;
            _bookingRepository = bookingRepository;
            _bookingItemRepository = bookingItemRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<CreatePaymentResult> Handle(CreatePaymentUrlCommand request, CancellationToken cancellationToken)
        {
            var domain = _payOSKey.Domain;
            PayOS payOS = new PayOS(apiKey: _payOSKey.ApiKey, checksumKey: _payOSKey.ChecksumKey, clientId: _payOSKey.ClientId);
            var booking = await _bookingRepository.FindAsync(x => x.ID == request.BookingID && x.DeletedAt == null, cancellationToken);
            if (booking == null)
            {
                throw new NotFoundException("Booking is not existed");
            }
            var buyer = await _userRepository.FindAsync(x => x.ID == request.BuyerID && x.DeletedAt == null, cancellationToken);
            if (buyer == null)
            {
                throw new NotFoundException("Buyer is not existed");
            }
            var payment = await _paymentRepository.FindAsync(x => x.ID == (int)request.PaymentID && x.DeletedAt == null, cancellationToken);
            if (payment == null)
            {
                throw new NotFoundException("Payment is not existed");
            }
            if (payment.Status != PaymentStatus.PENDING || payment.BookingID != booking.ID)
            {
                throw new ValidationException("Invalid payment");
            }
            List<BookingItemEntity> bookingItems = await _bookingItemRepository.FindAllAsync(x => x.BookingID == booking.ID && x.DeletedAt == null , cancellationToken);
            List<ItemData> items = new List<ItemData>();

            foreach (var bookingItem in bookingItems)
            {
                ItemData item = new ItemData(bookingItem.ShopService.Name, 1, (int)bookingItem.Amount);
                items.Add(item);
            }
            if (booking.Voucher != null)
            {
                ItemData item = new ItemData(booking.Voucher.Name, 1, (int)booking.Voucher.Amount);
                items.Add(item);
            }
            var returnUrl = $"payments/success?ID={payment.ID}&userID={booking.CreatorID}&bookingID={booking.ID}";
            var cancelUrl = $"payments/cancel?ID={payment.ID}&userID={booking.CreatorID}&bookingID={booking.ID}";

            PaymentData paymentData = new PaymentData(
                orderCode: request.PaymentID,
                amount: (int)booking.TotalPrice,
                description: booking.Note ?? "PAYOS",
                items: items,
                buyerName: booking.CreatorID,
                buyerPhone: booking.Customer.FullName,
                expiredAt: (int)(DateTimeOffset.Now.AddMinutes(3).ToUnixTimeSeconds()),
                cancelUrl: $"{domain}/{cancelUrl}",
                returnUrl: $"{domain}/{returnUrl}"
                );

            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            return createPayment;
        }
    }
}
