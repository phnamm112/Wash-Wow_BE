using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Mappings;
using WashAndWow.Domain.Entities.ConfigTable;

namespace WashAndWow.Application.FormTemplate
{
    public class FormTemplateDto : IMapFrom<FormTemplateEntity>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<TemplateContentDto> FormTemplateContents { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormTemplateEntity, FormTemplateDto>();
        }
    }
}
