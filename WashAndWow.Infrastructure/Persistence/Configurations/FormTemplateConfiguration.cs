using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Entities.ConfigTable;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class FormTemplateConfiguration : IEntityTypeConfiguration<FormTemplateEntity>
    {
        public void Configure(EntityTypeBuilder<FormTemplateEntity> builder)
        {

        }
    }
}
