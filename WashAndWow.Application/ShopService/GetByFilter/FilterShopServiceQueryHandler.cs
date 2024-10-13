using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Pagination;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.GetByFilter
{
    public class FilterShopServiceQueryHandler : IRequestHandler<FilterShopServiceQuery, PagedResult<ShopServiceDto>>
    {
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IMapper _mapper;

        public FilterShopServiceQueryHandler(IShopServiceRepository shopServiceRepository,
            ILaundryShopRepository laundryShopRepository, IMapper mapper)
        {
            _shopServiceRepository = shopServiceRepository;
            _laundryShopRepository = laundryShopRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShopServiceDto>> Handle(FilterShopServiceQuery request, CancellationToken cancellationToken)
        {
            Func<IQueryable<ShopServiceEntity>, IQueryable<ShopServiceEntity>> queryOptions = query =>
            {
                query = query.Where(x => x.DeletedAt == null);

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(x => x.Name.Contains(request.Name));
                }

                if (!string.IsNullOrEmpty(request.Description))
                {
                    query = query.Where(x => x.Description.Contains(request.Description));
                }

                if (!string.IsNullOrEmpty(request.ShopId))
                {
                    query = query.Where(x => x.ShopID == request.ShopId);
                }

                if (request.PriceMin.HasValue)
                {
                    query = query.Where(x => x.PricePerKg >= request.PriceMin.Value);
                }

                if (request.PriceMax.HasValue)
                {
                    query = query.Where(x => x.PricePerKg <= request.PriceMax.Value);
                }

                return query;
            };

            var result = await _shopServiceRepository.FindAllAsync(request.PageNo, request.PageSize, queryOptions, cancellationToken);
            var shopNames = await _laundryShopRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.Name, cancellationToken);
            return PagedResult<ShopServiceDto>.Create(
               totalCount: result.TotalCount,
               pageCount: result.PageCount,
               pageSize: result.PageSize,
               pageNumber: result.PageNo,
               data: result.MapToShopServiceDtoList(_mapper, shopNames));
        }
    }
}
