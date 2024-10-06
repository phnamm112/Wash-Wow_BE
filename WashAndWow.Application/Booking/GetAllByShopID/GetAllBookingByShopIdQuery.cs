using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Application.Common.Pagination;

namespace WashAndWow.Application.Booking.GetAllByShopID
{
    public class GetAllBookingbyShopIdQuery : IRequest<PagedResult<BookingDto>>, IQuery
    {
        public string ShopId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllBookingbyShopIdQuery()
        {
        }

        public GetAllBookingbyShopIdQuery(string shopId, int no, int pageSize)
        {
            PageNumber = no;
            PageSize = pageSize;
        }
    }
}
