using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities.Base;
using WashAndWow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace Wash_Wow.Domain.Entities
{
    [Table("Booking")]
    public class BookingEntity : BaseEntity
    {
        public required float LaundryWeight { get; set; }
        public required decimal TotalPrice {  get; set; }
        public string? Note { get; set; }
        public required BookingStatus Status { get; set; }
        public required DateTime ShopPickupTime { get; set; }

       
        [ForeignKey(nameof(CreatorID))]  // Booking luôn là customer
        public virtual UserEntity Customer { get; set; }

        public required string LaundryShopID {  get; set; }
        [ForeignKey(nameof(LaundryShopID))]
        public virtual LaundryShopEntity LaundryShop { get; set; }

        public required string VoucherID {  get; set; }  // 2 case: creator là admin/shop owner
        [ForeignKey(nameof(VoucherID))]
        public virtual VoucherEntity Voucher { get; set; }

        public virtual ICollection<BookingItemEntity> BookingItems { get; set; }
    }
}
