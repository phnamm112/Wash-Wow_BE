using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class VoucherConfiguration : IEntityTypeConfiguration<VoucherEntity>
    {
        public void Configure(EntityTypeBuilder<VoucherEntity> builder)
        {
            builder.HasKey(x => x.ID)
                .HasName("Code");
            builder.HasOne(x => x.Creator)
                .WithMany(u => u.CreatedVouchers)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
