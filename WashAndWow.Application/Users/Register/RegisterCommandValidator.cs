using FluentValidation;

namespace Wash_Wow.Application.Users.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
        }
    }
}
