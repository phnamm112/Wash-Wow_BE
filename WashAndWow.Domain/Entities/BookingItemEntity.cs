using System.ComponentModel.DataAnnotations.Schema;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Entities.Base;

namespace WashAndWow.Domain.Entities
{
    [Table("BookingItem")]
    public class BookingItemEntity : BaseEntity
    {
        public required decimal Amount { get; set; }
        public required string ServicesID { get; set; }
        [ForeignKey(nameof(ServicesID))]
        public virtual ShopServiceEntity ShopService { get; set; }
        public required string BookingID { get; set; }
        [ForeignKey(nameof(BookingID))]
        public virtual BookingEntity Booking { get; set; }
    }
}
