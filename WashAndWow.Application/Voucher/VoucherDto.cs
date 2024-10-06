using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using WashAndWow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Voucher
{
    public class VoucherDto : IMapFrom<VoucherEntity>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
        public VoucherType Type { get; set; }
        public decimal? MaximumReduce { get; set; }
        public decimal? MinimumReduce { get; set; }
        public decimal Amount { get; set; }
        public decimal ConditionOfUse { get; set; }
        public string CreatorID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VoucherEntity,VoucherDto>();
        }
    }

}
