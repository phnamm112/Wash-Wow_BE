using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using Wash_Wow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.LaundryShops
{
    public class LaundryShopDto : IMapFrom<LaundryShopEntity>
    {
        public string ID { get; set; }
        public string OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string PhoneContact { get; set; }
        public int TotalMachines { get; set; }
        public decimal Wallet { get; set; }
        public string Status { get; set; }
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LaundryShopEntity,LaundryShopDto>();
        }
    }

}
