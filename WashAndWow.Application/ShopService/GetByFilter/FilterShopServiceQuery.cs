using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Application.Common.Pagination;

namespace WashAndWow.Application.ShopService.GetByFilter
{
    public class FilterShopServiceQuery : IRequest<PagedResult<ShopServiceDto>>, IQuery
    {

        public FilterShopServiceQuery() { }

        public FilterShopServiceQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? ShopId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatorId { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
    }
}
