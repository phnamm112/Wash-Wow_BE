using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WashAndWow.Application.Rating.Create
{
    internal class CreateRatingCommandValidator:AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
            RuleFor(x => x.RatingValue)
            .Must(BeAWholeNumber)
            .WithMessage("Rating value must be a whole number between 1 and 5.")
            .InclusiveBetween(1, 5) // Ensures the value is between 1 and 5
            .WithMessage("Rating value must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(500) // Assuming a maximum comment length of 500 characters
                .WithMessage("Comment cannot exceed 500 characters.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.LaundryShopID)
                .NotEmpty().WithMessage("Laundry Shop ID is required.");
        }
        private bool BeAWholeNumber(int ratingValue)
        {
            return ratingValue == (int)ratingValue; // Ensures it's a whole number
        }
    }
}
