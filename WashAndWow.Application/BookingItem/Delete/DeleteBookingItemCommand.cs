using MediatR;

namespace WashAndWow.Application.BookingItem.Delete
{
    public class DeleteBookingItemCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteBookingItemCommand(string id)
        {
            Id = id;
        }

        public DeleteBookingItemCommand()
        {
        }
    }
}
