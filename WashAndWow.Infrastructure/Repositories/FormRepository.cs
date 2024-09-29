using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Persistence;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Infrastructure.Repositories
{
    public class FormRepository : RepositoryBase<FormEntity, FormEntity, ApplicationDbContext>, IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }
    }
}
