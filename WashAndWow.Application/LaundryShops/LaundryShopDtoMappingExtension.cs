using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using WashAndWow.Application.Form;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.LaundryShops
{
    public static class LaundryShopDtoMappingExtension
    {
        public static LaundryShopDto MapToLaundryShopDto(this LaundryShopEntity projectFrom, IMapper mapper)
            => mapper.Map<LaundryShopDto>(projectFrom);
        public static List<LaundryShopDto> MapToLaundryShopDtoList(this IEnumerable<LaundryShopEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToLaundryShopDto(mapper)).ToList();

        public static LaundryShopDto MapToLaundryShopDto(this LaundryShopEntity projectFrom, IMapper mapper, string ownerName)
        {
            var dto = mapper.Map<LaundryShopDto>(projectFrom);
            dto.OwnerName = ownerName;
            dto.Status = projectFrom.Status.ToString();
            return dto;
        }
        public static List<LaundryShopDto> MapToLaundryShopDtoList(this IEnumerable<LaundryShopEntity> projectFrom, IMapper mapper
            , Dictionary<string, string> ownerNames)
        => projectFrom.Select(x => x.MapToLaundryShopDto(mapper,
             ownerNames.ContainsKey(x.OwnerID) ? ownerNames[x.OwnerID] : "Lỗi"
                    )).ToList();
    }
}
