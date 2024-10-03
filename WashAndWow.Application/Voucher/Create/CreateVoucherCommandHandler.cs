using AutoMapper;
using MediatR;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Create
{
    public class CreateVoucherCommandHandler : IRequestHandler<CreateVoucherCommand, string>
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;

        public CreateVoucherCommandHandler(IVoucherRepository voucherRepository, IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
        {
            var voucherEntity = _mapper.Map<VoucherEntity>(request.Voucher);
            _voucherRepository.Add(voucherEntity);
            await _voucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return voucherEntity.ID;
        }
    }
}
