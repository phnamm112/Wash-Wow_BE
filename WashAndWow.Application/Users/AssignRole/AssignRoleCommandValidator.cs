using FluentValidation;

namespace WashAndWow.Application.Users.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Role)
                .NotEmpty().NotNull().WithMessage("Role is required");
            RuleFor(v => v.UserID)
                .NotEmpty().NotNull().WithMessage("UserID is required");
        }
    }
}
