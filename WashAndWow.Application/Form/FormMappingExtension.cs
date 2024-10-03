using AutoMapper;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Form
{
    public static class FormMappingExtension
    {
        public static FormDto MapToFormDto(this FormEntity projectFrom, IMapper mapper)
            => mapper.Map<FormDto>(projectFrom);
        public static List<FormDto> MapToFormDtoList(this IEnumerable<FormEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToFormDto(mapper)).ToList();

        public static FormDto MapToFormDto(this FormEntity projectFrom, IMapper mapper, string title, Dictionary<int, string> contents)
        {
            var dto = mapper.Map<FormDto>(projectFrom);
            dto.Title = title;
            dto.Status = projectFrom.Status.ToString();
            dto.FieldValues = projectFrom.FieldValues.MapToFieldValueDtoList(mapper, contents);
            return dto;
        }
        public static List<FormDto> MapToFormDtoList(this IEnumerable<FormEntity> projectFrom, IMapper mapper
            , Dictionary<int, string> title
            , Dictionary<int, string> contents)
        => projectFrom.Select(x => x.MapToFormDto(mapper,
             title.ContainsKey(x.FormTemplateID) ? title[x.FormTemplateID] : "Lỗi",
             contents
                    )).ToList();
    }
}
