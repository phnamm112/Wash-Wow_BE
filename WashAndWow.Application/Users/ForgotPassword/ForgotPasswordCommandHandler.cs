using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Users.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerifyRepository _emailVerifyRepository;       
        public ForgotPasswordCommandHandler(IUserRepository userRepository
            , IEmailVerifyRepository emailVerifyRepository)
        {
            _userRepository = userRepository;
            _emailVerifyRepository = emailVerifyRepository;
        }
        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.Email == request.Email && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("Email is not existed");
            }

            // tạo token để dùng mail service gửi đi
            var token = GenerateToken();
            var EmailVerify = new EmailVerification
            {
                UserID = user.ID,
                Token = token,
                ExpireTime = DateTime.UtcNow.AddHours(1)
            };
            _emailVerifyRepository.Add(EmailVerify);

            await _emailVerifyRepository.SendTokenResetPassword(user.Email, token);

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success. Please check your mail" : "Failed";
            
        }

        private string GenerateToken(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var token = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return token;
        }
    }
}
