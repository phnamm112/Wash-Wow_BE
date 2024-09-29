using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Application.Users.ChangePassword;

namespace WashAndWow.Application.Users.ResetForgotPassword
{
    public class ResetForgotPasswordCommandValidator : AbstractValidator<ResetForgotPasswordCommand>
    {
        public ResetForgotPasswordCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(6).WithMessage("Password must contains more than 6 characters");
        }
    }
}
