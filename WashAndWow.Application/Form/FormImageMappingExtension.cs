using AutoMapper;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Form
{
    public static class FormImageMappingExtension
    {
        public static FormImageDto MapToFormImageDto(this FormImageEntity projectFrom, IMapper mapper)
            => mapper.Map<FormImageDto>(projectFrom);
        public static List<FormImageDto> MapToFormImageDtoList(this IEnumerable<FormImageEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToFormImageDto(mapper)).ToList();
    }
}
