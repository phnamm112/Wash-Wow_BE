using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.LaundryShops.Read
{
    public class GetLaundryShopByIdQuery : IRequest<LaundryShopDto>, IQuery
    {
        public string Id { get; set; }
        public GetLaundryShopByIdQuery()
        {
            
        }
        public GetLaundryShopByIdQuery(string id)
        {
            Id = id;
        }
    }

}
