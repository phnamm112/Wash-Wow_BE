using MediatR;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Read
{
    public class GetAllShopServiceQuery : IRequest<IPagedResult<ShopServiceDto>>
    {
        public string ShopId { get; }
        public int PageNo { get; } = 1;
        public int PageSize { get; } = 10;
        public GetAllShopServiceQuery()
        {

        }
        public GetAllShopServiceQuery(string shopId, int pageNo, int pageSize)
        {
            ShopId = shopId;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
