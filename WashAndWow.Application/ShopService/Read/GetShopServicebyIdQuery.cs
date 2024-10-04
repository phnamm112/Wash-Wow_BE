using MediatR;

namespace WashAndWow.Application.ShopService.Read
{
    public class GetShopServiceByIdQuery : IRequest<ShopServiceDto>
    {
        public string Id { get; }

        public GetShopServiceByIdQuery(string id)
        {
            Id = id;
        }
    }

}
