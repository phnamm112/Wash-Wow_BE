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
    public class BookingItemConfiguration : IEntityTypeConfiguration<BookingItemEntity>
    {
        public void Configure(EntityTypeBuilder<BookingItemEntity> builder)
        {
            builder.HasOne(x => x.Booking)
                .WithMany(u => u.BookingItems)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ShopService)
                .WithMany(u => u.BookingItems)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
