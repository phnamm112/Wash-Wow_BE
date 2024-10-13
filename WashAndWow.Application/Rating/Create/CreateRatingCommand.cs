using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Rating.Create
{
    public class CreateRatingCommand : IRequest<RatingDto>, ICommand
    {
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public string UserId { get; set; }
        public string LaundryShopID { get; set; }

        // Parameterless constructor for deserialization
        public CreateRatingCommand() { }

        // Constructor with fields
        public CreateRatingCommand(int ratingValue, string? comment, string userId, string laundryShopID)
        {
            RatingValue = ratingValue;
            Comment = comment;
            UserId = userId;
            LaundryShopID = laundryShopID;
        }
    }
}
