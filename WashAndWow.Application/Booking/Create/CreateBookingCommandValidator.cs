using FluentValidation;

namespace WashAndWow.Application.Booking.Create
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.VoucherId)
                .NotEmpty().NotNull().WithMessage("VoucherID is required");

            RuleFor(v => v.BookingItems)
                .NotEmpty().WithMessage("There is no booking item in request");

            RuleFor(v => v.LaundryWeight)
                .NotNull().WithMessage("Laundry weight is required")
                .GreaterThan(0).WithMessage("Laundry weight must greater than 0");

            RuleFor(v => v.ShopPickupTime)
                .NotNull().WithMessage("Pickup time is required")
                .Must(NotBeInPast).WithMessage("Pickup time can not be in past");
        }

        private bool NotBeInPast(DateTime time)
        {
            return time >= DateTime.Now;
        }
    }
}
