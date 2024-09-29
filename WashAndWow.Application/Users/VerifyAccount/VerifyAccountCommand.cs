using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Users.VerifyAccount
{
    public class VerifyAccountCommand : IRequest<string>, ICommand
    {
        public VerifyAccountCommand(string token)
        {

            Token = token;
        }
        public string Token {  get; set; }
    }
}
