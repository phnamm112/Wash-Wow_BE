using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.Read
{
    public class GetAllBookingByShopIdCommandHandler : IRequestHandler<GetAllBookingbyShopIdQuery, IPagedResult<BookingDto>>
    {
        private readonly IRatingRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBookingByShopIdCommandHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            _repository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<IPagedResult<BookingDto>> Handle(GetAllBookingbyShopIdQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.FindAllAsync(x => x.LaundryShopID.Equals(request.ShopId), request.PageNo, request.PageSize, cancellationToken);
            // Map the entities to DTOs
            var pagedDtoResult = new PagedList<BookingDto>(
                pagedResult.TotalCount,
                pagedResult.PageNo,
                pagedResult.PageSize,
                _mapper.Map<List<BookingDto>>(pagedResult.ToList())
            );

            return pagedDtoResult;
        }
    }
}

