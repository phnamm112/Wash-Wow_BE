using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Booking.Delete
{
    public class DeleteBookingCommand : IRequest<bool>, ICommand
    {
        public string Id { get; set; }
        public DeleteBookingCommand(string id)
        {
            Id = id;
        }
        public DeleteBookingCommand()
        {

        }
    }
}
