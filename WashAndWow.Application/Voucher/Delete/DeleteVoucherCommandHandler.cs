using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Voucher.Delete
{
    public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, string>
    {
        private readonly IVoucherRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public DeleteVoucherCommandHandler(
            IVoucherRepository repository,
            IUserRepository userRepository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
        {
            var voucher = await _repository.FindAsync(x => x.ID.Equals(request.Id) && x.DeletedAt == null, cancellationToken);
            if (voucher == null)
            {
                throw new NotFoundException("Voucher is not existed");
            }
            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null || !user.Role.Equals(Role.Admin))
            {
                throw new UnauthorizedException("Method not allowed ! Unauthorized");
            }

            voucher.DeletedAt = DateTime.Now;
            voucher.DeleterID = _currentUserService.UserId;
            _repository.Update(voucher);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Deleted" : "Failed";

        }
    }
}
