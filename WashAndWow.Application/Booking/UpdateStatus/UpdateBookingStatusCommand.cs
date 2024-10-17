using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Booking.UpdateStatus
{
    public class UpdateBookingStatusCommand : IRequest<string>, ICommand
    {
        public string BookingId { get; set; }
        public object NewStatus { get; set; }

        public UpdateBookingStatusCommand() { }

        public UpdateBookingStatusCommand(string bookingId, object newStatus)
        {
            BookingId = bookingId;
            NewStatus = newStatus;
        }
    }
}
