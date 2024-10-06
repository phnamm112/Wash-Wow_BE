using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.ShopService.Delete
{
    public class DeleteShopServiceCommand : IRequest<bool>, ICommand
    {
        public string Id { get; }

        public DeleteShopServiceCommand(string id)
        {
            Id = id;
        }
        public DeleteShopServiceCommand()
        {
            
        }
    }
}
