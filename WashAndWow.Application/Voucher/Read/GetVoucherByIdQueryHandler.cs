using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Exceptions;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Read
{
    public class GetVoucherByIdQueryHandler : IRequestHandler<GetVoucherByIdQuery, VoucherDto>
    {
        private readonly IVoucherRepository _repository;
        private readonly IMapper _mapper;

        public GetVoucherByIdQueryHandler(IVoucherRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VoucherDto> Handle(GetVoucherByIdQuery request, CancellationToken cancellationToken)
        {
            var voucher = await _repository.FindAsync(x => x.ID == request.Id && x.DeletedAt == null, cancellationToken);
            if (voucher == null)
            {
                throw new NotFoundException("Voucher is not exist");
            }

            return voucher.MapToVoucherDto(_mapper);
        }
    }
}
