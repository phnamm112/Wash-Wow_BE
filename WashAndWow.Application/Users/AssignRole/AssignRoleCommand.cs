using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Users.AssignRole
{
    public class AssignRoleCommand : IRequest<string>, ICommand
    {
        public AssignRoleCommand()
        {

        }
        public AssignRoleCommand(string userID, Role role)
        {
            Role = role;
            UserID = userID;
        }
        public string UserID { get; set; }
        public Role Role { get; set; }
    }
}
