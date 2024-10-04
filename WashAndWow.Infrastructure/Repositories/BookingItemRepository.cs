using AutoMapper;
using Wash_Wow.Infrastructure.Persistence;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Infrastructure.Repositories
{
    public class BookingItemRepository : RepositoryBase<BookingItemEntity, BookingItemEntity, ApplicationDbContext>, IBookingItemRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingItemRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }
    }
}
