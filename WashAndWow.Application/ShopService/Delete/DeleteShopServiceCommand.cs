using MediatR;

namespace WashAndWow.Application.ShopService.Delete
{
    public class DeleteShopServiceCommand : IRequest<bool>
    {
        public string Id { get; }

        public DeleteShopServiceCommand(string id)
        {
            Id = id;
        }
    }
}
