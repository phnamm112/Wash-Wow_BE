using MediatR;

namespace WashAndWow.Application.Booking.Delete
{
    public class DeleteBookingCommand : IRequest<bool>
    {
        public string Id { get; }
        public DeleteBookingCommand(string id)
        {
            Id = id;
        }
    }
}
