using AutoMapper;
using WashAndWow.Domain.Entities.ConfigTable;

namespace WashAndWow.Application.FormTemplate
{
    public static class TemplateContentMappingExtension
    {
        public static TemplateContentDto MapToTemplateContentDto(this FormTemplateContentEntity projectFrom, IMapper mapper)
            => mapper.Map<TemplateContentDto>(projectFrom);
        public static List<TemplateContentDto> MapToTemplateContentDtoList(this IEnumerable<FormTemplateContentEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToTemplateContentDto(mapper)).ToList();

    }
}
