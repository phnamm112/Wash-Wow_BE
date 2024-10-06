using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Voucher.Create
{
    public class CreateVoucherCommandValidator : AbstractValidator<CreateVoucherCommand>
    {
        public CreateVoucherCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(v => v.Name)
                .NotNull().NotEmpty().WithMessage("Name is required");

            RuleFor(v => v.ConditionOfUse)
                .NotNull().WithMessage("Condition is required")
                .GreaterThanOrEqualTo(0).WithMessage("Condition must be greater or equal 0");

            RuleFor(v => v.ExpiryDate)
                .NotNull().WithMessage("Expiry date is require")
                .GreaterThan(DateTime.Now).WithMessage("Expiry date must not be in past");

            RuleFor(v => v.Amount)
                .NotNull().WithMessage("Amount is required")
                .GreaterThan(0).WithMessage("Amount must be greater than 0");

            RuleFor(v => v.MaximumReduce)
                .GreaterThanOrEqualTo(v => v.MinimumReduce).WithMessage("Maximum reduce can not be less than minimum reduce");

            RuleFor(v => v.Type)
                .IsInEnum();
        }
    }
}
