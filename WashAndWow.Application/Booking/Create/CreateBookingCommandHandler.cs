using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.Create
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, string>
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // Map the DTO to the entity using AutoMapper
            var booking = _mapper.Map<BookingEntity>(request);

            _repository.Add(booking);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return booking.ID;
        }
    }
}
