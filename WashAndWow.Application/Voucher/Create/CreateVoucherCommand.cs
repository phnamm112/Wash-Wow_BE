using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Voucher.Create
{
    public class CreateVoucherCommand : IRequest<string>, ICommand
    {
        public VoucherDto Voucher { get; }

        public CreateVoucherCommand(VoucherDto voucher)
        {
            Voucher = voucher;
        }
    }
}
