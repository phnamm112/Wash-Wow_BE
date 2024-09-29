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
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Form
{
    public class FormDto : IMapFrom<FormEntity>
    {
        public string Status { get; set; }
        public int FormTemplateID {  get; set; }
        public string Title {  get; set; }
        public List<FieldValueDto> FieldValues {  get; set; }

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
