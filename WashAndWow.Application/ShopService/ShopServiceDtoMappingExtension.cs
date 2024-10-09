using AutoMapper;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.ShopService
{
    public static class ShopServiceDtoMappingExtension
    {
        public static ShopServiceDto MapToShopServiceDto(this ShopServiceEntity projectFrom, IMapper mapper)
            => mapper.Map<ShopServiceDto>(projectFrom);

        public static List<ShopServiceDto> MapToShopServiceDtoList(this IEnumerable<ShopServiceEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToShopServiceDto(mapper)).ToList();

        public static ShopServiceDto MapToShopServiceDto(this ShopServiceEntity projectFrom, IMapper mapper, string shopName)
        {
            var dto = mapper.Map<ShopServiceDto>(projectFrom);
            dto.ShopName = shopName;
            return dto;
        }

        public static List<ShopServiceDto> MapToShopServiceDtoList(this IEnumerable<ShopServiceEntity> projectFrom, IMapper mapper,
            Dictionary<string, string> shopNames)
        => projectFrom.Select(x => x.MapToShopServiceDto(mapper,
            shopNames.ContainsKey(x.ShopID) ? shopNames[x.ShopID] : "Lỗi")).ToList();
    }
}
