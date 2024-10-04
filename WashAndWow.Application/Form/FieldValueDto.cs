using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Form
{
    public class FieldValueDto : IMapFrom<FormFieldValueEntity>
    {
        public string ID { get; set; }
        public string FieldValue { get; set; }
        public int FormTemplateContentID { get; set; }
        public string Content { get; set; }

        public FieldValueDto()
        {

        }
        public FieldValueDto(string iD, string fieldValue, int templateContentID, string content)
        {
            ID = iD;
            FieldValue = fieldValue;
            FormTemplateContentID = templateContentID;
            Content = content;
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormFieldValueEntity, FieldValueDto>();
        }
    }
}
