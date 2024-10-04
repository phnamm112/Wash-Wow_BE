using MediatR;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.BookingItem.Read
{
    public class GetAllBookingItemByBookingIdQuery : IRequest<IPagedResult<BookingItemDto>>
    {
        public string BookingId { get; }
        public int PageNo { get; } = 1;
        public int PageSize { get; } = 10;
        public GetAllBookingItemByBookingIdQuery()
        {
        }

        public GetAllBookingItemByBookingIdQuery(string bookingId, int pageNo, int pageSize)
        {
            BookingId = bookingId;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}



