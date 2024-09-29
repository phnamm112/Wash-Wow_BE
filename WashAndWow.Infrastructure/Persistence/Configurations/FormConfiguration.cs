using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class FormConfiguration : IEntityTypeConfiguration<FormEntity>
    {
        public void Configure(EntityTypeBuilder<FormEntity> builder)
        {
            builder.HasOne(x => x.Sender)
                .WithMany(u => u.SentForms)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
