using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class ShopServiceConfiguration : IEntityTypeConfiguration<ShopServiceEntity>
    {
        public void Configure(EntityTypeBuilder<ShopServiceEntity> builder)
        {
            builder.HasOne(x => x.LaundryShop)
                .WithMany(u => u.Services)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
