using MediatR;
using System.Globalization;
using Wash_Wow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Create
{
    public class CreateLaundryShopCommandHandler : IRequestHandler<CreateLaundryShopCommand, string>
    {
        private readonly ILaundryShopRepository _shopRepository;

        public CreateLaundryShopCommandHandler(ILaundryShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<string> Handle(CreateLaundryShopCommand request, CancellationToken cancellationToken)
        {
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
