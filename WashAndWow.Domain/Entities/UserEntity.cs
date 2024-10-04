using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wash_Wow.Domain.Entities.Base;
using WashAndWow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace Wash_Wow.Domain.Entities
{
    [Table("User")]
    public class UserEntity : BaseEntity
    {
        public required string PasswordHash { get; set; }
        public required string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Range(1, 50, ErrorMessage = "Email Address cannot be over 50 characters!")]
        public required string Email { get; set; }
        [MaxLength(11)]
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required Role Role { get; set; }
        public required UserStatus Status { get; set; }

        public virtual ICollection<BookingEntity> Bookings { get; set; }
        public virtual ICollection<LaundryShopEntity> LaundryShops { get; set; }

        public virtual ICollection<RatingEntity> Ratings { get; set; }   // List rating đã tạo
        public virtual ICollection<VoucherEntity> CreatedVouchers { get; set; }   // List voucher đã tạo
        public virtual ICollection<VoucherEntity> UsedVouchers { get; set; }     // List voucher đã sử dụng
        public virtual ICollection<FormEntity> SentForms { get; set; }  // ListForm đã gửi
    }
}
