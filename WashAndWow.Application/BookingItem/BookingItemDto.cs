using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.BookingItem
{
    public class BookingItemDto : IMapFrom<BookingItemDto>
    {
        public string ID { get; set; }
        public string ServicesId { get; set; }
        public string BookingId { get; set; }

        public BookingItemDto() { }

        public BookingItemDto(string id, string servicesId, string bookingId)
        {
            ID = id;
            ServicesId = servicesId;
            BookingId = bookingId;
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookingItemEntity, BookingItemDto>();
        }
    }

}
