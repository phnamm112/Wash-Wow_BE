using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Pagination;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.GetAllByShopID
{
    public class GetAllBookingByShopIdCommandHandler : IRequestHandler<GetAllBookingbyShopIdQuery, PagedResult<BookingDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IMapper _mapper;

        public GetAllBookingByShopIdCommandHandler(IBookingRepository bookingRepository
            , IUserRepository userRepository
            , ILaundryShopRepository laundryShopRepository
            , IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _laundryShopRepository = laundryShopRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BookingDto>> Handle(GetAllBookingbyShopIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookingRepository.FindAllAsync(x => x.LaundryShopID.Equals(request.ShopId) && x.DeletedAt == null
                , request.PageNumber, request.PageSize, cancellationToken);
            var customer = await _userRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.FullName, cancellationToken);
            var laundryShops = await _laundryShopRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.Name, cancellationToken);

            return PagedResult<BookingDto>.Create(
            totalCount: result.TotalCount,
            pageCount: result.PageCount,
            pageSize: result.PageSize,
                pageNumber: result.PageNo,
                data: result.MapToBookingDtoList(_mapper, customer, laundryShops));
        }
    }
}

