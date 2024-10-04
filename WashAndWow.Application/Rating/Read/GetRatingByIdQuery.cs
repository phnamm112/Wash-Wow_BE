using MediatR;

namespace WashAndWow.Application.Rating.Read
{
    public class GetRatingByIdQuery : IRequest<RatingDto>
    {
        public string Id { get; }

        public GetRatingByIdQuery(string id)
        {
            Id = id;
        }
    }
}
