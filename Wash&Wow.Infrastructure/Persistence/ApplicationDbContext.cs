using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Common.Interfaces;

namespace Wash_Wow.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //
            ConfigureModel(modelBuilder);
        }
        private void ConfigureModel(ModelBuilder modelBuilder)
        {


        }
    }
}
