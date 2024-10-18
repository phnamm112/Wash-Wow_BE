using System.ComponentModel.DataAnnotations.Schema;
using Wash_Wow.Domain.Entities.Base;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Entities.ConfigTable;
using static Wash_Wow.Domain.Enums.Enums;

namespace Wash_Wow.Domain.Entities
{
    [Table("Booking")]
    public class BookingEntity : BaseEntity
    {
        public required float LaundryWeight { get; set; }
        public required decimal TotalPrice { get; set; }
        public string? Note { get; set; }
        public required BookingStatus Status { get; set; }
        public required DateTime ShopPickupTime { get; set; }
        public required DateTime CustomerPickpupTime { get; set; }

        [ForeignKey(nameof(CreatorID))]  // Booking luôn là customer
        public virtual UserEntity Customer { get; set; }

        public required string LaundryShopID { get; set; }
        [ForeignKey(nameof(LaundryShopID))]
        public virtual LaundryShopEntity LaundryShop { get; set; }

        public decimal? VoucherDiscounted {  get; set; }
        public string? VoucherID { get; set; }  // 2 case: creator là admin/shop owner
        [ForeignKey(nameof(VoucherID))]
        public virtual VoucherEntity? Voucher { get; set; }
        public virtual ICollection<BookingItemEntity> BookingItems { get; set; }
    }
}
