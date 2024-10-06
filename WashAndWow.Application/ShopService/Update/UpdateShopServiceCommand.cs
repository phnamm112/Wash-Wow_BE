using MediatR;
using Wash_Wow.Application.Common.Interfaces;
namespace WashAndWow.Application.ShopService.Update
{
    public class UpdateShopServiceCommand : IRequest<ShopServiceDto>, ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerKg { get; set; }
        public string ShopID { get; set; }
        public UpdateShopServiceCommand()
        {
            
        }
    }
}
