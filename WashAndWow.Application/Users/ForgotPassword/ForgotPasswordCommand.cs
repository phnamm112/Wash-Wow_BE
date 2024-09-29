using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
