using AutoMapper;
using WashAndWow.Domain.Entities.ConfigTable;

namespace WashAndWow.Application.FormTemplate
{
    public static class FormTemplateMappingExtension
    {
        public static FormTemplateDto MapToFormTemplateDto(this FormTemplateEntity projectFrom, IMapper mapper)
            => mapper.Map<FormTemplateDto>(projectFrom);
        public static List<FormTemplateDto> MapToFormTemplateDtoList(this IEnumerable<FormTemplateEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToFormTemplateDto(mapper)).ToList();

    }
}
