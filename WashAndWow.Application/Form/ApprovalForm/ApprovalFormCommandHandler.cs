using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Form.ApprovalForm
{
    public class ApprovalFormCommandHandler : IRequestHandler<ApprovalFormCommand, string>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IFormRepository _formRepository;
        public ApprovalFormCommandHandler(IFormRepository formRepository, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _formRepository = formRepository;
        }
        public async Task<string> Handle(ApprovalFormCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(FormStatus), request.Status))
            {
                throw new NotFoundException("Status not found");
            }

            var existForm = await _formRepository.FindAsync(x => x.DeletedAt == null && x.ID == request.FormID, cancellationToken);
            if (existForm == null)
            {
                throw new NotFoundException("FormID is not exist");
            }

            existForm.Status = request.Status;
            _formRepository.Update(existForm);
            return await _formRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failed";
        }
    }
}
