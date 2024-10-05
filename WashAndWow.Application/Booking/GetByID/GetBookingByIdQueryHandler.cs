using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.GetByID
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IBookingRepository bookingRepository
            , IUserRepository userRepository
            , ILaundryShopRepository laundryShopRepository
            , IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _laundryShopRepository = laundryShopRepository;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.FindAsync(x => x.ID.Equals(request.Id) && x.DeletedAt == null, cancellationToken);
            if (booking == null)
            {
                throw new NotFoundException("Booking is not exist");
            }
            var user = await _userRepository.FindAsync(x => x.ID == booking.CreatorID && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User is not exist");
            }
            var laundryShop = await _laundryShopRepository.FindAsync(x => x.ID == booking.LaundryShopID && x.DeletedAt == null, cancellationToken);
            if (laundryShop == null)
            {
                throw new NotFoundException("Laundry shop is not exist");
            }

            return booking.MapToBookingDto(_mapper, user.FullName, laundryShop.Name);
        }
    }
}
