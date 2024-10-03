using MediatR;
using Microsoft.Extensions.Options;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace Wash_Wow.Application.Users.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerifyRepository _emailVerifyRepository;
        public RegisterCommandHandler(IUserRepository userRepository
            , IEmailVerifyRepository emailRepository
            , IOptions<MailSettings> emailSettings)
        {
            _userRepository = userRepository;
            _emailVerifyRepository = emailRepository;
        }
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _userRepository.AnyAsync(x => x.Email == request.Email && x.DeletedAt == null, cancellationToken);
            if (isExist)
            {
                throw new DuplicationException("Email đã được sử dụng");
            }
            isExist = await _userRepository.AnyAsync(x => x.PhoneNumber == request.PhoneNumber && x.DeletedAt == null, cancellationToken);
            if (isExist)
            {
                throw new DuplicationException("Số điện thoại đã được sử dụng");
            }
            var user = new UserEntity
            {
                Address = request.Address,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                PasswordHash = _userRepository.HashPassword(request.Password),
                Role = Role.Customer,
                //FIX CỨNG SAU NÀY DONE REDIRECT EMAIL THÌ SỬA LẠI
                Status = UserStatus.VERIFIED,
            };
            _userRepository.Add(user);
            // dùng token này để xác thực mail
            var token = Guid.NewGuid().ToString();
            var EmailVerify = new EmailVerification
            {
                UserID = user.ID,
                Token = token,
                ExpireTime = DateTime.UtcNow.AddHours(24)
            };
            _emailVerifyRepository.Add(EmailVerify);// Save the email verification entry

            string baseUrl = "http://example.com/verify-email";
            // Construct the confirmation URL
            var confirmationUrl = $"{baseUrl}?token={token}";

            // Send the confirmation email
            await _emailVerifyRepository.SendConfirmationEmailAsync(user.Email, confirmationUrl);
            // xử lý gửi mail - Nhân làm nhé
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return user.ID;
        }
    }
}
