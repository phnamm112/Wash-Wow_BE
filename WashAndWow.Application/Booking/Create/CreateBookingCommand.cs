using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using WashAndWow.Application.BookingItem.Create;

namespace WashAndWow.Application.Booking.Create
{
    public class CreateBookingCommand : IRequest<string>, ICommand
    {
        public float LaundryWeight { get; set; }
        public string? Note { get; set; }
        public DateTime ShopPickupTime { get; set; }
        public string LaundryShopId { get; set; }
        public string? VoucherId { get; set; }
        public List<CreateBookingItemCommand>? BookingItems { get; set; } = new();

    }
}
