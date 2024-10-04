using MediatR;

namespace WashAndWow.Application.Booking.Read
{
    public class GetBookingByIdQuery : IRequest<BookingDto>
    {
        public string Id { get; }

        public GetBookingByIdQuery(string id)
        {
            Id = id;
        }
    }
}
