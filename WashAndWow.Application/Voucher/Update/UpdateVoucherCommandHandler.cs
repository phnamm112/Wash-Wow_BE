using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Update
{
    public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand, string>
    {
        private readonly IVoucherRepository _repository;
        private readonly IMapper _mapper;
        public UpdateVoucherCommandHandler(
            IVoucherRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
        {
            var voucher = await _repository.FindAsync(x => x.ID.Equals(request.Id) && x.DeletedAt == null, cancellationToken);

            if (voucher == null)
            {
                throw new NotFoundException("Voucher is not existed");
            }

            voucher.Name = request.Name;
            voucher.ImgUrl = request.ImgUrl;
            voucher.ExpiryDate = request.ExpiryDate;
            voucher.Type = request.Type;
            voucher.MaximumReduce = request.MaximumReduce;
            voucher.MinimumReduce = request.MinimumReduce;
            voucher.Amount = request.Amount;
            voucher.ConditionOfUse = request.ConditionOfUse;

            _repository.Update(voucher);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failed";
        }
    }
}
