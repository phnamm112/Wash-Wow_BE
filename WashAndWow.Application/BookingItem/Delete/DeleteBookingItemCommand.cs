using MediatR;

namespace WashAndWow.Application.BookingItem.Delete
{
    public class DeleteBookingItemCommand : IRequest<bool>
    {
        public string Id { get; }

        public DeleteBookingItemCommand(string id)
        {
            Id = id;
        }
    }
}
