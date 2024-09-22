using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities.Base;
using WashAndWow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace Wash_Wow.Domain.Entities
{
    [Table("LaundryShop")]
    public class LaundryShopEntity : BaseEntity
    {
        public required string Address { get; set; }
        public required string Name { get; set; }
        [MaxLength(11)]
        public required string PhoneContact { get; set; }
        public required int TotalMachines {  get; set; }
        public required decimal Wallet {  get; set; }
        public required LaundryShopStatus Status { get; set; }
        public required TimeSpan OpeningHour { get; set; }
        public required TimeSpan ClosingHour {  get; set; }
        public required string OwnerID { get; set; }
        [ForeignKey(nameof(OwnerID))]
        public virtual UserEntity Owner { get; set; }
        public virtual ICollection<BookingEntity> Bookings { get; set; }
        public virtual ICollection<ShopServiceEntity> Services { get; set; }
    }
}
