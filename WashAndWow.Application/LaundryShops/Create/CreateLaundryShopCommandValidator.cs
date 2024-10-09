using FluentValidation;

namespace WashAndWow.Application.LaundryShops.Create
{
    public class CreateLaundryShopCommandValidator : AbstractValidator<CreateLaundryShopCommand>
    {
        public CreateLaundryShopCommandValidator()
        {
            ConfigureValidationRules();
        }
        private void ConfigureValidationRules()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Laundry shop name is required")
               .MaximumLength(100).WithMessage("Laundry shop name must not exceed 100 characters");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters");

            RuleFor(x => x.PhoneContact)
                .NotEmpty().WithMessage("Phone contact is required")
                .Matches(@"^\d{10,11}$").WithMessage("Phone contact must be between 10 to 11 digits");

            RuleFor(x => x.TotalMachines)
                .GreaterThan(0).WithMessage("Total machines must be greater than 0");

            RuleFor(x => x.OpeningHour)
                .NotEmpty().WithMessage("Opening hour is required")
                .Matches(@"^\d{2}:\d{2}$").WithMessage("Opening hour must be in HH:mm format");

            RuleFor(x => x.ClosingHour)
                .NotEmpty().WithMessage("Closing hour is required")
                .Matches(@"^\d{2}:\d{2}$").WithMessage("Closing hour must be in HH:mm format");
        }
    }
}
