using MediatR;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.Delete
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
    {
        private readonly IBookingRepository _repository;

        public DeleteBookingCommandHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _repository.FindAsync(x => x.ID.Equals(request.Id));

            if (booking == null) return false;

            _repository.Remove(booking);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
