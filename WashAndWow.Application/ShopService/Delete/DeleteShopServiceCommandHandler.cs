using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Common.Interfaces;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.ShopService.Delete
{
    public class DeleteShopServiceCommandHandler : IRequestHandler<DeleteShopServiceCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteShopServiceCommandHandler(IShopServiceRepository repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IUserRepository userRepository)
        {
            _shopServiceRepository = repository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteShopServiceCommand request, CancellationToken cancellationToken)
        {
            //User validation
            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var shopService = await _shopServiceRepository.FindAsync(ss => ss.ID.Equals(request.Id));
            if (shopService == null)
            {
                throw new NotFoundException($"Service with id: {request.Id} not found");
            }
            shopService.DeleterID = user.ID;
            shopService.DeletedAt = DateTime.Now;
            _shopServiceRepository.Update(shopService);
            await _unitOfWork.SaveChangesAsync(cancellationToken);// save change 
            return true; // Deletion successful
        }
    }
}
