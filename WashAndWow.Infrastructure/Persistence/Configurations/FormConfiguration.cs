using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class FormConfiguration : IEntityTypeConfiguration<FormEntity>
    {
        public void Configure(EntityTypeBuilder<FormEntity> builder)
        {
            builder.HasOne(x => x.Sender)
                .WithMany(u => u.SentForms)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(e => e.Status)
                .HasConversion(v => v.ToString()
                , v => (FormStatus)Enum.Parse(typeof(FormStatus), v));

            builder.HasMany(f => f.FormImages)
                .WithOne(i => i.Form)
                .HasForeignKey(i => i.FormID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.FieldValues)
                .WithOne(fv => fv.Form)
                .HasForeignKey(fv => fv.FormID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
