using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.Users.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly ICurrentUserService _currrentUserService;
        private readonly IUserRepository _userRepository;
        public ForgotPasswordCommandHandler(ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _currrentUserService = currentUserService;
            _userRepository = userRepository;
        }
        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.AnyAsync(x => x.Email == request.Email && x.DeletedAt == null, cancellationToken);
            if (!user)
            {
                throw new NotFoundException("Email is not existed");
            }

            // tạo token để dùng mail service gửi đi
            var token = GenerateToken();

            return "Fixing";
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
