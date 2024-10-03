using AutoMapper;
using Wash_Wow.Domain.Entities;

namespace WashAndWow.Application.LaundryShops
{
    public class LaundryShopProfile : Profile
    {
        public LaundryShopProfile()
        {
            CreateMap<LaundryShopEntity, LaundryShopDto>();
        }
    }

}
