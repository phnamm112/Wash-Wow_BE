using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Application.Common.Pagination;

namespace WashAndWow.Application.LaundryShops.GetByOwnerId
{
    public class GetLaundryShopByOwnerIdQuery : IRequest<PagedResult<LaundryShopDto>>, IQuery
    {
        public string OwnerID { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetLaundryShopByOwnerIdQuery()
        {

        }
        public GetLaundryShopByOwnerIdQuery(string ownerId, int pageNumber, int pageSize)
        {
            OwnerID = ownerId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
