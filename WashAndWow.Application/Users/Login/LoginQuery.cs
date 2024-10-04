using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Entities;

namespace Wash_Wow.Application.Users.Login
{
    public class LoginQuery : IRequest<UserLoginDto>, IQuery
    {
        public LoginQuery()
        {

        }
        public LoginQuery(LoginEntity loginEntity)
        {
            user.Email = loginEntity.Email;
            user.Password = loginEntity.Password;
        }
        public required LoginEntity user { get; set; }
    }
}
