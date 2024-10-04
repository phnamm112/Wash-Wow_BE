using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Form.ApprovalForm
{
    public class ApprovalFormCommand : IRequest<string>, ICommand
    {
        public ApprovalFormCommand()
        {

        }
        public string FormID { get; set; }
        public FormStatus Status { get; set; }
    }
}
