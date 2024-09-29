using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Mappings;
using Wash_Wow.Application.Users;
using Wash_Wow.Domain.Entities;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Form
{
    public class FieldValueDto : IMapFrom<FormFieldValueEntity>
    {
        public string ID {  get; set; }
        public string FieldValue {  get; set; }
        public int FormTemplateContentID {  get; set; }
        public string Content {  get; set; }

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
