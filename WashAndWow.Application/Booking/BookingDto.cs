using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using Wash_Wow.Domain.Entities;
using WashAndWow.Application.BookingItem;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Booking
{
    public class BookingDto : IMapFrom<BookingEntity>
    {
        public string ID { get; set; }
        public float LaundryWeight { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Note { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime ShopPickupTime { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string LaundryShopID { get; set; }
        public string LaundryShopName { get; set; }
        public int PaymentID {  get; set; }
        public string VoucherId { get; set; }
        public List<BookingItemDto>? BookingItems { get; set; }

        public BookingDto(string id
            , float laundryWeight
            , decimal totalPrice
            , string? note
            , BookingStatus status
            , DateTime shopPickupTime
            , string customerId
            , string laundryShopId
            , string voucherId
            , int paymentId
            , List<BookingItemDto>? bookingItems)
        {
            ID = id;
            LaundryWeight = laundryWeight;
            TotalPrice = totalPrice;
            Note = note;
            Status = status;
            ShopPickupTime = shopPickupTime;
            CustomerID = customerId;
            LaundryShopID = laundryShopId;
            VoucherId = voucherId;
            PaymentID = paymentId;
            BookingItems = bookingItems;
        }
        public BookingDto()
        {

        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookingEntity, BookingDto>();
        }
    }
}
