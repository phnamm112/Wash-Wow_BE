using MediatR;

namespace WashAndWow.Application.BookingItem.Read
{
    public class GetBookingItemByIdQuery : IRequest<BookingItemDto>
    {
        public string Id { get; }

        public GetBookingItemByIdQuery(string id)
        {
            Id = id;
        }
    }
}
