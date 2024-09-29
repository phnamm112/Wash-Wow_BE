using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Application.Form;
using WashAndWow.Domain.Entities;
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
