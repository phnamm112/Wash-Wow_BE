using MediatR;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.PaymentPayOS.VerifyPaymentWebhookData
{
    public class VerifyPaymentWebhookDataQuery : IQuery, IRequest<WebhookData>
    {
        public WebhookType WebhookBody {  get; set; }
        public VerifyPaymentWebhookDataQuery()
        {
            
        }
    }
}
