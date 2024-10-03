using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Form.GetAll
{
    public class GetAllFormQuery : IRequest<List<FormDto>>, IQuery
    {
        public GetAllFormQuery()
        {

        }
    }
}
