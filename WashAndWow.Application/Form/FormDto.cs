using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Form
{
    public class FormDto : IMapFrom<FormEntity>
    {
        public string ID { get; set; }
        public string Status { get; set; }
        public string SenderID {  get; set; }
        public int FormTemplateID { get; set; }
        public string Title { get; set; }
        public List<FormImageDto> FormImages { get; set; }
        public List<FieldValueDto> FieldValues { get; set; }

        public FormDto()
        {

        }
        public FormDto(string status, int formTemplateID, string title, List<FieldValueDto> fieldValues)
        {
            Status = status;
            FormTemplateID = formTemplateID;
            Title = title;
            FieldValues = fieldValues;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormEntity, FormDto>();
        }
    }
}
