using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.BookingItem.Read
{
    public class GetAllBookingItemByBookingIdQueryHandler : IRequestHandler<GetAllBookingItemByBookingIdQuery, IPagedResult<BookingItemDto>>
    {
        private readonly IBookingItemRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBookingItemByBookingIdQueryHandler(IBookingItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IPagedResult<BookingItemDto>> Handle(GetAllBookingItemByBookingIdQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.FindAllAsync(x => x.BookingID == request.BookingId, request.PageNo, request.PageSize, cancellationToken);
            // Map the entities to DTOs
            var pagedDtoResult = new PagedList<BookingItemDto>(
                pagedResult.TotalCount,
                pagedResult.PageNo,
                pagedResult.PageSize,
                _mapper.Map<List<BookingItemDto>>(pagedResult.ToList())
            );

            return pagedDtoResult;
        }
    }
}
