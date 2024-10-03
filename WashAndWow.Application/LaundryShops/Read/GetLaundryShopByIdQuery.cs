using MediatR;

namespace WashAndWow.Application.LaundryShops.Read
{
    public class GetLaundryShopByIdQuery : IRequest<LaundryShopDto>
    {
        public string Id { get; set; }

        public GetLaundryShopByIdQuery(string id)
        {
            Id = id;
        }
    }

}
