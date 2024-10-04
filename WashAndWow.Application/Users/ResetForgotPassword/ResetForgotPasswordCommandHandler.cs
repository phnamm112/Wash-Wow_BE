using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Users.ChangePassword
{
    public class ResetForgotPasswordCommandHandler : IRequestHandler<ResetForgotPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmailVerifyRepository _emailVerifyRepository;
        public ResetForgotPasswordCommandHandler(IUserRepository userRepository
            , ICurrentUserService currentUserService
            , IEmailVerifyRepository emailVerifyRepository)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _emailVerifyRepository = emailVerifyRepository;
        }
        public async Task<string> Handle(ResetForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // TODO: handle logic
            var validToken = await _emailVerifyRepository.FindAsync(x => x.Token == request.Token && x.ExpireTime > DateTime.UtcNow, cancellationToken);
            if (validToken == null)
            {
                return "Token is not valid";
            }
            var user = await _userRepository.FindAsync(x => x.ID == validToken.UserID && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User is not exist");
            }
            user.PasswordHash = _userRepository.HashPassword(request.NewPassword);
            _emailVerifyRepository.Remove(validToken);
            _userRepository.Update(user);

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failed";
        }
    }
}
