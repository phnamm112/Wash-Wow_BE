using MediatR;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.Booking.Read
{
    public class GetAllBookingbyShopIdQuery : IRequest<IPagedResult<BookingDto>>
    {
        public string ShopId { get; }
        public int PageNo { get; } = 1;
        public int PageSize { get; } = 10;

        public GetAllBookingbyShopIdQuery(string shopId, int pageNo, int pageSize)
        {
            ShopId = shopId;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
