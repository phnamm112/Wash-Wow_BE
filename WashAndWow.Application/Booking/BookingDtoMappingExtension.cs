using AutoMapper;
using Wash_Wow.Domain.Entities;

namespace WashAndWow.Application.Booking
{
    public static class BookingDtoMappingExtension
    {
        public static BookingDto MapToBookingDto(this BookingEntity projectFrom, IMapper mapper)
            => mapper.Map<BookingDto>(projectFrom);
        public static List<BookingDto> MapToBookingDtoList(this IEnumerable<BookingEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToBookingDto(mapper)).ToList();

        public static BookingDto MapToBookingDto(this BookingEntity entity, IMapper mapper, string customerName, string laundryShopName, int paymentId)
        {
            var dto = mapper.Map<BookingDto>(entity);
            dto.CustomerName = customerName;
            dto.LaundryShopName = laundryShopName;
            dto.PaymentID = paymentId;
            return dto;
        }

        public static List<BookingDto> MapToBookingDtoList(this IEnumerable<BookingEntity> projectFrom, IMapper mapper, 
            Dictionary<string, string> customers, Dictionary<string, string> laundryShops, Dictionary<string, int> payments)
             => projectFrom.Select(x => x.MapToBookingDto(mapper,
             customers.ContainsKey(x.CreatorID) ? customers[x.CreatorID] : "Lỗi",
             laundryShops.ContainsKey(x.LaundryShopID) ? laundryShops[x.LaundryShopID] : "Lỗi",
             payments.TryGetValue(x.ID, out var paymentId) ? payments[x.ID] : 0
                    )).ToList();
    }
}
