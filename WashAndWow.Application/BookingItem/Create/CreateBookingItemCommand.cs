using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.BookingItem.Create
{
    public class CreateBookingItemCommand : IRequest<string>, ICommand
    {
        public string ServicesId { get; set; }
    }
}
