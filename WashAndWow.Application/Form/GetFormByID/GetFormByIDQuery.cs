using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Form.GetFormByID
{
    public class GetFormByIDQuery : IRequest<FormDto>, IQuery
    {
        public GetFormByIDQuery(string id)
        {
            ID = id;
        }
        public string ID { get; set; }
    }
}
