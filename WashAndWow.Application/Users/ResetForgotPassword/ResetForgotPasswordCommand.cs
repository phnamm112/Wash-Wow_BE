using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Users.ChangePassword
{
    public class ResetForgotPasswordCommand : IRequest<string>, ICommand
    {
        public ResetForgotPasswordCommand()
        {

        }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public ResetForgotPasswordCommand(string token, string newPassword)
        {
            Token = token;
            NewPassword = newPassword;
        }
    }
}
