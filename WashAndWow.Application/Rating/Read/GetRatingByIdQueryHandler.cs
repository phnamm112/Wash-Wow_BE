using AutoMapper;
using MediatR;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Rating.Read
{
    public class GetVaucherByIdQueryHandler : IRequestHandler<GetRatingByIdQuery, RatingDto>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public GetVaucherByIdQueryHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<RatingDto> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.FindAsync(x => x.ID == request.Id);
            if (rating == null) return null;

            return _mapper.Map<RatingDto>(rating);
        }
    }
}
