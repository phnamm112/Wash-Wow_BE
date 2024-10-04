using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Domain.Repositories
{
    public interface IBookingRepository : IEFRepository<BookingEntity, BookingEntity>
    {
    }
}
