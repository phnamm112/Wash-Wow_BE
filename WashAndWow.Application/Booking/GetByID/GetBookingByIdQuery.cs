using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Booking.GetByID
{
    public class GetBookingByIdQuery : IRequest<BookingDto>, IQuery
    {
        public string Id { get; }

        public GetBookingByIdQuery(string id)
        {
            Id = id;
        }
    }
}
