using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Rating.Read
{
    public class GetAllRatingQueryHandler : IRequestHandler<GetAllRatingQuery, IPagedResult<RatingDto>>
    {
        private readonly IRatingRepository _repository;
        private readonly IMapper _mapper;

        public GetAllRatingQueryHandler(IRatingRepository ratingRepository, IMapper mapper)
        {
            _repository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<IPagedResult<RatingDto>> Handle(GetAllRatingQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.FindAllAsync(x => x.LaundryShopID == request.ShopId, request.PageNo, request.PageSize, cancellationToken);
            // Map the entities to DTOs
            var pagedDtoResult = new PagedList<RatingDto>(
                pagedResult.TotalCount,
                pagedResult.PageNo,
                pagedResult.PageSize,
                _mapper.Map<List<RatingDto>>(pagedResult.ToList())
            );

            return pagedDtoResult;
        }
    }
}
