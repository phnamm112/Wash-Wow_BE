using FluentValidation;

namespace WashAndWow.Application.Form.SendForm
{
    public class SendFormCommandValidator : AbstractValidator<SendFormCommand>
    {
        public SendFormCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {

        }
    }
}
