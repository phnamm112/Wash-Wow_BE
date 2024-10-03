using MediatR;

namespace WashAndWow.Application.Voucher.Read
{
    public class GetVoucherByIdQuery : IRequest<VoucherDto>
    {
        public string Id { get; }

        public GetVoucherByIdQuery(string id)
        {
            Id = id;
        }
    }
}
