using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using Wash_Wow.Domain.Entities;
using WashAndWow.Application.LaundryShops;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Rating
{
    public class RatingDto : IMapFrom<RatingEntity>
    {
        public string Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public string LaundryShopId { get; set; }
        public string CreatorId { get; set; }
        public string UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RatingEntity, RatingDto>();
        }
    }
}
