using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Pagination;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Read
{
    public class GetAllLaundryShopsQueryHandler : IRequestHandler<GetAllLaundryShopsQuery, PagedResult<LaundryShopDto>>
    {
        private readonly ILaundryShopRepository _shopRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllLaundryShopsQueryHandler(ILaundryShopRepository repository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _shopRepository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<LaundryShopDto>> Handle(GetAllLaundryShopsQuery request, CancellationToken cancellationToken)
        {
            // Get paged list of LaundryShopEntity from the repository
            var pagedResult = await _shopRepository.FindAllAsync(x => x.DeletedAt == null, request.PageNo, request.PageSize, cancellationToken);
            var ownerNames = await _userRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.FullName, cancellationToken);
            // Map the entities to DTOs
            return PagedResult<LaundryShopDto>.Create(
               totalCount: pagedResult.TotalCount,
               pageCount: pagedResult.PageCount,
               pageSize: pagedResult.PageSize,
               pageNumber: pagedResult.PageNo,
               data: pagedResult.MapToLaundryShopDtoList(_mapper, ownerNames));
        }

    }
}
