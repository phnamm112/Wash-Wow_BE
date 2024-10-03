using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Read
{
    public class GetAllShopServiceQueryHandler : IRequestHandler<GetAllShopServiceQuery, IPagedResult<ShopServiceDto>>
    {
        private readonly IShopServiceRepository _repository;
        private readonly IMapper _mapper;
        public GetAllShopServiceQueryHandler(IShopServiceRepository shopServiceRepository, IMapper mapper)
        {
            _repository = shopServiceRepository;
            _mapper = mapper;
        }
        public async Task<IPagedResult<ShopServiceDto>> Handle(GetAllShopServiceQuery request, CancellationToken cancellationToken)
        {

            var pagedResult = await _repository.FindAllAsync(x => x.ShopID == request.ShopId, request.PageNo, request.PageSize, cancellationToken);
            // Map the entities to DTOs
            var pagedDtoResult = new PagedList<ShopServiceDto>(
                pagedResult.TotalCount,
                pagedResult.PageNo,
                pagedResult.PageSize,
                _mapper.Map<List<ShopServiceDto>>(pagedResult.ToList())
            );

            return pagedDtoResult;
        }
    }
}
