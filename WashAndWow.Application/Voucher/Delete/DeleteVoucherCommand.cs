using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Voucher.Delete
{
    public class DeleteVoucherCommand : IRequest<string>, ICommand
    {
        public string Id { get; }
        public DeleteVoucherCommand()
        {

        }
        public DeleteVoucherCommand(string id)
        {
            Id = id;
        }
    }
}
