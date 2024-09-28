using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Persistence;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Infrastructure.Repositories
{
    public class LaundryShopRepository : RepositoryBase<LaundryShopEntity, LaundryShopEntity, ApplicationDbContext>, ILaundryShopRepository
    {
        private readonly ApplicationDbContext _context;

        public LaundryShopRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }
    }
}
