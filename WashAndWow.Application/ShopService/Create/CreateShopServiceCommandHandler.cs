using MediatR;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Create
{
    public class CreateShopServiceCommandHandler : IRequestHandler<CreateRatingCommand, string>
    {
        private readonly IShopServiceRepository _repository;
        public CreateShopServiceCommandHandler(IShopServiceRepository shopServiceRepository)
        {
            _repository = shopServiceRepository;
        }
        public async Task<string> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var shopServiceEntity = new ShopServiceEntity
            {
                Name = request.Name,
                Description = request.Description,
                PricePerKg = request.PricePerKg,
                ShopID = request.ShopId
            };
            _repository.Add(shopServiceEntity);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return shopServiceEntity.ID;
        }
    }
}
