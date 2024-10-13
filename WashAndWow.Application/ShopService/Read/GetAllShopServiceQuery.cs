using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Read
{
    public class GetAllShopServiceQuery : IRequest<IPagedResult<ShopServiceDto>>, IQuery
    {
        public string ShopId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public GetAllShopServiceQuery()
        {

        }
        public GetAllShopServiceQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
