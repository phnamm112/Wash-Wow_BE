using AutoMapper;
using Wash_Wow.Domain.Entities;
using WashAndWow.Application.Booking.Create;
using WashAndWow.Application.Booking.Update;

namespace WashAndWow.Application.Booking
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<CreateBookingCommand, BookingEntity>();
            CreateMap<UpdateBookingCommand, BookingEntity>();
            CreateMap<BookingEntity, BookingDto>();
        }
    }
}
