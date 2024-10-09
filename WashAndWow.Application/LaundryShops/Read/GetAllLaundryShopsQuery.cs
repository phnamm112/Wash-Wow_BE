using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Application.Common.Pagination;

namespace WashAndWow.Application.LaundryShops.Read
{
    public class GetAllLaundryShopsQuery : IRequest<PagedResult<LaundryShopDto>>, IQuery
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetAllLaundryShopsQuery()
        {

        }
        public GetAllLaundryShopsQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }

}
