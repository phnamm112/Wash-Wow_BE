using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wash_Wow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasIndex(x => x.PhoneNumber)
                .IsUnique();
            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.Property(e => e.Status)
                .HasConversion(v => v.ToString()
                , v => (UserStatus)Enum.Parse(typeof(UserStatus), v));

        }
    }
}
