using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Enums;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Create
{
    public class CreateShopServiceCommandHandler : IRequestHandler<CreateShopServiceCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly ILaundryShopRepository _laundryShopRepository;
        public CreateShopServiceCommandHandler(
            IShopServiceRepository shopServiceRepository,
            ILaundryShopRepository laundryShopRepository,
            IUserRepository userRepository,
            ICurrentUserService currentUserService)
        {
            _shopServiceRepository = shopServiceRepository;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _laundryShopRepository = laundryShopRepository;
        }
        public async Task<string> Handle(CreateShopServiceCommand request, CancellationToken cancellationToken)
        {
            //User validation
            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            // LaundryShop validation
            var laundryShop = await _laundryShopRepository.FindAsync(x => x.ID == request.ShopId && x.DeletedAt == null, cancellationToken);
            if (laundryShop == null)
            {
                throw new NotFoundException("Laundry shop is not exist");
            }
            // Check if the current user is the owner of the shop or user is a admin
            if (laundryShop.OwnerID != user.ID && user.Role != Enums.Role.Admin)
            {
                throw new UnauthorizedAccessException("You are not allowed to create a service for this shop");
            }
            // check if service have duplicate name
            var existingService = await _shopServiceRepository.FindAsync(s => s.Name == request.Name && s.ShopID == request.ShopId, cancellationToken);
            if (existingService != null)
            {
                throw new DuplicationException($"Service with name {request.Name} already exists in shop {request.ShopId}");
            }
            var shopServiceEntity = new ShopServiceEntity
            {
                Name = request.Name,
                Description = request.Description,
                PricePerKg = request.PricePerKg,
                MinLaundryWeight = request.MinLaundryWeight,
                ShopID = request.ShopId,
                CreatedAt = DateTime.Now,
                CreatorID = user.ID,
            };
            _shopServiceRepository.Add(shopServiceEntity);
            await _shopServiceRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return shopServiceEntity.ID;
        }
    }
}
