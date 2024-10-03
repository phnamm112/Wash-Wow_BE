using MediatR;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Voucher.Update
{
    public class UpdateVoucherCommand : IRequest<VoucherDto>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
        public VoucherType Type { get; set; }
        public decimal? MaximumReduce { get; set; }
        public decimal? MinimumReduce { get; set; }
        public decimal Amount { get; set; }
        public decimal ConditionOfUse { get; set; }
    }
}
