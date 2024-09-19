using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;
using System.Reflection.Emit;

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
