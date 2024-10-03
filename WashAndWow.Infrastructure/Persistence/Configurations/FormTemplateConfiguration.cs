using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
