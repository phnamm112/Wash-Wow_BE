using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WashAndWow.Application.Booking
{
    public class CreateBookingResponse
    {
        public string BookingId { get; set; }
        public int PaymentId { get; set; }
        public CreateBookingResponse()
        {
        }
        public CreateBookingResponse(string bookingId, int paymentId)
        {
            BookingId = bookingId;
            PaymentId = paymentId;
        }
    }
}
