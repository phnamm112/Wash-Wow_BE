using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
