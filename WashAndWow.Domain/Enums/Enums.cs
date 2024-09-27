using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wash_Wow.Domain.Enums
{
    public class Enums
    {
        // USER ENUM
        public enum Role
        {
            Admin,
            Customer,
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
            CANCELLED
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
    }
}
