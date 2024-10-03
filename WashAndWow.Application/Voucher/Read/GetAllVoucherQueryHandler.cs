using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Read
{
    public class GetAllVoucherQueryHandler : IRequestHandler<GetAllVoucherQuery, IPagedResult<VoucherDto>>
    {
        private readonly IVoucherRepository _repository;
        private readonly IMapper _mapper;

        public GetAllVoucherQueryHandler(IVoucherRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IPagedResult<VoucherDto>> Handle(GetAllVoucherQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.FindAllAsync(x => x.Amount >= 1, request.PageNo, request.PageSize, cancellationToken);
            // Map the entities to DTOs
            var pagedDtoResult = new PagedList<VoucherDto>(
                pagedResult.TotalCount,
                pagedResult.PageNo,
                pagedResult.PageSize,
                _mapper.Map<List<VoucherDto>>(pagedResult.ToList())
            );

            return pagedDtoResult;
        }
    }
}
