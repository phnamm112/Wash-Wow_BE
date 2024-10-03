using AutoMapper;
using MediatR;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Rating.Create
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, string>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public CreateRatingCommandHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var ratingEntity = _mapper.Map<RatingEntity>(request.Rating);
            _ratingRepository.Add(ratingEntity);
            await _ratingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return ratingEntity.ID;
        }
    }
}
