using MediatR;
using Wash_Wow.Domain.Common.Interfaces;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Voucher.Delete
{
    public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, bool>
    {
        private readonly IVoucherRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVoucherCommandHandler(
            IVoucherRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
        {
            var voucher = await _repository.FindAsync(ss => ss.ID.Equals(request.Id));
            if (voucher == null)
            {
                return false; // Service not found
            }

            _repository.Remove(voucher);
            await _unitOfWork.SaveChangesAsync(cancellationToken);// save change 
            return true; // Deletion successful
        }
    }
}
