using AutoMapper;
using MediatR;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.Update
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, BookingDto>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public UpdateBookingCommandHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _repository.FindAsync(x => x.ID.Equals(request.Id));

            if (booking == null) return null;

            _mapper.Map(request, booking);

            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
