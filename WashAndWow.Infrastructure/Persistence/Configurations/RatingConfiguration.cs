using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<RatingEntity>
    {
        public void Configure(EntityTypeBuilder<RatingEntity> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(u => u.Ratings)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LaundryShop)
                .WithMany(u => u.Ratings)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
