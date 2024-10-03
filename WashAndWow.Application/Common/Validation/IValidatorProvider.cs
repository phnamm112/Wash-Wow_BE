using FluentValidation;

namespace Wash_Wow.Application.Common.Validation
{
    public interface IValidatorProvider
    {
        IValidator<T> GetValidator<T>();
    }
}
