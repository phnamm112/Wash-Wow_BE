﻿using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Delete
{
    public class DeleteLaundryShopCommandHandler : IRequestHandler<DeleteLaundryShopCommand, bool>
    {
        private readonly ILaundryShopRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLaundryShopCommandHandler(
            ILaundryShopRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteLaundryShopCommand request, CancellationToken cancellationToken)
        {
            var shop = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);

            if (shop == null)
                return false;

            _repository.Remove(shop); // Assuming the Remove method is implemented
            await _unitOfWork.SaveChangesAsync(cancellationToken); // Save changes

            return true;
        }
    }

}
