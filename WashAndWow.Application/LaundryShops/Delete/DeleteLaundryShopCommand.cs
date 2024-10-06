using Castle.Components.DictionaryAdapter.Xml;
using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.LaundryShops.Delete
{
    public class DeleteLaundryShopCommand : IRequest<bool>, ICommand
    {
        public DeleteLaundryShopCommand()
        {
            
        }
        public string Id { get; set; }

        public DeleteLaundryShopCommand(string id)
        {
            Id = id;
        }
    }

}
