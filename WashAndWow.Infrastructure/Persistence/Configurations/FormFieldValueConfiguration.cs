using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class FormFieldValueConfiguration : IEntityTypeConfiguration<FormFieldValueEntity>
    {
        public void Configure(EntityTypeBuilder<FormFieldValueEntity> builder)
        {

        }
    }
}
