using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Rating.Update
{
    public class UpdateRatingCommand : IRequest<RatingDto>, ICommand
    {
        public string Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public UpdateRatingCommand()
        {
            
        }
    }
}
