using MediatR;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Users.VerifyAccount
{
    public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, string>
    {
        private readonly IEmailVerifyRepository _emailVerifyRepository;
        private readonly IUserRepository _userRepository;
        public VerifyAccountCommandHandler(IEmailVerifyRepository emailVerifyRepository, IUserRepository userRepository)
        {
            _emailVerifyRepository = emailVerifyRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            var validToken = await _emailVerifyRepository.FindAsync(x => x.Token == request.Token && x.ExpireTime > DateTime.UtcNow, cancellationToken);
            if (validToken == null)
            {
                return ("Token is not valid");
            }
            var user = await _userRepository.FindAsync(x => x.ID == validToken.UserID && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User is not exist");
            }
            user.Status = UserStatus.VERIFIED;
            _emailVerifyRepository.Remove(validToken);
            _userRepository.Update(user);
            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failed";
        }
    }
}
