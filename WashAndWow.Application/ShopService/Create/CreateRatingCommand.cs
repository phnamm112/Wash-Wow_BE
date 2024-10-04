using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.ShopService.Create
{
    public class CreateRatingCommand : IRequest<string>, ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public decimal PricePerKg { get; }
        public string ShopId { get; }
    }
}
