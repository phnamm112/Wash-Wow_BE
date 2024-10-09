using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.ShopService.Read
{
    public class GetShopServiceByIdQuery : IRequest<ShopServiceDto>, IQuery
    {
        public string Id { get; }

        public GetShopServiceByIdQuery(string id)
        {
            Id = id;
        }
        public GetShopServiceByIdQuery()
        {

        }
    }

}
