using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Exceptions;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Read
{
    public class GetShopServicebyIdQueryHandler : IRequestHandler<GetShopServiceByIdQuery, ShopServiceDto>
    {
        private readonly IShopServiceRepository _repository;
        private readonly IMapper _mapper;
        public GetShopServicebyIdQueryHandler(IShopServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ShopServiceDto?> Handle(GetShopServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var shopService = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);

            if (shopService == null || shopService.DeletedAt != null)
            {
                throw new NotFoundException($"Service with ID {request.Id} not found or is deleted");
            }

            return _mapper.Map<ShopServiceDto>(shopService);
        }
    }
}
