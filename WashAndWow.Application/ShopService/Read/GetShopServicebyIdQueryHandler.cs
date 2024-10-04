using AutoMapper;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Read
{
    public class GetShopServicebyIdQueryHandler
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
            var shop = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);

            if (shop == null)
                return null;

            return _mapper.Map<ShopServiceDto>(shop);
        }
    }
}
