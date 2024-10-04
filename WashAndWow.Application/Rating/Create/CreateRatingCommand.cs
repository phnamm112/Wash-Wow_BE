using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Rating.Create
{
    public class CreateRatingCommand : IRequest<string>, ICommand
    {
        public RatingDto Rating { get; }

        public CreateRatingCommand(RatingDto rating)
        {
            Rating = rating;
        }
    }
}
