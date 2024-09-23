using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Entities.Base;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Domain.Entities
{
    [Table("Voucher")]
    public class VoucherEntity : BaseEntity
    {
        public required string Name {  get; set; }
        public required string ImgUrl {  get; set; }
        public required DateTime ExpiryDate {  get; set; }
        public required VoucherType Type {  get; set; }
        public decimal? MaximumReduce {  get; set; }
        public decimal? MinimumReduce {  get; set; }
        public required decimal Amount {  get; set; }
        public required decimal ConditionOfUse {  get; set; }

        [ForeignKey(nameof(CreatorID))]
        public virtual UserEntity Creator { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }  //List user đã sử dụng
        public virtual ICollection<BookingEntity> Bookings { get; set; }  //List booking sử dụng
    }
}
