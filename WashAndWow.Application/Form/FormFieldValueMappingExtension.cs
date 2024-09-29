using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Form
{
    public static class FormFieldValueMappingExtension 
    {
        public static FieldValueDto MapToFieldValueDto(this FormFieldValueEntity projectFrom, IMapper mapper)
            => mapper.Map<FieldValueDto>(projectFrom);
        public static List<FieldValueDto> MapToFieldValueDtoList(this IEnumerable<FormFieldValueEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToFieldValueDto(mapper)).ToList();

        public static FieldValueDto MapToFieldValueDto(this FormFieldValueEntity entity, IMapper mapper, string content)
        {
            var dto = mapper.Map<FieldValueDto>(entity);
            dto.Content = content;
            return dto;
        }

        public static List<FieldValueDto> MapToFieldValueDtoList(this IEnumerable<FormFieldValueEntity> projectFrom, IMapper mapper, Dictionary<int, string> contents)
             => projectFrom.Select(x => x.MapToFieldValueDto(mapper,
             contents.ContainsKey(x.FormTemplateContentID) ? contents[x.FormTemplateContentID] : "Lỗi"
                    )).ToList();
    }
}
