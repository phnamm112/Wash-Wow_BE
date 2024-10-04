using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Rating.Delete
{
    public class DeleteVoucherCommandHandler : IRequestHandler<DeleteRatingCommand, bool>
    {
        private readonly IRatingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVoucherCommandHandler(
            IRatingRepository ratingRepository,
            IUnitOfWork unitOfWork)
        {
            _repository = ratingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = await _repository.FindAsync(ss => ss.ID.Equals(request.Id));
            if (rating == null)
            {
                return false; // Service not found
            }

            _repository.Remove(rating);
            await _unitOfWork.SaveChangesAsync(cancellationToken);// save change 
            return true; // Deletion successful
        }
    }
}
