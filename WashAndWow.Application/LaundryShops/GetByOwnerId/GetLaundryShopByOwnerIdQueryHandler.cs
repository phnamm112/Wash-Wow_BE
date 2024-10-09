using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Pagination;
using Wash_Wow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.GetByOwnerId
{
    public class GetLaundryShopByOwnerIdQueryHandler : IRequestHandler<GetLaundryShopByOwnerIdQuery, PagedResult<LaundryShopDto>>
    {
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IMapper _mapper;

        public GetLaundryShopByOwnerIdQueryHandler(ILaundryShopRepository laundryShopRepository, IMapper mapper)
        {
            _laundryShopRepository = laundryShopRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<LaundryShopDto>> Handle(GetLaundryShopByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            // Filter laundry shops by Owner ID
            Func<IQueryable<LaundryShopEntity>, IQueryable<LaundryShopEntity>> queryOptions = query =>
            {
                query = query.Where(x => x.OwnerID == request.OwnerID && x.DeletedAt == null);
                return query;
            };

            // Get paginated result
            var result = await _laundryShopRepository.FindAllAsync(request.PageNumber, request.PageSize, queryOptions, cancellationToken);

            // Map entities to DTOs
            return PagedResult<LaundryShopDto>.Create(
                totalCount: result.TotalCount,
                pageCount: result.PageCount,
                pageSize: result.PageSize,
                pageNumber: result.PageNo,
                data: result.MapToLaundryShopDtoList(_mapper)
            );
        }
    }
}
