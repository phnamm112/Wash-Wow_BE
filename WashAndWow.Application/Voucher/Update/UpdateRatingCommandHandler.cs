using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Update
{
    public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand, VoucherDto>
    {
        private readonly IVoucherRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVoucherCommandHandler(
            IVoucherRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<VoucherDto?> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
        {
            var voucher = await _repository.FindAsync(x => x.ID.Equals(request.Id), cancellationToken);

            if (voucher == null)
                return null;

            // Update properties
            voucher.Name = request.Name;
            voucher.ImgUrl = request.ImgUrl;
            voucher.ExpiryDate = request.ExpiryDate;
            voucher.Type = request.Type;
            voucher.MaximumReduce = request.MaximumReduce;
            voucher.MinimumReduce = request.MinimumReduce;
            voucher.Amount = request.Amount;
            voucher.ConditionOfUse = request.ConditionOfUse;

            await _unitOfWork.SaveChangesAsync(cancellationToken); // Save changes

            return _mapper.Map<VoucherDto>(voucher);
        }
    }
}
