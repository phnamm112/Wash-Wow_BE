using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WashAndWow.Application.LaundryShops.Delete
{
    public class DeleteLaundryShopCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteLaundryShopCommand(string id)
        {
            Id = id;
        }
    }

}
