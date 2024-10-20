using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.ShopService
{
    public class ShopServiceDto : IMapFrom<ShopServiceEntity>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal MinLaundryWeight { get; set; }
        public string ShopID { get; set; }
        public string ShopName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShopServiceEntity, ShopServiceDto>();
        }
    }

}
