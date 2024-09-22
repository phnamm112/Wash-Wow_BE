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
    class EmailVerificationConfiguration : IEntityTypeConfiguration<EmailVerification>
    {
        public void Configure(EntityTypeBuilder<EmailVerification> builder)
        {
            builder.ToTable("EmailVerification");

            builder.HasKey(e => e.UserID);
            builder.HasIndex(e => e.UserID)
                .IsUnique();
            builder.Property(e => e.UserID)
                .IsRequired();
            builder.Property(e => e.Token)
                .IsRequired();
            builder.Property(e => e.ExpireTime)
                .IsRequired()
                .HasColumnType("datetime");
            builder.HasIndex(e => e.Token)
                .IsUnique();
        }
    }
}
