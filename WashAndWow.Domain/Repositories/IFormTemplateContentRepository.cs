using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities.ConfigTable;

namespace WashAndWow.Domain.Repositories
{
    public interface IFormTemplateContentRepository : IEFRepository<FormTemplateContentEntity, FormTemplateContentEntity>
    {
    }
}
