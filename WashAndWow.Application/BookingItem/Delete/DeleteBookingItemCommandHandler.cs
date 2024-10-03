using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.BookingItem.Delete
{
    public class DeleteBookingItemCommandHandler : IRequestHandler<DeleteBookingItemCommand, bool>
    {
        private readonly IBookingItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookingItemCommandHandler(IBookingItemRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBookingItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.FindAsync(i => i.ID.Equals(request.Id));
            if (item == null) return false;
            _repository.Remove(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
