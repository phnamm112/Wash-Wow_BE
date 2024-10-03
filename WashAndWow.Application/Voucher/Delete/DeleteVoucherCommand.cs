using MediatR;

namespace WashAndWow.Application.Voucher.Delete
{
    public class DeleteVoucherCommand : IRequest<bool>
    {
        public string Id { get; }

        public DeleteVoucherCommand(string id)
        {
            Id = id;
        }
    }
}
