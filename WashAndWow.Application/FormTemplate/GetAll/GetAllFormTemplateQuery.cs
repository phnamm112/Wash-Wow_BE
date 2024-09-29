using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.FormTemplate.GetAll
{
    public class GetAllFormTemplateQuery : IRequest<List<FormTemplateDto>>, IQuery
    {
        public GetAllFormTemplateQuery()
        {
            
        }
    }
}
