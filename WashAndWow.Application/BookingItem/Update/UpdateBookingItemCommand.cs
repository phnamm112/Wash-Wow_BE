using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.BookingItem.Update
{
    public class UpdateBookingItemCommand : IRequest<BookingItemDto>, ICommand
    {
        public string Id { get; set; }
        public string ServiceId { get; set; }

        public UpdateBookingItemCommand()
        {
        }
    }
}
