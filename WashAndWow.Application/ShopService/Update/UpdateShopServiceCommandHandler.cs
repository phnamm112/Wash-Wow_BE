using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Update
{
    public class UpdateShopServiceCommandHandler : IRequestHandler<UpdateShopServiceCommand, ShopServiceDto>
    {
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateShopServiceCommandHandler(
            IShopServiceRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _shopServiceRepository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<ShopServiceDto?> Handle(UpdateShopServiceCommand request, CancellationToken cancellationToken)
        {
            // Fetch the existing shop service entity
            var shopService = await _shopServiceRepository.FindAsync(x=> x.ID.Equals(request.Id), cancellationToken);
            if (shopService == null)
            {
                throw new NotFoundException($"Service with ID {request.Id} not found");
            }

            // Ensure the current user is the owner of the shop
            var currentUserId = _currentUserService.UserId;
            if (shopService.LaundryShop.OwnerID != currentUserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to update this service");
            }

            // Check if a service with the updated name already exists in the shop
            var existingService = await _shopServiceRepository.FindAsync(s => s.Name == request.Name && s.ShopID == request.ShopID && s.ID != request.Id);
            if (existingService != null)
            {
                throw new Exception($"Service with name {request.Name} already exists in shop {request.ShopID}");
            }

            // Update the entity with new values
            shopService.Name = request.Name;
            shopService.Description = request.Description;
            shopService.PricePerKg = request.PricePerKg;
            shopService.UpdaterID = currentUserId;
            shopService.LastestUpdateAt = DateTime.UtcNow;  


            await _unitOfWork.SaveChangesAsync(cancellationToken); // Save changes

            return _mapper.Map<ShopServiceDto>(shopService);
        }
    }
}
