using MediatR;
using WashAndWow.Application.BookingItem;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Booking.Update
{
    public class UpdateBookingCommand : IRequest<BookingDto>
    {
        public string Id { get; set; }
        public float LaundryWeight { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Note { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime ShopPickupTime { get; set; }
        public string CustomerId { get; set; }
        public string LaundryShopId { get; set; }
        public string VoucherId { get; set; }
        public List<BookingItemDto> BookingItems { get; set; } = new();
    }
}

