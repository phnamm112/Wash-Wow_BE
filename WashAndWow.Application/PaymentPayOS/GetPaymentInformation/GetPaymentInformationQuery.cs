using MediatR;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.PaymentPayOS.GetPaymentInformation
{
    public class GetPaymentInformationQuery : IRequest<PaymentLinkInformation>, IQuery
    {
        public GetPaymentInformationQuery()
        {
            
        }
        public int PaymentID { get; set; }
    }
}
