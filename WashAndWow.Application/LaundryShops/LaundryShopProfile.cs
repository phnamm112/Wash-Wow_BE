using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
