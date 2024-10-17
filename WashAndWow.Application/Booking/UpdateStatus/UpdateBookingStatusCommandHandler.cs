using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Booking.UpdateStatus
{
    public class UpdateBookingStatusCommandHandler : IRequestHandler<UpdateBookingStatusCommand, string>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public UpdateBookingStatusCommandHandler(
            IBookingRepository bookingRepository,
            INotificationRepository notificationRepository,
            ICurrentUserService currentUserService,
            IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _notificationRepository = notificationRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateBookingStatusCommand request, CancellationToken cancellationToken)
        {
            // Validate user
            var user = await _userRepository.FindAsync(
                x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null)
                throw new NotFoundException("User not found.");

            // Validate booking exists
            var booking = await _bookingRepository.FindAsync(
                x => x.ID == request.BookingId && x.DeletedAt == null, cancellationToken);
            if (booking == null)
                throw new NotFoundException("Booking not found.");

            // Parse the new status
            var newStatus = ParseStatus(request.NewStatus);

            // Validate status transition
            if (!IsValidBookingStatusTransition(booking.Status, newStatus))
                throw new InvalidOperationException($"Cannot change status from {booking.Status} to {newStatus}.");

            // Update booking status
            booking.Status = newStatus;
            booking.UpdaterID = _currentUserService.UserId;
            booking.LastestUpdateAt = DateTime.UtcNow;
            _bookingRepository.Update(booking);

            // Create appropriate notification
            var notification = CreateNotification(booking, newStatus);
            if (notification != null)
                _notificationRepository.Add(notification);

            // Save changes
            var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0 ? "Booking status updated successfully" : "Failed to update booking status";
        }

        private static bool IsValidBookingStatusTransition(BookingStatus from, BookingStatus to)
        {
            var validTransitions = new Dictionary<BookingStatus, List<BookingStatus>>
            {
                { BookingStatus.PENDING, new List<BookingStatus> { BookingStatus.CONFIRMED, BookingStatus.CANCELLED, BookingStatus.REJECTED } },
                { BookingStatus.CONFIRMED, new List<BookingStatus> { BookingStatus.PROCESSING } },
                { BookingStatus.PROCESSING, new List<BookingStatus> { BookingStatus.COMPLETED } },
                { BookingStatus.COMPLETED, new List<BookingStatus>() }, // No further transitions allowed
                { BookingStatus.CANCELLED, new List<BookingStatus>() }, // No further transitions allowed
                { BookingStatus.REJECTED, new List<BookingStatus>() }  // No further transitions allowed
            };

            return validTransitions.ContainsKey(from) && validTransitions[from].Contains(to);
        }

        private NotificationEntity CreateNotification(BookingEntity booking, BookingStatus newStatus)
        {
            // Determine the notification type based on the new status
            NotificationType? notificationType = newStatus switch
            {
                BookingStatus.COMPLETED => NotificationType.BookingCompleted,
                BookingStatus.CANCELLED => NotificationType.BookingCanceled,
                BookingStatus.CONFIRMED => NotificationType.BookingConfirmed,
                BookingStatus.REJECTED => NotificationType.BookingRejected,
                _ => null
            };

            // If no valid notification type, return null (no notification needed)
            if (notificationType == null)
                return null;

            // Determine the receiver of the notification based on the new status
            var receiverId = newStatus switch
            {
                BookingStatus.CANCELLED or BookingStatus.REJECTED => booking.Customer.ID,
                _ => booking.LaundryShop.ID
            };

            // Create the content message for the notification
            var content = newStatus switch
            {
                BookingStatus.COMPLETED => $"Your booking with ID: {booking.ID} has been marked as completed.",
                BookingStatus.CANCELLED => $"Booking for {booking.Customer.FullName} has been cancelled.",
                BookingStatus.CONFIRMED => $"Your booking to {booking.LaundryShop.Name} has been confirmed.",
                BookingStatus.REJECTED => $"Your booking to {booking.LaundryShop.Name} has been rejected by the shop.",
                _ => string.Empty
            };

            // Return the constructed notification entity
            return new NotificationEntity
            {
                ReceiverID = receiverId,
                Content = content,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                CreatorID = _currentUserService.UserId,
                Type = notificationType.Value
            };
        }


        private BookingStatus ParseStatus(object status)
        {
            if (status is int intStatus && Enum.IsDefined(typeof(BookingStatus), intStatus))
                return (BookingStatus)intStatus;

            if (status is string strStatus)
            {
                if (int.TryParse(strStatus, out var parsedInt) && Enum.IsDefined(typeof(BookingStatus), parsedInt))
                    return (BookingStatus)parsedInt;

                if (Enum.TryParse<BookingStatus>(strStatus, true, out var parsedEnum))
                    return parsedEnum;
            }

            throw new ArgumentException($"Invalid booking status: {status}");
        }
    }
}
