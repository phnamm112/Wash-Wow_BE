using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Users.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<string>, ICommand
    {
        public ForgotPasswordCommand()
        {

        }
        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }
        public string Email { get; set; }
    }
}
