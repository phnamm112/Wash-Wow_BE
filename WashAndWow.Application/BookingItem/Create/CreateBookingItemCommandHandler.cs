using AutoMapper;
using MediatR;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.BookingItem.Create
{
    public class CreateBookingItemCommandHandler : IRequestHandler<CreateBookingItemCommand, string>
    {
        private IBookingItemRepository _repository;
        private IMapper _mapper;

        public CreateBookingItemCommandHandler(IBookingItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateBookingItemCommand request, CancellationToken cancellationToken)
        {
            var bookingItem = new BookingItemEntity
            {
                ServicesID = request.ServicesId,
                BookingID = request.BookingId
            };
            _repository.Add(bookingItem);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return bookingItem.ID;
        }
    }
}
