using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Infrastructure.Persistence;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Entities.ConfigTable;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Infrastructure.Repositories
{
    public class FormTemplateRepository : RepositoryBase<FormTemplateEntity, FormTemplateEntity, ApplicationDbContext>, IFormTemplateRepository
    {
        private readonly ApplicationDbContext _context;

        public FormTemplateRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }
    }
}
