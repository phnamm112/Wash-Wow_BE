using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Users.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, string>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        public AssignRoleCommandHandler(ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(AssignRoleCommand command, CancellationToken cancellationToken)
        {
            var existUser = await _userRepository.FindAsync(x => x.ID == command.UserID && x.DeletedAt == null, cancellationToken);
            if (existUser == null)
            {
                throw new NotFoundException("UserID is not exist");
            }
            if (existUser.Status.Equals(UserStatus.BANNED) || existUser.Status.Equals(UserStatus.UNVERIFY))
            {
                return "Unverify user or being banned";
            }
            if (!Enum.IsDefined(typeof(Role), command.Role))
            {
                throw new NotFoundException("Role is not exist");
            }

            existUser.Role = command.Role;
            existUser.UpdaterID = _currentUserService.UserId;
            existUser.LastestUpdateAt = DateTime.UtcNow;
            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success" : "Update Fail";
        }
    }
}
