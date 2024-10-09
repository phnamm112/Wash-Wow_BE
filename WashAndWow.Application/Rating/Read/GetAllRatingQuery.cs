using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.Rating.Read
{
    public class GetAllRatingQuery : IRequest<IPagedResult<RatingDto>>, IQuery
    {
        public string ShopId { get; }
        public int PageNo { get; } = 1;
        public int PageSize { get; } = 10;
        public GetAllRatingQuery()
        {
        }

        public GetAllRatingQuery(string shopId, int pageNo, int pageSize)
        {
            ShopId = shopId;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
