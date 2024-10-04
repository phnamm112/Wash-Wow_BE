using AutoMapper;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.BookingItem
{
    public class BookingItemProfile : Profile
    {
        public BookingItemProfile()
        {
            CreateMap<BookingItemEntity, BookingItemDto>();
        }
    }
}
