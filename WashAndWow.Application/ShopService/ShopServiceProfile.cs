using AutoMapper;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.ShopService
{
    public class ShopServiceProfile : Profile
    {
        public ShopServiceProfile()
        {
            CreateMap<ShopServiceEntity, ShopServiceDto>();
        }
    }
}
