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
    public class FormFieldValueDto
    {
        public int FieldID { get; set; }  
        public string Value { get; set; }
    }
}
