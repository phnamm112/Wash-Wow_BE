using MediatR;
using System.Globalization;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Create
{
    public class CreateLaundryShopCommandHandler : IRequestHandler<CreateLaundryShopCommand, string>
    {
        private readonly ILaundryShopRepository _shopRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public CreateLaundryShopCommandHandler(
            ILaundryShopRepository shopRepository,
            IUserRepository userRepository,
            ICurrentUserService currentUserService)
        {
            _shopRepository = shopRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateLaundryShopCommand request, CancellationToken cancellationToken)
        {
            //User validation
            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            // Check if shop with the same name, address, or phone already exists across the system
            var existingShop = await _shopRepository.FindAsync(x =>
                (x.Name == request.Name || x.Address == request.Address || x.PhoneContact == request.PhoneContact)
                && x.DeletedAt == null, cancellationToken);

            if (existingShop != null)
            {
                throw new DuplicationException("A laundry shop with the same name, address, or phone contact already exists.");
            }
            var laundryShop = new LaundryShopEntity
            {
                Name = request.Name,
                Address = request.Address,
                PhoneContact = request.PhoneContact,
                TotalMachines = request.TotalMachines,
                Wallet = request.Wallet,
                Status = request.Status,
                OpeningHour = TimeSpan.ParseExact(request.OpeningHour, @"hh\:mm", CultureInfo.InvariantCulture),
                ClosingHour = TimeSpan.ParseExact(request.ClosingHour, @"hh\:mm", CultureInfo.InvariantCulture),
                OwnerID = request.OwnerID
            };

            _shopRepository.Add(laundryShop);
            await _shopRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return laundryShop.ID;
        }
    }

}
