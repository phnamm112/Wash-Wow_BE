using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Delete
{
    public class DeleteShopServiceCommandHandler : IRequestHandler<DeleteShopServiceCommand, bool>
    {
        private readonly IShopServiceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteShopServiceCommandHandler(IShopServiceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteShopServiceCommand request, CancellationToken cancellationToken)
        {
            var shopService = await _repository.FindAsync(ss => ss.ID.Equals(request.Id));
            if (shopService == null)
            {
                return false; // Service not found
            }

            _repository.Remove(shopService);
            await _unitOfWork.SaveChangesAsync(cancellationToken);// save change 
            return true; // Deletion successful
        }
    }
}
