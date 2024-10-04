using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Rating.Update
{
    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand, RatingDto>
    {
        private readonly IRatingRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRatingCommandHandler(
            IRatingRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RatingDto?> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);

            if (rating == null)
                return null;

            // Update properties
            rating.RatingValue = request.RatingValue;
            rating.Comment = request.Comment;

            await _unitOfWork.SaveChangesAsync(cancellationToken); // Save changes

            return _mapper.Map<RatingDto>(rating);
        }
    }
}
