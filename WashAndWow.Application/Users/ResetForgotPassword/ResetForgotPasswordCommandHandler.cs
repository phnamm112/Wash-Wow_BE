using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.Users.ChangePassword
{
    public class ResetForgotPasswordCommandHandler : IRequestHandler<ResetForgotPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        public ResetForgotPasswordCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }
        public async Task<string> Handle(ResetForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // TODO: handle logic

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failed";
        }
    }
}
