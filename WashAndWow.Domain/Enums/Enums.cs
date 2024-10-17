namespace Wash_Wow.Domain.Enums
{
    public class Enums
    {
        // USER ENUM
        public enum Role
        {
            Admin,
            Customer,
            ShopOwner
        }
        public enum UserStatus
        {
            UNVERIFY,
            VERIFIED,
            BANNED
        }

        // BOOKING ENUM
        public enum BookingStatus
        {
            PENDING,
            CONFIRMED,
            PROCESSING,
            COMPLETED,
            CANCELLED,
            REJECTED
        }

        //SHOP ENUM
        public enum LaundryShopStatus
        {
            OPEN,
            BUSY,
            CLOSE
        }

        //VOUCHER ENUM
        public enum VoucherType
        {
            DISCOUNT_BY_PERCENT,
            DISCOUNT_BY_AMOUNT
        }

        //FORM ENUM
        public enum FormStatus
        {
            PENDING,
            APPROVED,
            REJECTED
        }

        //PAYMENT ENUM
        public enum PaymentStatus
        {
            SUCCESS,
            FAILURE,
            PENDING,
            CANCEL
        }
        public enum NotificationType
        {
            BookingCreated,
            BookingCanceled,
            BookingCompleted,
            BookingRejected,
            BookingConfirmed,
            RatingCreated,


        }
    }
}
