using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Application.Common.Pagination;

namespace WashAndWow.Application.Voucher.Read
{
    public class GetAllVoucherQuery : IRequest<PagedResult<VoucherDto>>, IQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllVoucherQuery()
        {

        }
        public GetAllVoucherQuery(int no, int pageSize)
        {
            PageNumber = no;
            PageSize = pageSize;
        }
    }
}
