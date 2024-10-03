using MediatR;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Read
{
    public class GetAllLaundryShopsQuery : IRequest<IPagedResult<LaundryShopDto>>
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
