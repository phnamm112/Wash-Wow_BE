using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Common.Interfaces;
using Wash_Wow.Domain.Enums;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Delete
{
    public class DeleteLaundryShopCommandHandler : IRequestHandler<DeleteLaundryShopCommand, bool>
    {
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public DeleteLaundryShopCommandHandler(
            ILaundryShopRepository laundryShopRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IUserRepository userRepository)
        {
            _laundryShopRepository = laundryShopRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteLaundryShopCommand request, CancellationToken cancellationToken)
        {
            // User validation
            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            // Retrieve the laundry shop to be deleted
            var laundryShop = await _laundryShopRepository.FindAsync(ls => ls.ID == request.Id && ls.DeletedAt == null, cancellationToken);
            if (laundryShop == null)
            {
                throw new NotFoundException($"Laundry shop with ID: {request.Id} not found");
            }

            // Check if the user is the owner of the shop or an admin
            if (laundryShop.OwnerID != user.ID && user.Role != Enums.Role.Admin)
            {
                throw new UnauthorizedAccessException("You are not allowed to delete this laundry shop");
            }

            // Mark the shop as deleted
            laundryShop.DeleterID = user.ID;
            laundryShop.DeletedAt = DateTime.Now;

            // Update the repository
            _laundryShopRepository.Update(laundryShop);
            await _unitOfWork.SaveChangesAsync(cancellationToken); // Save changes

            return true; // Deletion successful
        }
    }
}
