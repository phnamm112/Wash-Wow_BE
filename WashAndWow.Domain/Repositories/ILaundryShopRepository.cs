using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Domain.Repositories
{
    public interface ILaundryShopRepository : IEFRepository<LaundryShopEntity, LaundryShopEntity>
    {
        public Task<IPagedResult<LaundryShopEntity>> GetAllShopsPagedAsync(int pageNo, int pageSize, CancellationToken cancellationToken);
    }
}
