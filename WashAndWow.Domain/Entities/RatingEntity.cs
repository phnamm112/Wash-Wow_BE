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
    [Table("ShopRating")]
    public class RatingEntity : BaseEntity
    {
        public int RatingValue { get; set; }
        public string? Comment { get; set; }


        [ForeignKey(nameof(CreatorID))]  // Người tạo rating
        public virtual UserEntity User {  get; set; }

        public required string LaundryShopID {  get; set; }
        [ForeignKey(nameof(LaundryShopID))]
        public virtual LaundryShopEntity LaundryShop { get; set; }
    }
}
