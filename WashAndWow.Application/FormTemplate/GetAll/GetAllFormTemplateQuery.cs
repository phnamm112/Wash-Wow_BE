using MediatR;
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
