using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Rating
{
    public static class RatingDtoMappingExtension
    {
        public static RatingDto MapToRatingDto(this RatingEntity projectFrom, IMapper mapper)
        => mapper.Map<RatingDto>(projectFrom);

        public static List<RatingDto> MapToRatingDtoList(this IEnumerable<RatingEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToRatingDto(mapper)).ToList();

        public static RatingDto MapToRatingDto(this RatingEntity projectFrom, IMapper mapper, string userName)
        {
            var dto = mapper.Map<RatingDto>(projectFrom);
            dto.UserName = userName; // Assuming RatingDto has a UserName property
            return dto;
        }

        public static List<RatingDto> MapToRatingDtoList(this IEnumerable<RatingEntity> projectFrom, IMapper mapper,
            Dictionary<string, string> userNames)
        => projectFrom.Select(x => x.MapToRatingDto(mapper,
            userNames.ContainsKey(x.CreatorID) ? userNames[x.CreatorID] : "Unknown User")).ToList();
    }
}
