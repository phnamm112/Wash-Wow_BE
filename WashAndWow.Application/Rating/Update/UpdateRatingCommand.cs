using MediatR;

namespace WashAndWow.Application.Rating.Update
{
    public class UpdateRatingCommand : IRequest<RatingDto>
    {
        public string Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
    }
}
