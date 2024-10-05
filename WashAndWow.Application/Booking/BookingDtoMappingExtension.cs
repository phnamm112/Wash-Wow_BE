using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using WashAndWow.Application.Form;
using WashAndWow.Application.FormTemplate;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Entities.ConfigTable;

namespace WashAndWow.Application.Booking
{
    public static class BookingDtoMappingExtension
    {
        public static BookingDto MapToBookingDto(this BookingEntity projectFrom, IMapper mapper)
            => mapper.Map<BookingDto>(projectFrom);
        public static List<BookingDto> MapToBookingDtoList(this IEnumerable<BookingEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToBookingDto(mapper)).ToList();

        public static BookingDto MapToBookingDto(this BookingEntity entity, IMapper mapper, string customerName, string laundryShopName)
        {
            var dto = mapper.Map<BookingDto>(entity);
            dto.CustomerName = customerName;
            dto.LaundryShopName = laundryShopName;
            return dto;
        }

        public static List<BookingDto> MapToBookingDtoList(this IEnumerable<BookingEntity> projectFrom, IMapper mapper, Dictionary<string, string> customers, Dictionary<string, string> laundryShops)
             => projectFrom.Select(x => x.MapToBookingDto(mapper,
             customers.ContainsKey(x.CreatorID) ? customers[x.CreatorID] : "Lỗi",
             laundryShops.ContainsKey(x.LaundryShopID) ? laundryShops[x.LaundryShopID] : "Lỗi"
                    )).ToList();
    }
}
