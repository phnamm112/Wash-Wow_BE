using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Users.VerifyAccount
{
    public class VerifyAccountCommand : IRequest<string>, ICommand
    {
        public VerifyAccountCommand(string token)
        {

            Token = token;
        }
        public VerifyAccountCommand()
        {

        }
        public string Token { get; set; }
    }
}
