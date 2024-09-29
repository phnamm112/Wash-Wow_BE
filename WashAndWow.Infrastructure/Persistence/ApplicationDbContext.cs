using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Common.Interfaces;
using Wash_Wow.Domain.Entities;
using WashAndWow.Domain.Entities;
using WashAndWow.Infrastructure.Persistence.Configurations;

namespace Wash_Wow.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<LaundryShopEntity> LaundryShops { get; set; }
        public DbSet<BookingItemEntity> BookingItems { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<ShopServiceEntity> ShopServices { get; set; }
        public DbSet<VoucherEntity> Vouchers {  get; set; }
        public DbSet<FormEntity> Forms { get; set; }
        public DbSet<FormImageEntity> FormImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BookingItemConfiguration());
            modelBuilder.ApplyConfiguration(new LaundryShopConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new ShopServiceConfiguration());
            modelBuilder.ApplyConfiguration(new VoucherConfiguration());
            modelBuilder.ApplyConfiguration(new FormConfiguration());

            ConfigureModel(modelBuilder);
        }
        private void ConfigureModel(ModelBuilder modelBuilder)
        {


        }
    }
}
