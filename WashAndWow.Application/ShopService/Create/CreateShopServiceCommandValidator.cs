using FluentValidation;

namespace WashAndWow.Application.ShopService.Create
{
    public class CreateShopServiceCommandValidator : AbstractValidator<CreateShopServiceCommand>
    {
        public CreateShopServiceCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Service Name is required");

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Service Description is required");

            RuleFor(v => v.PricePerKg)
                .GreaterThan(0).WithMessage("Price per kg must be greater than 0");

            RuleFor(v => v.MinLaundryWeight)
                .GreaterThan(0).WithMessage("minimum weight must be greater than 0");

            RuleFor(v => v.ShopId)
                .NotEmpty().WithMessage("Shop ID is required");
        }
    }
}
