using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.BookingItem.Read
{
    public class GetBookingItemByIdQuery : IRequest<BookingItemDto>, IQuery
    {
        public string Id { get; }

        public GetBookingItemByIdQuery(string id)
        {
            Id = id;
        }
    }
}
