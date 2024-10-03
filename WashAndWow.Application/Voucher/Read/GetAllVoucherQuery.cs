using MediatR;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Read
{
    public class GetAllVoucherQuery : IRequest<IPagedResult<VoucherDto>>
    {
        public int PageNo { get; } = 1;
        public int PageSize { get; } = 10;
        public GetAllVoucherQuery()
        {
        }

        public GetAllVoucherQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
