using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Common.Interfaces;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Update
{
    public class UpdateLaundryShopCommandHandler : IRequestHandler<UpdateLaundryShopCommand, LaundryShopDto>
    {
        private readonly ILaundryShopRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLaundryShopCommandHandler(
            ILaundryShopRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<LaundryShopDto?> Handle(UpdateLaundryShopCommand request, CancellationToken cancellationToken)
        {
            var shop = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);

            if (shop == null)
                return null;

            // Update properties
            shop.Name = request.Name;
            shop.Address = request.Address;
            shop.PhoneContact = request.PhoneContact;
            shop.TotalMachines = request.TotalMachines;
            shop.Wallet = request.Wallet;
            shop.Status = request.Status;
            shop.OpeningHour = TimeSpan.ParseExact(request.OpeningHour, @"hh\:mm", CultureInfo.InvariantCulture);
            shop.ClosingHour = TimeSpan.ParseExact(request.ClosingHour, @"hh\:mm", CultureInfo.InvariantCulture);
            shop.OwnerID = request.OwnerID;

            await _unitOfWork.SaveChangesAsync(cancellationToken); // Save changes

            return _mapper.Map<LaundryShopDto>(shop);
        }
    }
}

