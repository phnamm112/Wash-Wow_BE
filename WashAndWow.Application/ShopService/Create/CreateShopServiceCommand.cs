using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.ShopService.Create
{
    public class CreateShopServiceCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerKg { get; set; }
        public string ShopId { get; set; }
    }
}
