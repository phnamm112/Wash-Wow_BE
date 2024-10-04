using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Update
{
    public class UpdateShopServiceCommandHandler : IRequestHandler<UpdateShopServiceCommand, ShopServiceDto>
    {
        private readonly IShopServiceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateShopServiceCommandHandler(
            IShopServiceRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ShopServiceDto?> Handle(UpdateShopServiceCommand request, CancellationToken cancellationToken)
        {
            var shopService = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);

            if (shopService == null)
                return null;

            // Update properties
            shopService.Name = request.Name;
            shopService.Description = request.Description;
            shopService.PricePerKg = request.PricePerKg;
            shopService.ShopID = request.ShopID;

            await _unitOfWork.SaveChangesAsync(cancellationToken); // Save changes

            return _mapper.Map<ShopServiceDto>(shopService);
        }
    }
}
