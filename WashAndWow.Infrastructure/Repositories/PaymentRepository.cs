using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Infrastructure.Persistence;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Entities.ConfigTable;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Infrastructure.Repositories
{
    public class PaymentRepository : RepositoryBase<PaymentEntity, PaymentEntity, ApplicationDbContext>, IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }
    }
}