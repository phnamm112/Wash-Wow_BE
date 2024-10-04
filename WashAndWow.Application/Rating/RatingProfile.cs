using AutoMapper;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Rating
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<RatingEntity, RatingDto>();
        }
    }
}
