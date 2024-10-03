using MediatR;

namespace WashAndWow.Application.BookingItem.Update
{
    public class UpdateBookingItemCommand : IRequest<BookingItemDto>
    {
        public string Id { get; set; }
        public string ServiceId { get; set; }
    }
}
