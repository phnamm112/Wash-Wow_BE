using FluentValidation;

namespace WashAndWow.Application.Form.ApprovalForm
{
    public class ApprovalFormCommandValidator : AbstractValidator<ApprovalFormCommand>
    {
        public ApprovalFormCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.FormID)
                .NotEmpty().NotNull().WithMessage("FormID is required");

            RuleFor(v => v.Status)
                .NotNull().WithMessage("Status is required");
        }
    }
}
