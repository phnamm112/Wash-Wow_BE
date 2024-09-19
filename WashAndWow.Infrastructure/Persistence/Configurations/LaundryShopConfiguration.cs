using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class LaundryShopConfiguration : IEntityTypeConfiguration<LaundryShopEntity>
    {
        public void Configure(EntityTypeBuilder<LaundryShopEntity> builder)
        {
            builder.HasIndex(x => x.Address)
                .IsUnique();
            builder.HasIndex(x => x.PhoneContact)
                .IsUnique();
            builder.HasOne(ls => ls.Owner)
                .WithMany(u => u.LaundryShops)
                .HasForeignKey(ls => ls.OwnerID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
