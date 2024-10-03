using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wash_Wow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
    {
        public void Configure(EntityTypeBuilder<BookingEntity> builder)
        {

            builder.Property(po => po.Note).HasMaxLength(255);
            builder.Property(e => e.Status)
                .HasConversion(v => v.ToString()
                , v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v));

            builder.HasOne(b => b.Customer)
                .WithMany(u => u.Bookings)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
