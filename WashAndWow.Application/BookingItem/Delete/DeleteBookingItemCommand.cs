using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.BookingItem.Delete
{
    public class DeleteBookingItemCommand : IRequest<bool>, ICommand
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
