using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
