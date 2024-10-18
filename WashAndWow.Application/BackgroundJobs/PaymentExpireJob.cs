using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wash_Wow.Domain.Enums.Enums;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace WashAndWow.Application.BackgroundJobs
{
    public class PaymentExpireJob : IJob
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<PaymentExpireJob> _logger;

        public PaymentExpireJob(
            IBookingRepository bookingRepository,
            IPaymentRepository paymentRepository,
            INotificationRepository notificationRepository,
            ILogger<PaymentExpireJob> logger)
        {
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Get the current time
            var currentTime = DateTime.UtcNow;
            _logger.LogInformation("PaymentExpireJob started at: {time}", currentTime);
            // Get all bookings that are confirmed and within 1 hour before the pickup time
            var bookingsToCheck = await _bookingRepository.FindAllAsync(b =>
                b.Status == BookingStatus.CONFIRMED && b.ShopPickupTime <= currentTime.AddHours(1));

            foreach (var booking in bookingsToCheck)
            {
                // Check if the payment is expired
                var payment = await _paymentRepository.FindAsync(p => p.BookingID == booking.ID);

                if (payment != null && payment.Status == PaymentStatus.PENDING)
                {
                    // Cancel the booking and notify the user
                    booking.Status = BookingStatus.CANCELLED;
                    booking.LastestUpdateAt = DateTime.UtcNow;
                    _bookingRepository.Update(booking);

                    // Create a notification for the user
                    var notification = new NotificationEntity
                    {
                        ReceiverID = booking.CreatorID, // Notify the user who created the booking
                        Content = $"Your booking with ID: {booking.ID} has been cancelled due to payment expiration.",
                        IsRead = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatorID = booking.CreatorID, // You can modify this to a service user ID if needed
                        Type = NotificationType.PaymentExpired // Assuming you have an enum for notification types
                    };

                    _notificationRepository.Add(notification);
                }
            }

            // Save changes for bookings and notifications
            await _bookingRepository.UnitOfWork.SaveChangesAsync();
            await _notificationRepository.UnitOfWork.SaveChangesAsync();

            _logger.LogInformation("PaymentExpireJob completed at: {time}", DateTime.UtcNow);
        }
    }
}
