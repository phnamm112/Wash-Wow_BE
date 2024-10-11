using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Net.payOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities.Third_Party_define;

namespace WashAndWow.Application.PaymentPayOS.ConfirmWebhook
{
    public class ConfirmWebhookCommandHandler : IRequestHandler<ConfirmWebhookCommand, string>
    {
        private readonly IConfiguration _config;
        private readonly PayOSKey _payOSKey;

        public ConfirmWebhookCommandHandler(IConfiguration config, IOptions<PayOSKey> payOSKey)
        {
            _config = config;
            _payOSKey = payOSKey.Value;
        }

        public async Task<string> Handle(ConfirmWebhookCommand request, CancellationToken cancellationToken)
        {
            PayOS payOS = new PayOS(_payOSKey.ClientId, _payOSKey.ApiKey, _payOSKey.ChecksumKey);

            return await payOS.confirmWebhook(_payOSKey.Domain);
        }
    }
}
