using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Booking.Update
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, string>
    {
        private readonly IBookingRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UpdateBookingCommandHandler(IBookingRepository repository
            , IMapper mapper
            , ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _repository.FindAsync(x => x.ID.Equals(request.Id) && x.DeletedAt == null, cancellationToken);
            if (booking == null)
            {
                throw new NotFoundException("Booking is not exist");
            };

            booking.UpdaterID = _currentUserService.UserId;
            booking.LastestUpdateAt = DateTime.Now;

            _repository.Update(booking);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failed";
        }
    }
}
