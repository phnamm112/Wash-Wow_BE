using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using WashAndWow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
    {
        public void Configure(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.ToTable("Notification");
            builder.HasKey(n => n.ID);

            builder.Property(n => n.ReceiverID)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.IsRead)
                .HasDefaultValue(false);

            builder.Property(n => n.ReadAt)
                .IsRequired(false);

            builder.Property(n => n.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (NotificationType)Enum.Parse(typeof(NotificationType), v))
                .IsRequired();
        }
    }
}
