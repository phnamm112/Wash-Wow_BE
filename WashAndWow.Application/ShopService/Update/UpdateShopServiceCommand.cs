using MediatR;
namespace WashAndWow.Application.ShopService.Update
{
    public class UpdateShopServiceCommand : IRequest<ShopServiceDto>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerKg { get; set; }
        public string ShopID { get; set; }

    }
}
