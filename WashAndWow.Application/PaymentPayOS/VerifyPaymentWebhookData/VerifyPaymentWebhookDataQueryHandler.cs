using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities.Third_Party_define;

namespace WashAndWow.Application.PaymentPayOS.VerifyPaymentWebhookData
{
    public class VerifyPaymentWebhookDataQueryHandler : IRequestHandler<VerifyPaymentWebhookDataQuery, WebhookData>
    {
        private readonly IConfiguration _config;
        private readonly PayOSKey _payOSKey;

        public VerifyPaymentWebhookDataQueryHandler(IConfiguration config, IOptions<PayOSKey> payOSKey)
        {
            _config = config;
            _payOSKey = payOSKey.Value;
        }

        public async Task<WebhookData> Handle(VerifyPaymentWebhookDataQuery request, CancellationToken cancellationToken)
        {
            PayOS payOS = new PayOS(_payOSKey.ClientId, _payOSKey.ApiKey, _payOSKey.ChecksumKey);

            WebhookData webhookData = payOS.verifyPaymentWebhookData(request.WebhookBody);
            return webhookData;
        }
    }
}
