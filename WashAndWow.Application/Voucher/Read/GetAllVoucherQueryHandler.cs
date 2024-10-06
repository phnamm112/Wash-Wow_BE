using AutoMapper;
using Castle.Core.Resource;
using MediatR;
using Wash_Wow.Application.Common.Models;
using Wash_Wow.Application.Common.Pagination;
using Wash_Wow.Domain.Repositories;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Application.Booking;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Read
{
    public class GetAllVoucherQueryHandler : IRequestHandler<GetAllVoucherQuery, PagedResult<VoucherDto>>
    {
        private readonly IVoucherRepository _repository;
        private readonly IMapper _mapper;

        public GetAllVoucherQueryHandler(IVoucherRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<VoucherDto>> Handle(GetAllVoucherQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.FindAllAsync(x => x.DeletedAt == null, request.PageNumber, request.PageSize, cancellationToken);

            return PagedResult<VoucherDto>.Create(
            totalCount: result.TotalCount,
            pageCount: result.PageCount,
            pageSize: result.PageSize,
            pageNumber: result.PageNo,
                data: result.MapToVoucherDtoList(_mapper));
        }
    }
}
