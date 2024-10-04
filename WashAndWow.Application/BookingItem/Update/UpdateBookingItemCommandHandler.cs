using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.BookingItem.Update
{
    internal class UpdateBookingItemCommandHandler : IRequestHandler<UpdateBookingItemCommand, BookingItemDto>
    {
        private readonly IBookingItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookingItemCommandHandler(
            IBookingItemRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookingItemDto?> Handle(UpdateBookingItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);

            if (item == null)
                return null;

            // Update properties
            item.ServicesID = request.ServiceId;

            await _unitOfWork.SaveChangesAsync(cancellationToken); // Save changes

            return _mapper.Map<BookingItemDto>(item);
        }
    }
}