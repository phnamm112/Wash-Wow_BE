using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities.Base;
using static Wash_Wow.Domain.Enums.Enums;

namespace Wash_Wow.Domain.Entities
{
    [Table("Booking")]
    public class BookingEntity : BaseEntity
    {
        public required decimal LaundryWeight { get; set; }
        public string? Note { get; set; }
        public required BookingStatus Status { get; set; }
        public required DateTime ShopPickupTime { get; set; }
        public required string CustomerID {  get; set; } // get data sdt và tên người dùng, khỏi lưu field data chi cho mệt
        [ForeignKey(nameof(CustomerID))]
        public virtual UserEntity Customer { get; set; }
        public required string LaundryShopID {  get; set; }
        [ForeignKey(nameof(LaundryShopID))]
        public virtual LaundryShopEntity LaundryShop { get; set; }
    }
}
