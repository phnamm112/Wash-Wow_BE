using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.Delete
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
    {
        private readonly IBookingRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public DeleteBookingCommandHandler(
            IBookingRepository repository,
            ICurrentUserService currentUserService,
            IUserRepository userRepository)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var booking = await _repository.FindAsync(x => x.ID.Equals(request.Id));

            if (booking == null) throw new NotFoundException($"không tìm thấy Booking có ID:{request.Id }");

            booking.DeleterID = user.ID;
            booking.DeletedAt = DateTime.Now;
            _repository.Update(booking);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
