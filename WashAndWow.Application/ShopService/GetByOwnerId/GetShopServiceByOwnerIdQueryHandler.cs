using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Pagination;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.GetByOwnerId
{
    public class GetShopServiceByOwnerIdQueryHandler : IRequestHandler<GetShopServiceByOwnerIdQuery, PagedResult<ShopServiceDto>>
    {
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IMapper _mapper;

        public GetShopServiceByOwnerIdQueryHandler(
            IShopServiceRepository shopServiceRepository,
            ILaundryShopRepository laundryShopRepository,
            IMapper mapper)
        {
            _shopServiceRepository = shopServiceRepository;
            _laundryShopRepository = laundryShopRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShopServiceDto>> Handle(GetShopServiceByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            // Validate if the owner exists
            var laundryShop = await _laundryShopRepository.FindAllAsync(x => x.OwnerID == request.OwnerID && x.DeletedAt == null, cancellationToken);
            if (!laundryShop.Any())
            {
                throw new UnauthorizedAccessException("Invalid Owner ID.");
            }

            // Filter shop services by Owner ID
            Func<IQueryable<ShopServiceEntity>, IQueryable<ShopServiceEntity>> queryOptions = query =>
            {
                query = query.Where(x => x.LaundryShop.OwnerID == request.OwnerID && x.LaundryShop.DeletedAt == null);
                return query;
            };

            // Get paginated result
            var result = await _shopServiceRepository.FindAllAsync(request.PageNumber, request.PageSize, queryOptions, cancellationToken);
            var shopNames = await _laundryShopRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.Name, cancellationToken);
            // Map entities to DTOs
            return PagedResult<ShopServiceDto>.Create(
                totalCount: result.TotalCount,
                pageCount: result.PageCount,
                pageSize: result.PageSize,
                pageNumber: result.PageNo,
                data: result.MapToShopServiceDtoList(_mapper, shopNames)
            );
        }
    }
}
