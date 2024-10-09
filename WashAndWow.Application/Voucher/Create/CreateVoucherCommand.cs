using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Voucher.Create
{
    public class CreateVoucherCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
        public VoucherType Type { get; set; }
        public decimal? MaximumReduce { get; set; }
        public decimal? MinimumReduce { get; set; }
        public decimal Amount { get; set; }
        public decimal ConditionOfUse { get; set; }

        public CreateVoucherCommand()
        {

        }
        public CreateVoucherCommand(string name, string imgUrl, DateTime expiryDate, VoucherType type, decimal? maximumReduce, decimal? minimumReduce, decimal amount, decimal conditionOfUse)
        {
            Name = name;
            ImgUrl = imgUrl;
            ExpiryDate = expiryDate;
            Type = type;
            MaximumReduce = maximumReduce;
            MinimumReduce = minimumReduce;
            Amount = amount;
            ConditionOfUse = conditionOfUse;
        }
    }
}
