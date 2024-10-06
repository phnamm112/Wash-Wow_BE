using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Rating.Read
{
    public class GetRatingByIdQuery : IRequest<RatingDto>, IQuery
    {
        public string Id { get; }
        public GetRatingByIdQuery()
        {
            
        }
        public GetRatingByIdQuery(string id)
        {
            Id = id;
        }
    }
}
