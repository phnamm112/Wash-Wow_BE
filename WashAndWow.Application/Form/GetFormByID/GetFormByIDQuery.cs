using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Form.GetFormByID
{
    public class GetFormByIDQuery : IRequest<FormDto>, IQuery
    {
        public GetFormByIDQuery()
        {
        }

        public GetFormByIDQuery(string id)
        {
            ID = id;
        }
        public string ID { get; set; }
    }
}
