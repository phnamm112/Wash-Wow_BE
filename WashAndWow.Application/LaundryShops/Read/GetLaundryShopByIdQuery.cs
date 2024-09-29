using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
