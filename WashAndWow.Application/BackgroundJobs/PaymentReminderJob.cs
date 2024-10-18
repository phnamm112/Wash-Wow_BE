using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wash_Wow.Domain.Enums.Enums;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace WashAndWow.Application.BackgroundJobs
{
    public class PaymentReminderJob : IJob
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<PaymentReminderJob> _logger;

        public PaymentReminderJob(
            IBookingRepository bookingRepository,
            IPaymentRepository paymentRepository,
            INotificationRepository notificationRepository,
            ILogger<PaymentReminderJob> logger)
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
            _logger.LogInformation("PaymentReminderJob started at: {time}", currentTime);
            // Get all bookings that are confirmed and payments are pending
            var bookingsToRemind = await _bookingRepository.FindAllAsync(b =>
                b.Status == BookingStatus.CONFIRMED);

            foreach (var booking in bookingsToRemind)
            {
                // Check if the associated payment exists and is pending
                var payment = await _paymentRepository.FindAsync(p => p.BookingID == booking.ID);
                if (payment != null && payment.Status == PaymentStatus.PENDING)
                {
                    // Create a notification reminding the user to process payment
                    var notification = new NotificationEntity
                    {
                        ReceiverID = booking.CreatorID, // Notify the user who created the booking
                        Content = $"Reminder: Please process your payment for booking ID: {booking.ID}.",
                        IsRead = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatorID = booking.CreatorID, 
                        Type = NotificationType.PaymentReminder 
                    };

                    _notificationRepository.Add(notification);
                }
            }

            // Save changes for notifications
            await _notificationRepository.UnitOfWork.SaveChangesAsync();
            _logger.LogInformation("PaymentReminderJob completed at: {time}", DateTime.UtcNow);
        }
    }
}
