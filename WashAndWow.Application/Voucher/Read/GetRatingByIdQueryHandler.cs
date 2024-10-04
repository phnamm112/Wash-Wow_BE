using AutoMapper;
using MediatR;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Read
{
    public class GetVaucherByIdQueryHandler : IRequestHandler<GetVoucherByIdQuery, VoucherDto>
    {
        private readonly IVoucherRepository _repository;
        private readonly IMapper _mapper;

        public GetVaucherByIdQueryHandler(IVoucherRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VoucherDto> Handle(GetVoucherByIdQuery request, CancellationToken cancellationToken)
        {
            var rating = await _repository.FindAsync(x => x.ID == request.Id);
            if (rating == null) return null;

            return _mapper.Map<VoucherDto>(rating);
        }
    }
}
