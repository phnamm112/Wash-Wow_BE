using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Application.Common.Pagination;

namespace WashAndWow.Application.ShopService.GetByOwnerId
{
    public class GetShopServiceByOwnerIdQuery : IRequest<PagedResult<ShopServiceDto>>, IQuery
    {
        public string OwnerID { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetShopServiceByOwnerIdQuery()
        {
        }
        public GetShopServiceByOwnerIdQuery(string ownerId, int pageNumber, int pageSize)
        {
            OwnerID = ownerId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
