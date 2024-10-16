﻿using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace Wash_Wow.Application.Users.Register
{
    public class RegisterCommand : IRequest<string>, ICommand
    {
        public RegisterCommand(string email, string password, string fullName, string phoneNumber, string address)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Address = address;
        }
        public RegisterCommand()
        {

        }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
