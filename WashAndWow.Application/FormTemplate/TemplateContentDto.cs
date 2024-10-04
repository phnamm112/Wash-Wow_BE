using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using WashAndWow.Domain.Entities.ConfigTable;

namespace WashAndWow.Application.FormTemplate
{
    public class TemplateContentDto : IMapFrom<FormTemplateContentEntity>
    {
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormTemplateContentEntity, TemplateContentDto>();
        }
    }
}
