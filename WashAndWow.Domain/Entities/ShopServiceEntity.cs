using System.ComponentModel.DataAnnotations.Schema;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Entities.Base;

namespace WashAndWow.Domain.Entities
{
    [Table("ShopService")]
    public class ShopServiceEntity : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal PricePerKg { get; set; }
        public required string ShopID { get; set; }
        [ForeignKey(nameof(ShopID))]
        public virtual LaundryShopEntity LaundryShop { get; set; }

        public virtual ICollection<BookingItemEntity> BookingItems { get; set; } // List các item đã chọn service
    }
}
