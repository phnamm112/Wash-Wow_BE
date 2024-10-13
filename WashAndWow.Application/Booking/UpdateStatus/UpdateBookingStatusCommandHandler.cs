using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
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
            //Validate user 
            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            // Validate booking exists
            var booking = await _bookingRepository.FindAsync(x => x.ID == request.BookingId && x.DeletedAt == null, cancellationToken);
            if (booking == null)
            {
                throw new NotFoundException("Booking not found");
            }
            // Validate status transition
            if (!IsValidBookingStatusTransition(booking.Status, request.NewStatus))
            {
                throw new InvalidOperationException($"Cannot change status from {booking.Status} to {request.NewStatus}");
            }
            // Update booking status
            booking.Status = request.NewStatus;
            booking.UpdaterID = _currentUserService.UserId;
            booking.LastestUpdateAt = DateTime.UtcNow;
            _bookingRepository.Update(booking);

            // Create and save notification if the status is updated to COMPLETED
            if (request.NewStatus == BookingStatus.COMPLETED)
            {
                var notification = new NotificationEntity
                {
                    ReceiverID = booking.CreatorID, // Notify the user who created the booking
                    Content = $"Your booking with ID: {booking.ID} has been marked as completed.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow,
                    CreatorID = _currentUserService.UserId,
                    Type = NotificationType.BookingCompleted // Assuming you have an enum for notification types
                };

                _notificationRepository.Add(notification);
            }
            // Create and save notification if the status is updated to COMPLETED
            if (request.NewStatus == BookingStatus.CANCELLED)
            {
                var notification = new NotificationEntity
                {
                    ReceiverID = booking.LaundryShop.ID, // Notify the shop of booking
                    Content = $"Booking for {booking.Customer.FullName} has been cancelled",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow,
                    CreatorID = _currentUserService.UserId,
                    Type = NotificationType.BookingCanceled
                };

                _notificationRepository.Add(notification);
            }
            // Save changes
            var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0 ? "Booking status updated successfully" : "Failed to update booking status";
        }
        private static bool IsValidBookingStatusTransition(BookingStatus from, BookingStatus to)
        {
            var validTransitions = new Dictionary<BookingStatus, List<BookingStatus>>
            {
                { BookingStatus.PENDING, new List<BookingStatus> { BookingStatus.CONFIRMED, BookingStatus.CANCELLED } },
                { BookingStatus.CONFIRMED, new List<BookingStatus> { BookingStatus.PROCESSING } },
                { BookingStatus.PROCESSING, new List<BookingStatus> { BookingStatus.COMPLETED } },
                { BookingStatus.COMPLETED, new List<BookingStatus>() }, // No further transitions allowed
                { BookingStatus.CANCELLED, new List<BookingStatus>() }  // No further transitions allowed
            };
            return validTransitions.ContainsKey(from) && validTransitions[from].Contains(to);
        }
    }
}
