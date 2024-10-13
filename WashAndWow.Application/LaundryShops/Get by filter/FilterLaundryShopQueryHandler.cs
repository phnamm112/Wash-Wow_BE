using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Pagination;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Get_by_filter
{
    public class FilterLaundryShopQueryHandler : IRequestHandler<FilterLaundryShopQuery, PagedResult<LaundryShopDto>>
    {
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FilterLaundryShopQueryHandler(ILaundryShopRepository laundryShopRepository
            , IUserRepository userRepository
            , IMapper mapper)
        {
            _laundryShopRepository = laundryShopRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<LaundryShopDto>> Handle(FilterLaundryShopQuery request, CancellationToken cancellationToken)
        {
            Func<IQueryable<LaundryShopEntity>, IQueryable<LaundryShopEntity>> queryOptions = query =>
            {
                query = query.Where(x => x.DeletedAt == null);
                if (!string.IsNullOrEmpty(request.OwnerID))
                {
                    query = query.Where(x => x.OwnerID.Contains(request.OwnerID));
                }
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(x => x.Name.Contains(request.Name));
                }
                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(x => x.Address.Contains(request.Address));
                }
                if (!string.IsNullOrEmpty(request.PhoneContact))
                {
                    query = query.Where(x => x.PhoneContact.Contains(request.PhoneContact));
                }
                if (!string.IsNullOrEmpty(request.Status.ToString()))
                {
                    query = query.Where(x => x.Status.Equals(request.Status));
                }
                return query;
            };
            var result = await _laundryShopRepository.FindAllAsync(request.PageNumber, request.PageSize, queryOptions, cancellationToken);
            var ownerNames = await _userRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.FullName, cancellationToken);

            return PagedResult<LaundryShopDto>.Create(
                totalCount: result.TotalCount,
                pageCount: result.PageCount,
                pageSize: result.PageSize,
                pageNumber: result.PageNo,
                data: result.MapToLaundryShopDtoList(_mapper, ownerNames));
        }
    }
}
