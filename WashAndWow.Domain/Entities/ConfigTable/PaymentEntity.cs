using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using WashAndWow.Domain.Entities.Base;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Domain.Entities.ConfigTable
{
    public class PaymentEntity : ConfiguredEntity
    {
        public required double Amount { get; set; }
        public required PaymentStatus Status { get; set; }
        public required string BookingID { get; set; }
        [ForeignKey(nameof(BookingID))]
        public virtual BookingEntity Booking { get; set; }
    }
}
