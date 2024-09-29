using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Mappings;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Form
{
    public class FormImageDto : IMapFrom<FormImageEntity>
    {
        public string Url {  get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormImageEntity, FormImageDto>();
        }
    }
}
