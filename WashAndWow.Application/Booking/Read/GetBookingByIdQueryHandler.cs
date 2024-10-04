using AutoMapper;
using MediatR;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.Read
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await _repository.FindAsync(x => x.ID.Equals(request.Id));

            if (booking == null) return null;

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
