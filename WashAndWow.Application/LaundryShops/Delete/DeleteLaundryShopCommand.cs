using MediatR;

namespace WashAndWow.Application.LaundryShops.Delete
{
    public class DeleteLaundryShopCommand : IRequest<bool>
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
