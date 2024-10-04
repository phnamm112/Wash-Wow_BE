using AutoMapper;
using Wash_Wow.Infrastructure.Persistence;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Entities.ConfigTable;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Infrastructure.Repositories
{
    public class FormTemplateContentRepository : RepositoryBase<FormTemplateContentEntity, FormTemplateContentEntity, ApplicationDbContext>, IFormTemplateContentRepository
    {
        private readonly ApplicationDbContext _context;

        public FormTemplateContentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }
    }
}
