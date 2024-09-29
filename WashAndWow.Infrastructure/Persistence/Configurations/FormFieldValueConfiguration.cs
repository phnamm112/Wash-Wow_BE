using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
