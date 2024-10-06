using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Voucher.Read
{
    public class GetVoucherByIdQuery : IRequest<VoucherDto>, IQuery
    {
        public string Id { get; }

        public GetVoucherByIdQuery(string id)
        {
            Id = id;
        }
    }
}
