﻿using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Read
{
    public class GetAllLaundryShopsQueryHandler : IRequestHandler<GetAllLaundryShopsQuery, IPagedResult<LaundryShopDto>>
    {
        private readonly ILaundryShopRepository _repository;
        private readonly IMapper _mapper;

        public GetAllLaundryShopsQueryHandler(ILaundryShopRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IPagedResult<LaundryShopDto>> Handle(GetAllLaundryShopsQuery request, CancellationToken cancellationToken)
        {
            // Get paged list of LaundryShopEntity from the repository
            var pagedResult = await _repository.FindAllAsync(x => x.DeletedAt == null, request.PageNo, request.PageSize, cancellationToken);

            // Map the entities to DTOs
            var pagedDtoResult = new PagedList<LaundryShopDto>(
                pagedResult.TotalCount,
                pagedResult.PageNo,
                pagedResult.PageSize,
                _mapper.Map<List<LaundryShopDto>>(pagedResult.ToList())
            );

            return pagedDtoResult;
        }

    }
}
