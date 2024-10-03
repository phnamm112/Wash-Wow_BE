using AutoMapper;
using MediatR;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.BookingItem.Read
{
    public class GetBookingItemByIdQueryHandler : IRequestHandler<GetBookingItemByIdQuery, BookingItemDto>
    {
        private readonly IBookingItemRepository _repository;
        private readonly IMapper _mapper;

        public GetBookingItemByIdQueryHandler(IBookingItemRepository bookingItemRepository, IMapper mapper)
        {
            _repository = bookingItemRepository;
            _mapper = mapper;
        }

        public async Task<BookingItemDto> Handle(GetBookingItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _repository.FindAsync(x => x.ID == request.Id);
            if (item == null) return null;

            return _mapper.Map<BookingItemDto>(item);
        }
    }
}

